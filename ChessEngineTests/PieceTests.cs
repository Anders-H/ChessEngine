using ChessEngine;
using Xunit;

namespace ChessEngineTests;

public class PieceTests
{
    [Fact]
    public void CanSerialize()
    {
        Assert.True(new Piece(Color.White, Symbol.Pawn).ToString() == "P");
        Assert.True(new Piece(Color.White, Symbol.Knight).ToString() == "N");
        Assert.True(new Piece(Color.White, Symbol.Bishop).ToString() == "B");
        Assert.True(new Piece(Color.White, Symbol.Rook).ToString() == "R");
        Assert.True(new Piece(Color.White, Symbol.Queen).ToString() == "Q");
        Assert.True(new Piece(Color.White, Symbol.King).ToString() == "K");
        Assert.True(new Piece(Color.Black, Symbol.Pawn).ToString() == "p");
        Assert.True(new Piece(Color.Black, Symbol.Knight).ToString() == "n");
        Assert.True(new Piece(Color.Black, Symbol.Bishop).ToString() == "b");
        Assert.True(new Piece(Color.Black, Symbol.Rook).ToString() == "r");
        Assert.True(new Piece(Color.Black, Symbol.Queen).ToString() == "q");
        Assert.True(new Piece(Color.Black, Symbol.King).ToString() == "k");
    }

    [Fact]
    public void CanParse()
    {
        Assert.True(new Piece("P").Compare(Color.White, Symbol.Pawn));
        Assert.True(new Piece("N").Compare(Color.White, Symbol.Knight));
        Assert.True(new Piece("B").Compare(Color.White, Symbol.Bishop));
        Assert.True(new Piece("R").Compare(Color.White, Symbol.Rook));
        Assert.True(new Piece("Q").Compare(Color.White, Symbol.Queen));
        Assert.True(new Piece("K").Compare(Color.White, Symbol.King));
        Assert.True(new Piece("p").Compare(Color.Black, Symbol.Pawn));
        Assert.True(new Piece("n").Compare(Color.Black, Symbol.Knight));
        Assert.True(new Piece("b").Compare(Color.Black, Symbol.Bishop));
        Assert.True(new Piece("r").Compare(Color.Black, Symbol.Rook));
        Assert.True(new Piece("q").Compare(Color.Black, Symbol.Queen));
        Assert.True(new Piece("k").Compare(Color.Black, Symbol.King));
    }
}