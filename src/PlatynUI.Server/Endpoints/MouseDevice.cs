using System.Diagnostics;
using PlatynUI.JsonRpc.Endpoints;
using PlatynUI.Runtime;
using PlatynUI.Runtime.Core;

namespace PlatynUI.Server.Endpoints;

partial class MouseDeviceEndpoint : IMouseDeviceEndpoint
{
    public Size? DoubleClickSize { get; init; } = null;
    public int? DoubleClickTime { get; init; } = null;

    public Size GetDoubleClickSize()
    {
        return DoubleClickSize ?? MouseDevice.GetDoubleClickSize();
    }

    public int GetDoubleClickTime()
    {
        return DoubleClickTime ?? MouseDevice.GetDoubleClickTime();
    }

    public Point GetPosition()
    {
        return MouseDevice.GetPosition();
    }

    public MouseButton DefaultButton = MouseButton.Left;
    public int MultiClickDelay = 200;
    public int AfterPressReleaseDelay = 50;
    public int MaxMoveDuration = 1500;
    public int AfterMoveDelay = 10;
    public int EnsureMoveDelay = 500;
    public bool EnsureMovePosition = true;
    public double EnsureMoveThreshold { get; init; } = 0.5;
    public bool EnsureClickPosition = true;
    public int AfterClickDelay = 50;
    public int BeforeNextClickDelay = 50;
    public MouseMoveType MoveType = MouseMoveType.Linear;
    public double MoveStepsPerPixel = 0.3;
    public MouseAcceleration Acceleration = MouseAcceleration.Constant;

    private static Tuple<double, double> GetPoint(double? x, double? y)
    {
        if (x != null && y != null)
        {
            return new Tuple<double, double>(x.Value, y.Value);
        }

        var mousePos = MouseDevice.GetPosition();

        return new Tuple<double, double>(x ?? mousePos.X, y ?? mousePos.Y);
    }

    public Point Move(double? x = null, double? y = null, MouseOptions? options = null)
    {
        if (x == null && y == null)
        {
            return MouseDevice.GetPosition();
        }
        var (x1, y1) = GetPoint(x, y);

        var afterMoveDelay = options?.AfterMoveDelay ?? AfterMoveDelay;

        try
        {
            var moveType = options?.MoveType ?? MoveType;
            if (moveType == MouseMoveType.Direct)
            {
                MouseDevice.Move(x1, y1);
            }
            else
            {
                var algorithm = GetMoveAlgorithm(moveType);
                var stepsPerPixel = options?.MoveStepsPerPixel ?? MoveStepsPerPixel;
                var acceleration = options?.Acceleration ?? Acceleration;
                ExecuteMouseMovement(
                    x1,
                    y1,
                    options?.MaxMoveDuration ?? (Math.Max(0, MaxMoveDuration - afterMoveDelay)),
                    algorithm,
                    stepsPerPixel,
                    acceleration
                );
            }
        }
        finally
        {
            Thread.Sleep(afterMoveDelay);
        }

        var ensureMovePostion = options?.EnsureMovePosition ?? EnsureMovePosition;
        var ensureMoveDelay = options?.EnsureMoveDelay ?? EnsureMoveDelay;

        var ensureMoveThreshold = options?.EnsureMoveThreshold ?? EnsureMoveThreshold;
        if (ensureMoveThreshold < 0)
        {
            ensureMoveThreshold = 0;
        }

        var newPos = MouseDevice.GetPosition();
        if (ensureMovePostion)
        {
            var stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedMilliseconds < ensureMoveDelay)
            {
                if (Math.Abs(newPos.X - x1) <= ensureMoveThreshold && Math.Abs(newPos.Y - y1) <= ensureMoveThreshold)
                {
                    break;
                }
                Thread.Sleep(5);
                newPos = MouseDevice.GetPosition();
            }
            stopwatch.Stop();
        }

