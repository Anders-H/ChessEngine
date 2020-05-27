using ChessEngine;
using Xunit;

namespace ChessEngineTests
{
    public class BoardTests
    {
        [Fact]
        public void CanSetPieceUsingLogicalCoordinates()
        {
            var board = new Board();
            var pawn = new Piece("P");
            var knight = new Piece("N");

            board.SetPieceUsingLogicalCoordinates(1, 1, pawn);
            board.SetPieceUsingLogicalCoordinates(8, 8, knight);
            Assert.True(board.GetPieceUsingPhysicalCoordinates(0, 7).ToString() == "P");
            Assert.True(board.GetPieceUsingPhysicalCoordinates(7, 0).ToString() == "N");

            board.Clear();
            
            board.SetPieceUsingPhysicalCoordinates(0, 7, pawn);
            board.SetPieceUsingPhysicalCoordinates(7, 0, knight);
            Assert.True(board.GetPieceUsingLogicalCoordinates(1, 1).ToString() == "P");
            Assert.True(board.GetPieceUsingLogicalCoordinates(8, 8).ToString() == "N");
        }
        
        [Fact]
        public void CanSetPieceUsingPositionName()
        {
            var board = new Board();
            var pawn = new Piece("P");
            var knight = new Piece("N");

            board.SetPieceUsingPositionName("a1", pawn);
            board.SetPieceUsingPositionName("h8", knight);
            Assert.True(board.GetPieceUsingPhysicalCoordinates(0, 7).ToString() == "P");
            Assert.True(board.GetPieceUsingPhysicalCoordinates(7, 0).ToString() == "N");

            board.Clear();
            
            board.SetPieceUsingPhysicalCoordinates(0, 7, pawn);
            board.SetPieceUsingPhysicalCoordinates(7, 0, knight);
            Assert.True(board.GetPieceUsingPositionName("a1").ToString() == "P");
            Assert.True(board.GetPieceUsingPositionName("h8").ToString() == "N");
        }
        
        [Fact]
        public void CanSerialize()
        {
            const string expectedSerialized = "rnbqkbnrpppppppp................................PPPPPPPPRNBQKBNR";
            var board = new Board();
            board.Reset();
            var serialized = board.ToString();
            Assert.True(serialized == expectedSerialized);
        }
    }
}