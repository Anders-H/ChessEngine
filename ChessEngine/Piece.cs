using System;

namespace ChessEngine;

public class Piece : IFriendlyString
{
    public Color Color { get; }
    public Symbol Symbol { get; }
    public Move? LastMove { get; set; }
        
    public Piece(Color color, Symbol symbol)
    {
        Color = color;
        Symbol = symbol;
    }

    public Piece(string piece)
    {
        switch (piece)
        {
            case "P":
                Color = Color.White;
                Symbol = Symbol.Pawn;
                return;
            case "N":
                Color = Color.White;
                Symbol = Symbol.Knight;
                return;
            case "B":
                Color = Color.White;
                Symbol = Symbol.Bishop;
                return;
            case "R":
                Color = Color.White;
                Symbol = Symbol.Rook;
                return;
            case "Q":
                Color = Color.White;
                Symbol = Symbol.Queen;
                return;
            case "K":
                Color = Color.White;
                Symbol = Symbol.King;
                return;
            case "p":
                Color = Color.Black;
                Symbol = Symbol.Pawn;
                return;
            case "n":
                Color = Color.Black;
                Symbol = Symbol.Knight;
                return;
            case "b":
                Color = Color.Black;
                Symbol = Symbol.Bishop;
                return;
            case "r":
                Color = Color.Black;
                Symbol = Symbol.Rook;
                return;
            case "q":
                Color = Color.Black;
                Symbol = Symbol.Queen;
                return;
            case "k":
                Color = Color.Black;
                Symbol = Symbol.King;
                return;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
        
    public bool Compare(Color color, Symbol symbol) =>
        Color == color && Symbol == symbol;

    public bool IsUntouched =>
        LastMove == null;

    public float Value =>
        Symbol switch
        {
            Symbol.Pawn => 1.0f,
            Symbol.Knight => 3.0f,
            Symbol.Bishop => 3.5f,
            Symbol.Rook => 5.0f,
            Symbol.Queen => 9.0f,
            Symbol.King => 1000000.0f,
            _ => throw new Exception()
        };
        
    public override string ToString() =>
        Color switch
        {
            Color.White => (Symbol switch
            {
                Symbol.Pawn => "P",
                Symbol.Knight => "N",
                Symbol.Bishop => "B",
                Symbol.Rook => "R",
                Symbol.Queen => "Q",
                Symbol.King => "K",
                _ => throw new Exception()
            }),
            Color.Black => (Symbol switch
            {
                Symbol.Pawn => "p",
                Symbol.Knight => "n",
                Symbol.Bishop => "b",
                Symbol.Rook => "r",
                Symbol.Queen => "q",
                Symbol.King => "k",
                _ => throw new Exception()
            }),
            _ => throw new Exception()
        };

    public string ColorAsFriendlyString() =>
        Color switch
        {
            Color.White => "White",
            Color.Black => "Black",
            _ => throw new Exception()
        };

    public string SymbolAsFriendlyString() =>
        Symbol switch
        {
            Symbol.Pawn => "Pawn",
            Symbol.Knight => "Knight",
            Symbol.Bishop => "Bishop",
            Symbol.Rook => "Rook",
            Symbol.Queen => "Queen",
            Symbol.King => "King",
            _ => throw new SystemException()
        };
        
    public string ToFriendlyString() =>
        $"{ColorAsFriendlyString()} {SymbolAsFriendlyString().ToLower()}";
        
    public static implicit operator string(Piece p) =>
        p?.ToString() ?? "";
        
    public static implicit operator Piece(string s) =>
        new Piece(s);
}