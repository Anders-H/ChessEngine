using System.Drawing;
using ChessEngine;

namespace ChessGraphicsRenderer;

public class PhysicalPosition : Position
{
    public Rectangle Location { get; private set; }

    public PhysicalPosition(int physicalX, int physicalY, Rectangle location) : base(physicalX, physicalY)
    {
        Location = location;
    }
        
    public PhysicalPosition(string name, Rectangle location) : base(name)
    {
        Location = location;
    }

    public void RecalcPosition(Point boardPosition, Size tileSize)
    {
        var x = tileSize.Width * PhysicalX + boardPosition.X;
        var y = tileSize.Height * PhysicalY + boardPosition.Y;
        Location = new Rectangle(x, y, tileSize.Width, tileSize.Height);
    }
        
    public bool HitTest(int x, int y) =>
        x >= Location.Left
        && x < Location.Left + Location.Width
        && y >= Location.Top
        && y < Location.Top + Location.Height;

    public bool IsSameLocationAs(PhysicalPosition? other)
    {
        if (other == null)
            return false;

        return PhysicalX == other.PhysicalX
               && PhysicalY == other.PhysicalY;
    }
}