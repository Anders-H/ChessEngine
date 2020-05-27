using System;
using System.Text;

namespace ChessEngine
{
    public class Board
    {
        private readonly Piece?[,] _pieces;
        
        public Board()
        {
            _pieces = new Piece[8, 8];
        }

        public void Clear()
        {
            for (var y = 0; y < 8; y++)
                for (var x = 0; x < 8; x++)
                    _pieces[x, y] = null;
        }

        public void Reset()
        {
            Clear();
            SetPieceUsingPhysicalCoordinates(0, 0, "r");
            SetPieceUsingPhysicalCoordinates(1, 0, "n");
            SetPieceUsingPhysicalCoordinates(2, 0, "b");
            SetPieceUsingPhysicalCoordinates(3, 0, "q");
            SetPieceUsingPhysicalCoordinates(4, 0, "k");
            SetPieceUsingPhysicalCoordinates(5, 0, "b");
            SetPieceUsingPhysicalCoordinates(6, 0, "n");
            SetPieceUsingPhysicalCoordinates(7, 0, "r");
            for (var x = 0; x < 8; x++)
            {
                SetPieceUsingPhysicalCoordinates(x, 1, "p");
                SetPieceUsingPhysicalCoordinates(x, 6, "P");
            }
            SetPieceUsingPhysicalCoordinates(0, 7, "R");
            SetPieceUsingPhysicalCoordinates(1, 7, "N");
            SetPieceUsingPhysicalCoordinates(2, 7, "B");
            SetPieceUsingPhysicalCoordinates(3, 7, "Q");
            SetPieceUsingPhysicalCoordinates(4, 7, "K");
            SetPieceUsingPhysicalCoordinates(5, 7, "B");
            SetPieceUsingPhysicalCoordinates(6, 7, "N");
            SetPieceUsingPhysicalCoordinates(7, 7, "R");
        }

        public void SetPieceUsingPhysicalCoordinates(Position position, Piece? piece)
        {
            if (position == null)
                return;
            SetPieceUsingPhysicalCoordinates(position.PhysicalX, position.PhysicalY, piece);
        }

        public void SetPieceUsingPhysicalCoordinates(int x, int y, Piece? piece)
        {
            if (x < 0 || x > 7)
                throw new ArgumentOutOfRangeException();
            if (y < 0 || y > 7)
                throw new ArgumentOutOfRangeException();
            if (piece == null)
                _pieces[x, y] = null;
            else
                _pieces[x, y] = piece;
        }

        public Piece? GetPieceUsingPhysicalCoordinates(Position position) =>
            position == null
                ? null
                : GetPieceUsingPhysicalCoordinates(position.PhysicalX, position.PhysicalY);
        
        public Piece? GetPieceUsingPhysicalCoordinates(int x, int y)
        {
            if (x < 0 || x > 7)
                throw new ArgumentOutOfRangeException();
            if (y < 0 || y > 7)
                throw new ArgumentOutOfRangeException();
            return _pieces[x, y];
        }

        public void SetPieceUsingLogicalCoordinates(int x, int y, Piece? piece)
        {
            if (x < 1 || x > 8)
                throw new ArgumentOutOfRangeException();
            if (y < 1 || y > 8)
                throw new ArgumentOutOfRangeException();
            SetPieceUsingPhysicalCoordinates(x - 1, 8 - y, piece);
        }

        public Piece? GetPieceUsingLogicalCoordinates(int x, int y)
        {
            if (x < 1 || x > 8)
                throw new ArgumentOutOfRangeException();
            if (y < 1 || y > 8)
                throw new ArgumentOutOfRangeException();
            return GetPieceUsingPhysicalCoordinates(x - 1, 8 - y);
        }

        public override string ToString()
        {
            var s = new StringBuilder();
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var piece = GetPieceUsingPhysicalCoordinates(x, y);
                    s.Append(piece == null ? "." : piece.ToString());
                }
            }
            return s.ToString();
        }

        public bool PositionIsFree(int physicalX, int physicalY) =>
            _pieces[physicalX, physicalY] == null;
        
        public void SetPieceUsingPositionName(Position position, Piece? piece) =>
            SetPieceUsingPhysicalCoordinates(position.PhysicalX, position.PhysicalY, piece);

        public Piece? GetPieceUsingPositionName(Position position) =>
            GetPieceUsingPhysicalCoordinates(position.PhysicalX, position.PhysicalY);
    }
}