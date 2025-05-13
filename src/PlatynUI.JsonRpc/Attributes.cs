namespace PlatynUI.JsonRpc;

[AttributeUsage(AttributeTargets.Method)]
public class JsonRpcRequestAttribute(string name = "") : Attribute
{
    public string Name { get; } = name;
}

[AttributeUsage(AttributeTargets.Method)]
public class JsonRpcNotificationAttribute(string name = "") : Attribute
{
    public string Name { get; } = name;
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class JsonRpcEndpointAttribute(string @namespace = "") : Attribute
{
    public string Namespace { get; set; } = @namespace;
}

public abstract class JsonRpcEndpoint { }
