using System;
using System.Drawing;

namespace ChessEngine
{
    public class Move : IFriendlyString
    {
        public int MoveNumber { get; set; }
        public Piece Piece { get; }
        public Position From { get; }
        public Position To { get; }

        public Piece? SecondaryPiece { get; set; }
        public Position? SecondaryFrom { get; set; }
        public Position? SecondaryTo { get; set; }
        
        public SpecialMove SpecialMove { get; set; }
        
        public Move(Piece piece, Position from, Position to)
        {
            SpecialMove = SpecialMove.None;
            Piece = piece;
            From = from;
            To = to;
        }

        public Move(Piece piece, Position from, int toPhysicalX, int toPhysicalY)
        {
            SpecialMove = SpecialMove.None;
            Piece = piece;
            From = from;
            To = new Position(toPhysicalX, toPhysicalY);
        }
        
        public Move(Piece piece, Point physicalFrom, Point physicalTo)
        {
            SpecialMove = SpecialMove.None;
            Piece = piece;
            From = new Position(physicalFrom.X, physicalFrom.Y);
            To = new Position(physicalTo.X, physicalTo.Y);
        }

        public override string ToString() =>
            SpecialMove switch
            {
                SpecialMove.None => $"{(MoveNumber > 0 ? $"M{MoveNumber}: " : "")}{Piece} {From}-{To}",
                SpecialMove.CastlingShort => $"{(MoveNumber > 0 ? $"M{MoveNumber}: " : "")}{Piece} {From}-{To}, {SecondaryPiece} {SecondaryFrom}-{SecondaryTo} (castling short)",
                SpecialMove.CastlingLong => $"{(MoveNumber > 0 ? $"M{MoveNumber}: " : "")}{Piece} {From}-{To}, {SecondaryPiece} {SecondaryFrom}-{SecondaryTo} (castling long)",
                SpecialMove.EnPassant => $"{(MoveNumber > 0 ? $"M{MoveNumber}: " : "")}{Piece} {From}-{To} (en passant)",
                SpecialMove.Promotion => $"{(MoveNumber > 0 ? $"M{MoveNumber}: " : "")}{Piece} {From}-{To} (upgraded from {SecondaryPiece})",
                _ => throw new ArgumentOutOfRangeException()
            };

        public string ToFriendlyString() =>
            SpecialMove switch
            {
                SpecialMove.None => $"{(MoveNumber > 0 ? $"Move {MoveNumber}: " : "")}{Piece.ToFriendlyString()} {From}-{To}",
                SpecialMove.CastlingShort => $"{(MoveNumber > 0 ? $"Move {MoveNumber}: " : "")}{Piece.ToFriendlyString()} {From}-{To}, {SecondaryPiece!.ToFriendlyString()} {SecondaryFrom}-{SecondaryTo} (castling short)",
                SpecialMove.CastlingLong => $"{(MoveNumber > 0 ? $"Move {MoveNumber}: " : "")}{Piece.ToFriendlyString()} {From}-{To}, {SecondaryPiece!.ToFriendlyString()} {SecondaryFrom}-{SecondaryTo} (castling long)",
                SpecialMove.EnPassant => $"{(MoveNumber > 0 ? $"Move {MoveNumber}: " : "")}{Piece.ToFriendlyString()} {From}-{To} (en passant)",
                SpecialMove.Promotion => $"{(MoveNumber > 0 ? $"Move {MoveNumber}: " : "")}{Piece.ToFriendlyString()} {From}-{To} (upgraded from {SecondaryPiece!.ToFriendlyString()})",
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}