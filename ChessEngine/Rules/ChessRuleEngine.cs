using System;
using ChessEngine.Rules.PieceMoveRules;

namespace ChessEngine.Rules;

public class ChessRuleEngine
{
    private readonly Board _board;

    public ChessRuleEngine(Board board)
    {
        _board = board;
    }

    public MoveList? GetPossibleMoves(Position? position)
    {
        if (position == null)
            return null;

        var piece = _board.GetPieceUsingPhysicalCoordinates(position);
        if (piece == null)
            return null;

        var moveRules = GetMoveRules(piece, position);
            
        return moveRules?.GetMoves();
    }

    private MoveRules? GetMoveRules(Piece piece, Position position) =>
        piece.Symbol switch
        {
            Symbol.Pawn => new MoveRulesPawn(_board, piece, position),
            Symbol.Knight => null,
            Symbol.Bishop => null,
            Symbol.Rook => null,
            Symbol.Queen => null,
            Symbol.King => null,
            _ => throw new ArgumentOutOfRangeException()
        };
}