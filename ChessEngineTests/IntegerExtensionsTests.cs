using ChessEngine.Extensions;
using Xunit;

namespace ChessEngineTests;

public class IntegerExtensionsTests
{
    [Fact]
    public void CanDetermineOnBoard()
    {
        Assert.False((-2).IsOnBoard());
        Assert.False((-1).IsOnBoard());
        Assert.True(0.IsOnBoard());
        Assert.True(1.IsOnBoard());
        Assert.True(2.IsOnBoard());
        Assert.True(3.IsOnBoard());
        Assert.True(4.IsOnBoard());
        Assert.True(5.IsOnBoard());
        Assert.True(6.IsOnBoard());
        Assert.True(7.IsOnBoard());
        Assert.False(8.IsOnBoard());
        Assert.False(9.IsOnBoard());
    }
}