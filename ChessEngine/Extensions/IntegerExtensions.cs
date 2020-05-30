namespace ChessEngine.Extensions
{
    public static class IntegerExtensions
    {
        public static bool IsOnBoard(this int i) =>
            i >= 0 && i < 8;
    }
}