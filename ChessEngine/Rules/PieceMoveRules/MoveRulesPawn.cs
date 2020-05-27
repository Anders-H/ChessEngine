namespace ChessEngine.Rules.PieceMoveRules
{
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
            var home = _piece.Color == Color.White
                ? 6
                : 1;
            
            var direction = _piece.Color == Color.White
                ? -1
                : 1;

            var moveList = new MoveList();
            
            if (_position.PhysicalY == home)
            {
                var doubleStep = _position.PhysicalY + direction + direction;
                if (_board.PositionIsFree(_position.PhysicalX, doubleStep))
                    moveList.AddMove(_piece, _position, _position.PhysicalX, doubleStep);
            }

            var oneStep = _position.PhysicalY + direction;
            if (oneStep >= 0 && oneStep < 8 && _board.PositionIsFree(_position.PhysicalX, oneStep))
                moveList.AddMove(_piece, _position, _position.PhysicalX, oneStep);

            var x = _position.PhysicalX - 1;
            if (x >= 0 && x < 8 && !_board.PositionIsFree(x, oneStep))
                moveList.AddMove(_piece, _position, x, oneStep);

            x = _position.PhysicalX + 1;
            if (x >= 0 && x < 8 && !_board.PositionIsFree(x, oneStep))
                moveList.AddMove(_piece, _position, x, oneStep);

            //TODO: En passant.
            
            return moveList;
        }
    }
}