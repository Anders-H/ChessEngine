using ChessEngine.Extensions;

namespace ChessEngine.Rules.PieceMoveRules;

public class MoveRulesPawn : MoveRules
{
    private readonly Board _board;
    private readonly Piece _piece;
    private readonly Position _position;
        
    public MoveRulesPawn(Board board, Piece piece, Position position)
    {
        _board = board;
        _piece = piece;
        _position = position;
    }

    public override MoveList GetMoves()
    {
        var homeY = _piece.Color == Color.White
            ? 6
            : 1;
            
        var directionY = _piece.Color == Color.White
            ? -1
            : 1;

        var moveList = new MoveList();
            
        AddInitialDoubleStepMove(homeY, directionY, ref moveList);

        AddSingleStepMove(directionY, ref moveList);
            
        AddCapture(-1, directionY, ref moveList);
            
        AddCapture(1, directionY, ref moveList);

        AddEnPassant(-1, directionY, ref moveList);
            
        AddEnPassant(1, directionY, ref moveList);
            
        return moveList;
    }

    private void AddInitialDoubleStepMove(int homeY, int directionY, ref MoveList moveList)
    {
        if (_position.PhysicalY != homeY)
            return;
            
        var doubleStep = _position.PhysicalY + directionY + directionY;
            
        if (_board.PositionIsFree(_position.PhysicalX, doubleStep))
            moveList.AddMove(_piece, _position, _position.PhysicalX, doubleStep);
    }

    private void AddSingleStepMove(int directionY, ref MoveList moveList)
    {
        var oneStep = _position.PhysicalY + directionY;
            
        if (oneStep.IsOnBoard() && _board.PositionIsFree(_position.PhysicalX, oneStep))
            moveList.AddMove(_piece, _position, _position.PhysicalX, oneStep);
    }

    private void AddCapture(int directionX, int directionY, ref MoveList moveList)
    {
        var oneStep = _position.PhysicalY + directionY;

        var side = _position.PhysicalX + directionX;
            
        if (side.IsOnBoard() && !_board.PositionIsFree(side, oneStep))
            moveList.AddMove(_piece, _position, side, oneStep);
    }

    private void AddEnPassant(int directionX, int directionY, ref MoveList moveList)
    {
        var neighbourX = _position.PhysicalX + directionX;
            
        if (!neighbourX.IsOnBoard())
            return;
            
        var neighbourPiece = _board.GetPieceUsingPhysicalCoordinates(directionX, _position.PhysicalY);

        if (neighbourPiece == null)
            return;

        if (neighbourPiece.Color == _piece.Color)
            return;

        var targetY = _position.PhysicalY + directionY;

        if (!targetY.IsOnBoard())
            return;
            
        //TODO
    }
}