using System.Collections.Generic;
using System.Linq;

namespace ChessEngine
{
    public class MoveList : List<Move>
    {
        public Move? GetMoveWithTargetAt(int physicalX, int physicalY) =>
            this.FirstOrDefault(x => x.To?.PhysicalX == physicalX && x.To?.PhysicalY == physicalY);

        public void AddMove(Piece piece, Position position, int targetPhysicalX, int targetPhysicalY) =>
            Add(new Move(piece, position, targetPhysicalX, targetPhysicalY));
    }
}