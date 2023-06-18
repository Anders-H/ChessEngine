using System;

namespace ChessEngine.Rules;

public class PositionRules
{
    private readonly Board _board;

    public PositionRules(Board board)
    {
        _board = board;
    }

    public PositionDescription GetPositionDescription(Position position)
    {
        var piece = _board.GetPieceUsingPhysicalCoordinates(position);
            
        if (piece == null)
            return PositionDescription.Other;
            
        if (!CouldBeHomePosition(piece, position))
            return PositionDescription.Other;

        return piece.LastMove == null
            ? PositionDescription.HomeUnmoved
            : PositionDescription.HomeMoved;
    }

    public static bool CouldBeHomePosition(Piece piece, Position position)
    {
        switch (piece.Color)
        {
            case Color.White:
                switch (piece.Symbol)
                {
                    case Symbol.Pawn:
                        return position.PhysicalY == 6;
                    case Symbol.Knight:
                        return position.PhysicalY == 7 && (position.PhysicalX == 1 || position.PhysicalX == 6);
                    case Symbol.Bishop:
                        return position.PhysicalY == 7 && (position.PhysicalX == 2 || position.PhysicalX == 5);
                    case Symbol.Rook:
                        return position.PhysicalY == 7 && (position.PhysicalX == 0 || position.PhysicalX == 7);
                    case Symbol.Queen:
                        return position.PhysicalY == 7 && position.PhysicalX == 3;
                    case Symbol.King:
                        return position.PhysicalY == 7 && position.PhysicalX == 4;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case Color.Black:
                switch (piece.Symbol)
                {
                    case Symbol.Pawn:
                        return position.PhysicalY == 1;
                    case Symbol.Knight:
                        return position.PhysicalY == 0 && (position.PhysicalX == 1 || position.PhysicalX == 6);
                    case Symbol.Bishop:
                        return position.PhysicalY == 0 && (position.PhysicalX == 2 || position.PhysicalX == 5);
                    case Symbol.Rook:
                        return position.PhysicalY == 0 && (position.PhysicalX == 0 || position.PhysicalX == 7);
                    case Symbol.Queen:
                        return position.PhysicalY == 0 && position.PhysicalX == 3;
                    case Symbol.King:
                        return position.PhysicalY == 0 && position.PhysicalX == 4;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}