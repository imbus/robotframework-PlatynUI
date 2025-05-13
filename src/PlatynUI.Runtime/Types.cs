// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

namespace PlatynUI.Runtime;

public struct Point(double x, double y)
{
    public double X { get; set; } = x;
    public double Y { get; set; } = y;

    public override readonly string ToString() => $"({X}, {Y})";

    public static bool operator ==(Point obj1, Point obj2)
    {
        return obj1.X == obj2.X && obj1.Y == obj2.Y;
    }

    // this is second one '!='
    public static bool operator !=(Point obj1, Point obj2)
    {
        return !(obj1.X == obj2.X && obj1.Y == obj2.Y);
    }

    // this is third one 'Equals'
    public override readonly bool Equals(object? obj)
    {
        return obj switch
        {
            Point p => this == p,
            _ => false,
        };
    }

    public override readonly int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }

    public static readonly Point Empty = new(0, 0);
}

public struct Size(double width, double height)
{
    public double Width { get; set; } = width;
    public double Height { get; set; } = height;

    public override readonly string ToString() => $"({Width}, {Height})";

    public static bool operator ==(Size obj1, Size obj2)
    {
        return obj1.Width == obj2.Width && obj1.Height == obj2.Height;
    }

    // this is second one '!='
    public static bool operator !=(Size obj1, Size obj2)
    {
        return !(obj1.Width == obj2.Width && obj1.Height == obj2.Height);
    }

    // this is third one 'Equals'
    public override readonly bool Equals(object? obj)
    {
        return obj switch
        {
            Size p => this == p,
            _ => false,
        };
    }

    public override readonly int GetHashCode()
    {
        return Width.GetHashCode() ^ Height.GetHashCode();
    }
}

public struct Rect(double x, double y, double width, double height)
{
    public double X { get; set; } = x;
    public double Y { get; set; } = y;
    public double Width { get; set; } = width;
    public double Height { get; set; } = height;

    public readonly double Left => X;
    public readonly double Right => X + Width;
    public readonly double Top => Y;
    public readonly double Bottom => Y + Height;

    public readonly Point TopLeft => new(X, Y);
    public readonly Point BottomRight => new(X + Width - 1, Y + Height - 1);
    public readonly Point BottomLeft => new(X, Y + Height - 1);
    public readonly Point TopRight => new(X + Width - 1, Y);
    public readonly Point Center => new(X + Width / 2, Y + Height / 2);

    public Size Size
    {
        readonly get => new(Width, Height);
        set
        {
            Width = value.Width;
            Height = value.Height;
        }
    }

    public override readonly string ToString() => $"({X}, {Y}, {Width}, {Height})";

    public static bool operator !=(Rect rect1, Rect rect2)
    {
        return !(
            rect1.X == rect2.X && rect1.Y == rect2.Y && rect1.Width == rect2.Width && rect1.Height == rect2.Height
        );
    }

    public static bool operator ==(Rect rect1, Rect rect2)
    {
        return rect1.X == rect2.X && rect1.Y == rect2.Y && rect1.Width == rect2.Width && rect1.Height == rect2.Height;
    }

    public readonly bool Equals(Rect value)
    {
        return X == value.X && Y == value.Y && Width == value.Width && Height == value.Height;
    }

    public override readonly bool Equals(object? o)
    {
        if (o is not Rect)
            return false;

        return Equals((Rect)o);
    }

    public override readonly int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
    }

    public static bool Equals(Rect rect1, Rect rect2)
    {
        return rect1.Equals(rect2);
    }

    public readonly bool Contains(Point point)
    {
        return point.X >= X && point.X <= X + Width - 1 && point.Y >= Y && point.Y <= Y + Height - 1;
    }

    public readonly bool Contains(Rect other)
    {
        return X <= other.X
            && Y <= other.Y
            && X + Width >= other.X + other.Width
            && Y + Height >= other.Y + other.Height;
    }

    public static readonly Rect Empty = new(0, 0, 0, 0);
}
