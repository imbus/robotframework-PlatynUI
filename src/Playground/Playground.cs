using PlatynUI.Runtime;

var r = DisplayDevice.GetBoundingRectangle();

DisplayDevice.HighlightRect(r.X, r.Y, 500, 500);

var rectWidth = 50;
var rectHeight = 50;

for (var i = r.X; i < r.X + r.Width - rectWidth; i++)
{
    DisplayDevice.HighlightRect(i, r.Y, rectWidth, rectHeight);

    Thread.Sleep(1);
}

for (var i = r.Y; i < r.Y + r.Height; i++)
{
    DisplayDevice.HighlightRect(r.X + r.Width - rectWidth, i, rectWidth, rectHeight);

    Thread.Sleep(1);
}

for (var i = r.X + r.Width - rectWidth; i > r.X; i--)
{
    DisplayDevice.HighlightRect(i, r.Y + r.Height - rectHeight, rectWidth, rectHeight);

    Thread.Sleep(1);
}

for (var i = r.Y + r.Height - rectHeight; i > r.Y; i--)
{
    DisplayDevice.HighlightRect(r.X, i, rectWidth, rectHeight);

    Thread.Sleep(1);
}

Thread.Sleep(5000);
Console.WriteLine("Fertig");
