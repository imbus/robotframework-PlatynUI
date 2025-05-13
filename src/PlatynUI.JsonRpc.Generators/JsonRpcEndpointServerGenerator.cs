using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace PlatynUI.JsonRpc.Generators;

[Generator]
public class JsonRpcEndpointServerGenerator : IIncrementalGenerator
{
    private const string JsonRpcEndpointAttributeName = "PlatynUI.JsonRpc.JsonRpcEndpointAttribute";
    private const string JsonRpcRequestAttributeName = "PlatynUI.JsonRpc.JsonRpcRequestAttribute";
    private const string JsonRpcNotificationAttributeName = "PlatynUI.JsonRpc.JsonRpcNotificationAttribute";
    private const string TaskTypeName = "System.Threading.Tasks.Task";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classesWithRpc = context
            .SyntaxProvider.CreateSyntaxProvider(
                predicate: (node, _) => node is ClassDeclarationSyntax cds && cds.BaseList != null,
                transform: (ctx, _) =>
                {
                    var classDecl = (ClassDeclarationSyntax)ctx.Node;

                    bool isPartial = classDecl.Modifiers.Any(m => m.ValueText == "partial");
                    if (!isPartial)
                        return null;

                    var model = ctx.SemanticModel;
                    if (model.GetDeclaredSymbol(classDecl) is not INamedTypeSymbol classSym)
                        return null;

                    var endpointInfos = new List<EndpointInfo>();

                    foreach (var iface in classSym.AllInterfaces)
                    {
                        var endpointAttr = iface
                            .GetAttributes()
                            .FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == JsonRpcEndpointAttributeName);
                        if (endpointAttr == null)
                            continue;

                        var endpointName = endpointAttr.ConstructorArguments[0].Value as string ?? iface.Name;
                        var methods = iface
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
                        if (methods.Length == 0)
                            continue;

                        endpointInfos.Add(new EndpointInfo(classSym, iface, endpointName, methods));
                    }

                    return endpointInfos.Count > 0 ? new ClassEndpointInfos(classSym, endpointInfos) : null;
                }
            )
            .Where(x => x != null)!;

        context.RegisterSourceOutput(classesWithRpc, (spc, info) => GenerateSource(spc, info!));
    }

    private void GenerateSource(SourceProductionContext context, ClassEndpointInfos classInfo)
    {
        var ns = classInfo.ClassSymbol.ContainingNamespace.IsGlobalNamespace
            ? string.Empty
            : classInfo.ClassSymbol.ContainingNamespace.ToDisplayString();
        var className = classInfo.ClassSymbol.Name;

        var methodNameMap =
            new Dictionary<string, List<(IMethodSymbol Method, string InterfaceName, string MethodName)>>();

        foreach (var info in classInfo.EndpointInfos)
        {
            var endpointName = info.EndpointName;
            var interfaceName = info.InterfaceSymbol.Name;

            foreach (var m in info.Methods)
            {
                var attr = m.GetAttributes()
                    .First(a =>
                        a.AttributeClass?.ToDisplayString()
                            is JsonRpcRequestAttributeName
                                or JsonRpcNotificationAttributeName
                    );
                var rpcName = string.IsNullOrWhiteSpace(attr.ConstructorArguments[0].Value as string)
                    ? m.Name
                    : attr.ConstructorArguments[0].Value as string ?? m.Name;
                var fullMethod = String.IsNullOrWhiteSpace(endpointName) ? $"{rpcName}" : $"{endpointName}/{rpcName}";

                if (!methodNameMap.TryGetValue(fullMethod, out var methods))
                {
                    methods = [];
                    methodNameMap[fullMethod] = methods;
                }

                methods.Add((m, interfaceName, m.Name));
            }
        }

        foreach (var entry in methodNameMap)
        {
            if (entry.Value.Count > 1)
            {
                Location location = classInfo.ClassSymbol.Locations.FirstOrDefault() ?? Location.None;

                var conflictDescriptions = string.Join(
                    ", ",
                    entry.Value.Select(m => $"{m.InterfaceName}.{m.MethodName}")
                );

                context.ReportDiagnostic(
                    Diagnostic.Create(
                        new DiagnosticDescriptor(
                            "PJSONRPC001",
                            "Duplicate JSON-RPC method name",
                            "Duplicate JSON-RPC method name '{0}' in class '{1}'. Conflicting methods: {2}. Method names must be unique within a JSON-RPC endpoint.",
                            "JsonRpc",
                            DiagnosticSeverity.Error,
                            true
                        ),
                        location,
                        entry.Key,
                        className,
                        conflictDescriptions
                    )
                );
            }
        }

        var sb = new StringBuilder();

        sb.AppendLine($"using System.Text.Json;");
        sb.AppendLine($"using System.Text.Json.Serialization;");
        sb.AppendLine($"using PlatynUI.JsonRpc;");

        sb.AppendLine();
        if (!string.IsNullOrEmpty(ns))
            sb.AppendLine($"namespace {ns};");
        sb.AppendLine();

        sb.AppendLine("#nullable enable");
        sb.AppendLine();

        sb.AppendLine($"partial class {className}");
        sb.AppendLine("{");

        var allMethods = classInfo.EndpointInfos.SelectMany(ei => ei.Methods).Distinct(MethodEqualityComparer.Instance);

        foreach (var m in allMethods)
        {
            var containingInterface = m.ContainingType.Name;
            var recName = $"{containingInterface}_{m.Name}Params";

            sb.AppendLine($"    internal record {recName}");
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
        }
        sb.AppendLine();

        sb.AppendLine($"    public static {className} Attach(JsonRpcPeer peer, {className}? instance = null)");
        sb.AppendLine("    {");
        sb.AppendLine($"        var impl = instance ?? new {className}();");

        foreach (var info in classInfo.EndpointInfos)
        {
            var interfaceName = info.InterfaceSymbol.ToDisplayString();
            var endpointName = info.EndpointName;

            sb.AppendLine($"        var target{info.InterfaceSymbol.Name} = ({interfaceName})impl;");

            foreach (var m in info.Methods)
            {
                var attr = m.GetAttributes()
                    .First(a =>
                        a.AttributeClass?.ToDisplayString()
                            is JsonRpcRequestAttributeName
                                or JsonRpcNotificationAttributeName
                    );
                var rpcName = string.IsNullOrWhiteSpace(attr.ConstructorArguments[0].Value as string)
                    ? m.Name
                    : attr.ConstructorArguments[0].Value as string ?? m.Name;
                var fullMethod = String.IsNullOrWhiteSpace(endpointName) ? $"{rpcName}" : $"{endpointName}/{rpcName}";
                var isRequest = attr.AttributeClass!.ToDisplayString() == JsonRpcRequestAttributeName;

                if (isRequest)
                    sb.AppendLine($"        peer.RegisterRequestHandler(\"{fullMethod}\", (@params, options) =>");
                else
                    sb.AppendLine($"        peer.RegisterNotificationHandler(\"{fullMethod}\", (@params, options) =>");

                sb.AppendLine("        {");
                sb.AppendLine("            var elem = (JsonElement)(@params ?? throw new ArgumentException());");
                sb.AppendLine(
                    $"            var dto = elem.Deserialize<{m.ContainingType.Name}_{m.Name}Params>(options)!;"
                );
                var args = string.Join(", ", m.Parameters.Select(p => $"dto.{p.Name}"));

                if (isRequest)
                {
                    if (m.ReturnsVoid)
                    {
                        sb.AppendLine($"            target{info.InterfaceSymbol.Name}.{m.Name}({args});");
                        sb.AppendLine($"            return Task.FromResult<object?>(null);");
                    }
                    else if (IsTaskLike(m.ReturnType))
                        sb.AppendLine($"            return target{info.InterfaceSymbol.Name}.{m.Name}({args});");
                    else
                        sb.AppendLine(
                            $"            return Task.FromResult(target{info.InterfaceSymbol.Name}.{m.Name}({args}));"
                        );
                }
                else
                {
                    if (IsTaskLike(m.ReturnType))
                        sb.AppendLine($"            _ = target{info.InterfaceSymbol.Name}.{m.Name}({args});");
                    else
                        sb.AppendLine($"            target{info.InterfaceSymbol.Name}.{m.Name}({args});");
                    sb.AppendLine("            return Task.CompletedTask;");
                }
                sb.AppendLine(isRequest ? "        });" : "        });");
            }
            sb.AppendLine();
        }

        sb.AppendLine("        return impl;");
        sb.AppendLine("    }");
        sb.AppendLine("}");

        context.AddSource($"{className}_JsonRpcServer.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
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

    private class EndpointInfo(
        INamedTypeSymbol classSymbol,
        INamedTypeSymbol interfaceSymbol,
        string endpointName,
        IMethodSymbol[] methods
    )
    {
        public INamedTypeSymbol ClassSymbol = classSymbol;
        public INamedTypeSymbol InterfaceSymbol = interfaceSymbol;
        public string EndpointName = endpointName;
        public IMethodSymbol[] Methods = methods;
    }

    private class ClassEndpointInfos
    {
        public ClassEndpointInfos(INamedTypeSymbol classSymbol, List<EndpointInfo> endpointInfos)
        {
            ClassSymbol = classSymbol;
            EndpointInfos = endpointInfos;
        }

        public INamedTypeSymbol ClassSymbol;
        public List<EndpointInfo> EndpointInfos;
    }

    private class MethodEqualityComparer : IEqualityComparer<IMethodSymbol>
    {
        public static readonly MethodEqualityComparer Instance = new MethodEqualityComparer();

        public bool Equals(IMethodSymbol x, IMethodSymbol y)
        {
            if (x == null || y == null)
                return SymbolEqualityComparer.Default.Equals(x, y);

            if (
                x.Name != y.Name
                || x.Parameters.Length != y.Parameters.Length
                || !SymbolEqualityComparer.Default.Equals(x.ContainingType, y.ContainingType)
            )
                return false;

            for (int i = 0; i < x.Parameters.Length; i++)
            {
                var xParam = x.Parameters[i];
                var yParam = y.Parameters[i];

                if (xParam.Name != yParam.Name || !SymbolEqualityComparer.Default.Equals(xParam.Type, yParam.Type))
                    return false;
            }

            return true;
        }

        public int GetHashCode(IMethodSymbol obj)
        {
            var hash = obj.Name.GetHashCode();
            hash = hash * 31 + SymbolEqualityComparer.Default.GetHashCode(obj.ContainingType);
            foreach (var param in obj.Parameters)
            {
                hash = hash * 31 + param.Name.GetHashCode();
            }
            return hash;
        }
    }
}
