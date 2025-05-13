using PlatynUI.Runtime;
using PlatynUI.Runtime.Core;

namespace PlatynUI.JsonRpc.Endpoints;

[JsonRpcEndpoint("displayDevice")]
public partial interface IDisplayDeviceEndpoint
{
    [JsonRpcRequest("getBoundingRectangle")]
    public Rect GetBoundingRectangle();

    [JsonRpcRequest("highlightRect")]
    public void HighlightRect(double x, double y, double width, double height, double time = 3);
}

public enum MouseMoveType
{
    Direct,
    Linear,
    Curved,
    Shaky,
    BezierCurved,
    Overshooting,
}

public enum MouseAcceleration
{
    Constant,
    FastToSlow,
    SlowToFast,
    Smooth,
}

public record MouseOptions
{
    public MouseMoveType? MoveType { get; init; }
    public double? MoveStepsPerPixel { get; init; }
    public MouseAcceleration? Acceleration { get; init; }
    public int? MultiClickDelay { get; init; }
    public int? AfterPressReleaseDelay { get; init; }
    public int? MaxMoveDuration { get; init; }
    public int? AfterMoveDelay { get; init; }
    public int? EnsureMoveDelay { get; init; }
    public bool? EnsureMovePosition { get; init; }
    public bool? EnsureClickPosition { get; init; }
    public double? EnsureMoveThreshold { get; init; }
    public int? AfterClickDelay { get; init; }
    public int? BeforeNextClickDelay { get; init; }
}

[JsonRpcEndpoint("mouseDevice")]
public partial interface IMouseDeviceEndpoint
{
    [JsonRpcRequest("getDoubleClickTime")]
    int GetDoubleClickTime();

    [JsonRpcRequest("getDoubleClickSize")]
    Size GetDoubleClickSize();

    [JsonRpcRequest("getPosition")]
    Point GetPosition();

    [JsonRpcRequest("move")]
    Point Move(double? x = null, double? y = null, MouseOptions? options = null);

    [JsonRpcRequest("press")]
    void Press(MouseButton? button = null, double? x = null, double? y = null, MouseOptions? options = null);

    [JsonRpcRequest("release")]
    void Release(MouseButton? button = null, double? x = null, double? y = null, MouseOptions? options = null);

    [JsonRpcRequest("click")]
    void Click(
        MouseButton? button = null,
        double? x = null,
        double? y = null,
        int count = 1,
        MouseOptions? options = null
    );
}
