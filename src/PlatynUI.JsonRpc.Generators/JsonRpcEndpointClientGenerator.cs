using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace PlatynUI.JsonRpc.Generators;

[Generator]
public class JsonRpcEndpointClientGenerator : IIncrementalGenerator
{
    private const string JsonRpcEndpointAttributeName = "PlatynUI.JsonRpc.JsonRpcEndpointAttribute";
    private const string JsonRpcRequestAttributeName = "PlatynUI.JsonRpc.JsonRpcRequestAttribute";
    private const string JsonRpcNotificationAttributeName = "PlatynUI.JsonRpc.JsonRpcNotificationAttribute";
    private const string TaskTypeName = "System.Threading.Tasks.Task";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var interfaces = context
            .SyntaxProvider.CreateSyntaxProvider(
                predicate: (node, _) => node is InterfaceDeclarationSyntax,
                transform: (ctx, _) =>
                {
                    var interfaceDecl = (InterfaceDeclarationSyntax)ctx.Node;

                    if (!interfaceDecl.Modifiers.Any(m => m.ValueText == "partial"))
                        return null;

                    var model = ctx.SemanticModel;
                    if (model.GetDeclaredSymbol(interfaceDecl) is not INamedTypeSymbol interfaceSym)
                        return null;

                    var endpointAttr = interfaceSym
                        .GetAttributes()
                        .FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == JsonRpcEndpointAttributeName);
                    if (endpointAttr == null)
                        return null;

                    var endpointName = endpointAttr
                        .ConstructorArguments.Select(arg => arg.Value as string)
                        .FirstOrDefault(value => !string.IsNullOrEmpty(value))
                        is string name
                        ? $"{name}"
                        : string.Empty;

                    var methods = interfaceSym
                        .GetMembers()
                        .OfType<IMethodSymbol>()
                        .Where(m =>
                            m.GetAttributes()
                                .Any(a =>
                                    a.AttributeClass?.ToDisplayString()
                                        is JsonRpcRequestAttributeName
                                            or JsonRpcNotificationAttributeName
                                )
                        )
                        .ToArray();

                    return methods.Length > 0 ? new InterfaceInfo(interfaceSym, endpointName, methods) : null;
                }
            )
            .Where(x => x != null)!;

        context.RegisterSourceOutput(interfaces, (spc, info) => GenerateSource(spc, info!));
    }

    private void GenerateSource(SourceProductionContext context, InterfaceInfo info)
    {
        var ns = info.InterfaceSymbol.ContainingNamespace.IsGlobalNamespace
            ? string.Empty
            : info.InterfaceSymbol.ContainingNamespace.ToDisplayString();

        var interfaceName = info.InterfaceSymbol.Name;
        var interfaceAccessibility = info.InterfaceSymbol.DeclaredAccessibility.ToString().ToLowerInvariant();

        var sb = new StringBuilder();

        sb.AppendLine($"using System.Text.Json;");
        sb.AppendLine($"using System.Text.Json.Serialization;");
        sb.AppendLine($"using System.Threading.Tasks;");
        sb.AppendLine($"using PlatynUI.JsonRpc;");
        sb.AppendLine();

        if (!string.IsNullOrEmpty(ns))
            sb.AppendLine($"namespace {ns};");
        sb.AppendLine();

        sb.AppendLine("#nullable enable");
        sb.AppendLine();

        sb.AppendLine($"{interfaceAccessibility} partial interface {interfaceName}");
        sb.AppendLine("{");
        sb.AppendLine($"    public static {interfaceName} Attach(JsonRpcPeer peer)");
        sb.AppendLine("    {");
        sb.AppendLine($"        return new {interfaceName}ClientImplementation.{interfaceName}ClientImpl(peer);");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();

        sb.AppendLine($"file static class {interfaceName}ClientImplementation");
        sb.AppendLine("{");

        foreach (var m in info.Methods)
        {
            var recName = $"{m.Name}Params";

            sb.AppendLine($"    private record {recName}");
            sb.AppendLine("    {");

            foreach (var p in m.Parameters)
            {
                var typeStr = p.Type.ToDisplayString();

                var requiredModifier = p.HasExplicitDefaultValue ? "" : "required ";

                sb.Append($"        public {requiredModifier}{typeStr} {p.Name} {{ get; init; }}");

                if (p.HasExplicitDefaultValue)
                {
                    var defaultValue = p.ExplicitDefaultValue;
                    string defaultValueStr;

                    if (defaultValue == null)
                    {
                        defaultValueStr = "null";
                    }
                    else if (defaultValue is string stringValue)
                    {
                        defaultValueStr = $"\"{stringValue}\"";
                    }
                    else if (defaultValue is bool boolValue)
                    {
                        defaultValueStr = boolValue ? "true" : "false";
                    }
                    else
                    {
                        defaultValueStr = defaultValue.ToString();
                    }

                    sb.Append($" = {defaultValueStr};");
                }

                sb.AppendLine();
            }

            sb.AppendLine("    }");
            sb.AppendLine();
        }

        sb.AppendLine($"    internal class {interfaceName}ClientImpl : {interfaceName}");
        sb.AppendLine("    {");
        sb.AppendLine("        private readonly JsonRpcPeer _peer;");
        sb.AppendLine();
        sb.AppendLine($"        public {interfaceName}ClientImpl(JsonRpcPeer peer)");
        sb.AppendLine("        {");
        sb.AppendLine("            _peer = peer;");
        sb.AppendLine("        }");
        sb.AppendLine();

        foreach (var m in info.Methods)
        {
            var attr = m.GetAttributes()
                .First(a =>
                    a.AttributeClass?.ToDisplayString()
                        is JsonRpcRequestAttributeName
                            or JsonRpcNotificationAttributeName
                );
            var isNotification = attr.AttributeClass!.ToDisplayString() == JsonRpcNotificationAttributeName;
            var rpcName = string.Empty;

            if (attr.ConstructorArguments.Length > 0)
            {
                rpcName = attr.ConstructorArguments[0].Value as string ?? m.Name;
            }
            else
            {
                rpcName = m.Name;
            }

            sb.Append($"        public {m.ReturnType.ToDisplayString()} {m.Name}(");

            var paramList = string.Join(
                ", ",
                m.Parameters.Select(p =>
                    $"{p.Type.ToDisplayString()} {p.Name}{(p.HasExplicitDefaultValue ? " = " + FormatDefaultValue(p) : "")}"
                )
            );
            sb.Append(paramList);
            sb.AppendLine(")");
            sb.AppendLine("        {");

            sb.Append($"            var @params = new {m.Name}Params {{ ");
            sb.Append(string.Join(", ", m.Parameters.Select(p => $"{p.Name} = {p.Name}")));
            sb.AppendLine(" };");

            var isTaskLike = IsTaskLike(m.ReturnType);

            var fullMethod = String.IsNullOrWhiteSpace(rpcName) ? m.Name : rpcName;
            fullMethod = String.IsNullOrWhiteSpace(info.EndpointName)
                ? fullMethod
                : $"{info.EndpointName}/{fullMethod}";

            if (isNotification)
            {
                bool isValid =
                    m.ReturnType.ToDisplayString() == "void" || m.ReturnType.ToDisplayString() == TaskTypeName;

                if (!isValid)
                {
                    var location =
                        m.Locations.Length > 0 && m.Locations[0].SourceTree != null
                            ? Location.Create(m.Locations[0].SourceTree!, m.Locations[0].SourceSpan)
                            : null;

                    context.ReportDiagnostic(
                        Diagnostic.Create(
                            new DiagnosticDescriptor(
                                "PJSONRPC002",
                                "Invalid return type for JsonRpc notification",
                                "Notification method '{0}' must return 'void' or 'Task', but returns '{1}'",
                                "JsonRpc",
                                DiagnosticSeverity.Error,
                                true
                            ),
                            location,
                            m.Name,
                            m.ReturnType.ToDisplayString()
                        )
                    );
                }
                {
                    if (isTaskLike)
                    {
                        sb.AppendLine($"            _peer.SendNotificationAsync(\"{fullMethod}\", @params);");
                    }
                    else
                    {
                        sb.AppendLine($"            _peer.SendNotification(\"{fullMethod}\", @params);");
                    }
                }
            }
            else
            {
                if (isTaskLike)
                {
                    if (m.ReturnType.ToString() == TaskTypeName)
                    {
                        sb.AppendLine($"            return _peer.SendRequestAsync(\"{fullMethod}\", @params);");
                    }
                    else
                    {
                        var resultType = ((INamedTypeSymbol)m.ReturnType).TypeArguments[0].ToDisplayString();
                        sb.AppendLine(
                            $"            return _peer.SendRequestAsync<{resultType}>(\"{fullMethod}\", @params);"
                        );
                    }
                }
                else
                {
                    if (m.ReturnType.ToDisplayString() == "void")
                    {
                        sb.AppendLine($"            _peer.SendRequest(\"{fullMethod}\", @params);");
                    }
                    else
                    {
                        sb.AppendLine(
                            $"            return _peer.SendRequest<{m.ReturnType.ToDisplayString()}>(\"{fullMethod}\", @params);"
                        );
                    }
                }
            }

            sb.AppendLine("        }");
            sb.AppendLine();
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");

        context.AddSource($"{interfaceName}_JsonRpcClient.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }

    private bool IsTaskLike(ITypeSymbol typeSymbol)
    {
        if (typeSymbol == null)
            return false;

        if (typeSymbol.OriginalDefinition != null)
        {
            var taskSymbol = typeSymbol.ContainingAssembly?.GetTypeByMetadataName(TaskTypeName);
            if (taskSymbol != null && SymbolEqualityComparer.Default.Equals(typeSymbol.OriginalDefinition, taskSymbol))
                return true;

            var genericTaskSymbol = typeSymbol.ContainingAssembly?.GetTypeByMetadataName(TaskTypeName + "`1");
            if (
                genericTaskSymbol != null
                && SymbolEqualityComparer.Default.Equals(typeSymbol.OriginalDefinition, genericTaskSymbol)
            )
                return true;
        }

        return typeSymbol.BaseType != null && IsTaskLike(typeSymbol.BaseType);
    }

    private string FormatDefaultValue(IParameterSymbol parameter)
    {
        if (!parameter.HasExplicitDefaultValue)
            return string.Empty;

        var value = parameter.ExplicitDefaultValue;
        if (value == null)
            return "null";

        if (value is string s)
            return $"\"{s}\"";

        if (value is bool b)
            return b ? "true" : "false";

        return value.ToString();
    }

    private class InterfaceInfo(INamedTypeSymbol interfaceSymbol, string endpointName, IMethodSymbol[] methods)
    {
        public INamedTypeSymbol InterfaceSymbol = interfaceSymbol;
        public string EndpointName = endpointName;
        public IMethodSymbol[] Methods = methods;
    }
}
