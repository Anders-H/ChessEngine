namespace ChessEngine.Extensions;

public static class IntegerExtensions
{
    public static bool IsOnBoard(this int i) =>
        i is >= 0 and < 8;
}