        if (
            ensureMovePostion
            && (Math.Abs(newPos.X - x1) > ensureMoveThreshold || Math.Abs(newPos.Y - y1) > ensureMoveThreshold)
        )
        {
            throw new Exception(
                $"Mouse position is not as expected. Expected: ({x1}, {y1}), Actual: ({newPos.X}, {newPos.Y})"
            );
        }

        return newPos;
    }

    private delegate IEnumerable<(double X, double Y)> MouseMoveAlgorithm(
        double startX,
        double startY,
        double targetX,
        double targetY,
        double distance,
        int steps,
        Rect screen,
        Random random
    );

    private static MouseMoveAlgorithm GetMoveAlgorithm(MouseMoveType moveType)
    {
        return moveType switch
        {
            MouseMoveType.Linear => LinearMoveAlgorithm,
            MouseMoveType.Curved => CurvedMoveAlgorithm,
            MouseMoveType.Shaky => ShakyMoveAlgorithm,
            MouseMoveType.BezierCurved => BezierMoveAlgorithm,
            MouseMoveType.Overshooting => OvershootingMoveAlgorithm,
            _ => LinearMoveAlgorithm,
        };
    }

    private static void ExecuteMouseMovement(
        double x,
        double y,
        int maxMoveDuration,
        MouseMoveAlgorithm algorithm,
        double stepsPerPixel,
        MouseAcceleration acceleration
    )
    {
        var screen = DisplayDevice.GetBoundingRectangle();
        var currentPosition = MouseDevice.GetPosition();
        var totalDistance = CalculateDistance(currentPosition.X, currentPosition.Y, x, y);

        x = Math.Max(screen.Left, Math.Min(screen.Right, x));
        y = Math.Max(screen.Top, Math.Min(screen.Bottom, y));

        if (totalDistance < 5)
        {
            MouseDevice.Move(x, y);
            return;
        }

        var maxPossibleDistance = Math.Sqrt(
            Math.Pow(screen.Right - screen.Left, 2) + Math.Pow(screen.Bottom - screen.Top, 2)
        );

        var actualMoveDuration = (int)(maxMoveDuration * (totalDistance / maxPossibleDistance));
        actualMoveDuration = Math.Max(actualMoveDuration, 100);

        var steps = Math.Min((int)(totalDistance * stepsPerPixel), 500);
        steps = Math.Max(steps, 20);

        var random = new Random();
        var stopwatch = Stopwatch.StartNew();

        var positions = algorithm(currentPosition.X, currentPosition.Y, x, y, totalDistance, steps, screen, random)
            .ToList();
        var totalSteps = positions.Count;

        if (positions.Count > 0 && (Math.Abs(positions[^1].X - x) > 0.1 || Math.Abs(positions[^1].Y - y) > 0.1))
        {
            positions.Add((x, y));
            totalSteps++;
        }

        for (int i = 0; i < positions.Count; i++)
        {
            var (X, Y) = positions[i];
            MouseDevice.Move(X, Y);

            if (i == positions.Count - 1)
                continue;

            var elapsedMs = stopwatch.ElapsedMilliseconds;

            var remainingTimeMs = Math.Max(0, actualMoveDuration - elapsedMs);
            var remainingSteps = totalSteps - i - 1;

            if (remainingSteps <= 0 || remainingTimeMs <= 0)
                continue;

            var progressFactor = (double)(i + 1) / totalSteps;

            var delayFactor = acceleration switch
            {
                MouseAcceleration.FastToSlow => EaseOutQuad(progressFactor),
                MouseAcceleration.SlowToFast => 1 - EaseInQuad(progressFactor),
                MouseAcceleration.Smooth => 0.5 + 0.5 * Math.Sin((progressFactor - 0.5) * Math.PI),
                MouseAcceleration.Constant => 0.5,
                _ => EaseOutQuad(progressFactor),
            };

            var delayMs = (int)(remainingTimeMs * delayFactor / remainingSteps);

            if (delayMs > 0)
            {
                Thread.Sleep(delayMs);
            }
        }

        MouseDevice.Move(x, y);
    }

    private static (double X, double Y) ClampToScreen(double x, double y, Rect screen)
    {
        return (Math.Max(screen.Left, Math.Min(screen.Right, x)), Math.Max(screen.Top, Math.Min(screen.Bottom, y)));
    }

    private static double CalculateDistance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }

    private static IEnumerable<(double X, double Y)> LinearMoveAlgorithm(
        double startX,
        double startY,
        double targetX,
        double targetY,
        double distance,
        int steps,
        Rect screen,
        Random random
    )
    {
        for (var i = 1; i <= steps; i++)
        {
            var t = (double)i / steps;
            var intermediateX = startX + (targetX - startX) * t;
            var intermediateY = startY + (targetY - startY) * t;
            (intermediateX, intermediateY) = ClampToScreen(intermediateX, intermediateY, screen);
            yield return (intermediateX, intermediateY);
        }
    }

    private static IEnumerable<(double X, double Y)> CurvedMoveAlgorithm(
        double startX,
        double startY,
        double targetX,
        double targetY,
        double distance,
        int steps,
        Rect screen,
        Random random
    )
    {
        var curveAmplitude = distance * (0.05 + random.NextDouble() * 0.1);

        if (random.Next(2) == 0)
            curveAmplitude = -curveAmplitude;

        var dx = targetX - startX;
        var dy = targetY - startY;

        var perpLength = Math.Sqrt(dx * dx + dy * dy);
        var perpX = -dy / perpLength;
        var perpY = dx / perpLength;

        for (var i = 1; i <= steps; i++)
        {
            var t = (double)i / steps;
            var easedT = EaseInOutQuad(t);
            var curveOffset = curveAmplitude * 4 * t * (1 - t);
            var baseX = startX + (targetX - startX) * easedT;
            var baseY = startY + (targetY - startY) * easedT;
            var intermediateX = baseX + perpX * curveOffset;
            var intermediateY = baseY + perpY * curveOffset;
            (intermediateX, intermediateY) = ClampToScreen(intermediateX, intermediateY, screen);
            yield return (intermediateX, intermediateY);
        }
    }

    private static IEnumerable<(double X, double Y)> ShakyMoveAlgorithm(
        double startX,
        double startY,
        double targetX,
        double targetY,
        double distance,
        int steps,
        Rect screen,
        Random random
    )
    {
        steps = Math.Max(steps, 20);

        var maxShakiness = Math.Min(distance * 0.05, 10.0);

        for (var i = 1; i <= steps; i++)
        {
            var t = (double)i / steps;
            var easedT = EaseInOutQuad(t);
            var baseX = startX + (targetX - startX) * easedT;
            var baseY = startY + (targetY - startY) * easedT;
            var shakeAmplitude = maxShakiness * (1 - easedT);
            var shakeX = (random.NextDouble() * 2 - 1) * shakeAmplitude;
            var shakeY = (random.NextDouble() * 2 - 1) * shakeAmplitude;
            var intermediateX = baseX + shakeX;
            var intermediateY = baseY + shakeY;
            (intermediateX, intermediateY) = ClampToScreen(intermediateX, intermediateY, screen);
            yield return (intermediateX, intermediateY);
        }
    }

    private static IEnumerable<(double X, double Y)> BezierMoveAlgorithm(
        double startX,
        double startY,
        double targetX,
        double targetY,
        double distance,
        int steps,
        Rect screen,
        Random random
    )
    {
        steps = Math.Max(steps, 15);

        var dx = targetX - startX;
        var dy = targetY - startY;

        var control1DistanceAlongPath = 0.3 + random.NextDouble() * 0.2;
        var control1X = startX + dx * control1DistanceAlongPath;
        var control1Y = startY + dy * control1DistanceAlongPath;

        var perpOffset1 = (random.NextDouble() - 0.5) * distance * 0.4;
        control1X += -dy * perpOffset1 / distance;
        control1Y += dx * perpOffset1 / distance;

        var control2DistanceAlongPath = 0.6 + random.NextDouble() * 0.2;
        var control2X = startX + dx * control2DistanceAlongPath;
        var control2Y = startY + dy * control2DistanceAlongPath;

        var perpOffset2 = (random.NextDouble() - 0.5) * distance * 0.3;
        control2X += -dy * perpOffset2 / distance;
        control2Y += dx * perpOffset2 / distance;

        for (var i = 0; i <= steps; i++)
        {
            var t = (double)i / steps;
            var oneMinusT = 1 - t;
            var oneMinusTCubed = oneMinusT * oneMinusT * oneMinusT;
            var oneMinusTSquared = oneMinusT * oneMinusT;
            var tSquared = t * t;
            var tCubed = tSquared * t;

            var bx =
                oneMinusTCubed * startX
                + 3 * oneMinusTSquared * t * control1X
                + 3 * oneMinusT * tSquared * control2X
                + tCubed * targetX;

            var by =
                oneMinusTCubed * startY
                + 3 * oneMinusTSquared * t * control1Y
                + 3 * oneMinusT * tSquared * control2Y
                + tCubed * targetY;

            (bx, by) = ClampToScreen(bx, by, screen);
            yield return (bx, by);
        }
    }

    private static IEnumerable<(double X, double Y)> OvershootingMoveAlgorithm(
        double startX,
        double startY,
        double targetX,
        double targetY,
        double distance,
        int steps,
        Rect screen,
        Random random
    )
    {
        if (distance < 10)
        {
            foreach (
                var position in LinearMoveAlgorithm(startX, startY, targetX, targetY, distance, steps, screen, random)
            )
            {
                yield return position;
            }
            yield break;
        }

        var overshootFactor = 0.05 + random.NextDouble() * 0.1;

        var overshootX = targetX + (targetX - startX) * overshootFactor;
        var overshootY = targetY + (targetY - startY) * overshootFactor;

        (overshootX, overshootY) = ClampToScreen(overshootX, overshootY, screen);

        var timeToOvershootMs = (int)(steps * 0.7);
        var timeToTargetMs = steps - timeToOvershootMs;

        var distanceToOvershoot = CalculateDistance(startX, startY, overshootX, overshootY);
        var steps1 = Math.Min((int)(distanceToOvershoot / 5), 80);
        steps1 = Math.Max(steps1, 10);

        var dx1 = overshootX - startX;
        var dy1 = overshootY - startY;

        var perpLength1 = Math.Sqrt(dx1 * dx1 + dy1 * dy1);
        var perpX1 = -dy1 / perpLength1;
        var perpY1 = dx1 / perpLength1;

        var curveAmplitude1 = distanceToOvershoot * (0.02 + random.NextDouble() * 0.05);
        if (random.Next(2) == 0)
            curveAmplitude1 = -curveAmplitude1;

        for (var i = 1; i <= steps1; i++)
        {
            var t = (double)i / steps1;
            var easedT = EaseOutQuad(t);

            var curveOffset = curveAmplitude1 * 4 * t * (1 - t);

            var baseX = startX + (overshootX - startX) * easedT;
            var baseY = startY + (overshootY - startY) * easedT;

            var intermediateX = baseX + perpX1 * curveOffset;
            var intermediateY = baseY + perpY1 * curveOffset;

            (intermediateX, intermediateY) = ClampToScreen(intermediateX, intermediateY, screen);
            yield return (intermediateX, intermediateY);
        }

        var overshootPosition = MouseDevice.GetPosition();

        var distanceToTarget = CalculateDistance(overshootPosition.X, overshootPosition.Y, targetX, targetY);
        var steps2 = Math.Min((int)(distanceToTarget / 5), 40);
        steps2 = Math.Max(steps2, 5);

        for (var i = 1; i <= steps2; i++)
        {
            var t = (double)i / steps2;
            var easedT = EaseInQuad(t);

            var intermediateX = overshootPosition.X + (targetX - overshootPosition.X) * easedT;
            var intermediateY = overshootPosition.Y + (targetY - overshootPosition.Y) * easedT;

            (intermediateX, intermediateY) = ClampToScreen(intermediateX, intermediateY, screen);
            yield return (intermediateX, intermediateY);
        }
    }

    private static double EaseInOutQuad(double t)
    {
        return t < 0.5 ? 2 * t * t : 1 - Math.Pow(-2 * t + 2, 2) / 2;
    }

    private static double EaseInQuad(double t)
    {
        return t * t;
    }

    private static double EaseOutQuad(double t)
    {
        return 1 - (1 - t) * (1 - t);
    }

    public void Press(MouseButton? button = null, double? x = null, double? y = null, MouseOptions? options = null)
    {
        Move(x, y, options);

        MouseDevice.Press(button ?? DefaultButton);

        int delay = options?.AfterPressReleaseDelay ?? AfterPressReleaseDelay;
        if (delay > 0)
        {
            Thread.Sleep(delay);
        }
    }

    public void Release(MouseButton? button = null, double? x = null, double? y = null, MouseOptions? options = null)
    {
        Move(x, y, options);

        MouseDevice.Release(button ?? DefaultButton);

        int delay = options?.AfterPressReleaseDelay ?? AfterPressReleaseDelay;
        if (delay > 0)
        {
            Thread.Sleep(delay);
        }
    }

    DateTime? lastClickTime = null;
    Point? lastClickPosition = null;
    Rect? lastClickRect = null;
    MouseButton? lastClickButton = null;

    public void Click(
        MouseButton? button = null,
        double? x = null,
        double? y = null,
        int count = 1,
        MouseOptions? options = null
    )
    {
        var (x1, y1) = GetPoint(x, y);
        var currentPos = Move(x, y, options);

        lastClickPosition = currentPos;
        var doubleClickSize = GetDoubleClickSize();

        lastClickRect = new Rect(
            x1 - doubleClickSize.Width / 2,
            y1 - doubleClickSize.Height / 2,
            doubleClickSize.Width,
            doubleClickSize.Height
        );

        if (options?.EnsureClickPosition ?? EnsureClickPosition)
        {
            var ensureMoveThreshold = options?.EnsureMoveThreshold ?? EnsureMoveThreshold;
            if (ensureMoveThreshold < 0)
            {
                ensureMoveThreshold = 0;
            }

            if (Math.Abs(currentPos.X - x1) > ensureMoveThreshold || Math.Abs(currentPos.Y - y1) > ensureMoveThreshold)
            {
                throw new Exception(
                    $"Mouse position is not as expected. Expected: ({x}, {y}), Actual: ({currentPos.X}, {currentPos.Y})"
                );
            }
        }

        int clickDelay = options?.MultiClickDelay ?? MultiClickDelay;
        int releaseDelay = options?.AfterPressReleaseDelay ?? AfterPressReleaseDelay;
        int nextClickDelay = options?.BeforeNextClickDelay ?? BeforeNextClickDelay;

        for (var i = 0; i < count; i++)
        {
            if (i > 0 && nextClickDelay > 0)
            {
                Thread.Sleep(nextClickDelay);
            }

            MouseDevice.Press(button ?? DefaultButton);
            MouseDevice.Release(button ?? DefaultButton);

            if (i < count - 1 && clickDelay > 0)
            {
                Thread.Sleep(clickDelay);
            }
        }
        lastClickTime = DateTime.UtcNow;
        int afterClickDelay = options?.AfterClickDelay ?? AfterClickDelay;
        if (afterClickDelay > 0)
        {
            Thread.Sleep(afterClickDelay);
        }
    }
}
