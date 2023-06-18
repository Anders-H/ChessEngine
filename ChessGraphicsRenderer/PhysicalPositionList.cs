using System.Collections.Generic;
using System.Linq;

namespace ChessGraphicsRenderer;

public class PhysicalPositionList : List<PhysicalPosition>
{
    public PhysicalPosition? GetTileAt(int x, int y) =>
        this.FirstOrDefault(p => p.HitTest(x, y));
}