using System;
using System.Drawing;
using ChessEngine;
using ChessGraphicsRenderer.Properties;
using Color = System.Drawing.Color;

namespace ChessGraphicsRenderer;

public class Renderer
{
    private Size _boardSize;
    private Size _tileSize;
    private readonly PhysicalPositionList _physicalPositions;
    public PhysicalPosition? SelectedTile { get; set; }
    public Point BoardPosition { get; set; }

    public Renderer(Size boardSize, Point boardPosition)
    {
        BoardSize = boardSize;
        BoardPosition = boardPosition;
        _physicalPositions = new PhysicalPositionList();
    }

    public PhysicalPosition? GetTileAt(int pixelX, int pixelY) =>
        _physicalPositions.GetTileAt(pixelX, pixelY);

    public bool GetTileWithPieceIsSelected(Board board)
    {
        if (SelectedTile == null)
            return false;
        return board.GetPieceUsingPhysicalCoordinates(SelectedTile.PhysicalX,
            SelectedTile.PhysicalY) != null;
    }

    public Size BoardSize
    {
        get => _boardSize;
        set
        {
            _boardSize = value;
            RecalcSizes();
        }
    }

    private void RecalcSizes()
    {
        var squareWidth = BoardSize.Width / 8;
        var squareHeight = BoardSize.Height / 8;
        _tileSize = new Size(squareWidth, squareHeight);
        _boardSize = new Size(_boardSize.Width * 8, _boardSize.Height * 8);
        SelectedTile?.RecalcPosition(BoardPosition, _tileSize);
    }
        
    public void Draw(Graphics g, Font font, Board board, MoveList possibleMoves)
    {
        _physicalPositions.Clear();
        g.Clear(Color.Teal);

        var characterSize = g.MeasureString("8", font);
            
        var xpos = BoardPosition.X;
        var ypos = BoardPosition.Y;
        var firstIsWhite = false;
        var whiteTileBrush = new SolidBrush(Color.FromArgb(255, 206, 158));
        var blackTileBrush = new SolidBrush(Color.FromArgb(209, 139, 71));
        for (var y = 0; y < 8; y++)
        {
            firstIsWhite = !firstIsWhite;
            var currentIsWhite = firstIsWhite;
            for (var x = 0; x < 8; x++)
            {
                _physicalPositions.Add(
                    new PhysicalPosition(
                        x,
                        y,
                        new Rectangle(xpos, ypos, _tileSize.Width, _tileSize.Height)
                    )
                );
                    
                var b = currentIsWhite ? whiteTileBrush : blackTileBrush;
                g.FillRectangle(b, xpos, ypos, _tileSize.Width, _tileSize.Height);

                var piece = board.GetPieceUsingPhysicalCoordinates(x, y);
                if (piece != null)
                    DrawPiece(g, xpos, ypos, piece);

                var move = possibleMoves?.GetMoveWithTargetAt(x, y);
                if (move != null)
                {
                    g.DrawRectangle(
                        Pens.Yellow,
                        xpos + 3,
                        ypos + 3,
                        _tileSize.Width - 6,
                        _tileSize.Height - 6
                    );
                    g.DrawRectangle(
                        Pens.Yellow,
                        xpos + 4,
                        ypos + 4,
                        _tileSize.Width - 8,
                        _tileSize.Height - 8
                    );
                }

                DrawTileLabels(g, font, y, x, xpos, ypos, characterSize);
                    
                currentIsWhite = !currentIsWhite;
                xpos += _tileSize.Width;
            }
            ypos += _tileSize.Height;
            xpos = BoardPosition.X;
        }
            
        DrawSelection(g);
    }

    private void DrawPiece(Graphics g, int xpos, int ypos, Piece piece)
    {
        var pieceX = xpos + 3;
        var pieceY = ypos + 3;
        var pieceWidth = _tileSize.Width - 6;
        var pieceHeight = _tileSize.Height - 6;
        var bitmap = SelectBitmap(piece);
        if (bitmap != null)
            g.DrawImage(bitmap, pieceX, pieceY, pieceWidth, pieceHeight);
    }

    private static Bitmap? SelectBitmap(Piece piece)
    {
        switch (piece.Color)
        {
            case ChessEngine.Color.White:
                switch (piece.Symbol)
                {
                    case Symbol.Pawn:
                        return Resources.white_pawn;
                    case Symbol.Knight:
                        return Resources.white_knight;
                    case Symbol.Bishop:
                        return Resources.white_bishop;
                    case Symbol.Rook:
                        return Resources.white_rook;
                    case Symbol.Queen:
                        return Resources.white_queen;
                    case Symbol.King:
                        return Resources.white_king;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case ChessEngine.Color.Black:
                switch (piece.Symbol)
                {
                    case Symbol.Pawn:
                        return Resources.black_pawn;
                    case Symbol.Knight:
                        return Resources.black_knight;
                    case Symbol.Bishop:
                        return Resources.black_bishop;
                    case Symbol.Rook:
                        return Resources.black_rook;
                    case Symbol.Queen:
                        return Resources.black_queen;
                    case Symbol.King:
                        return Resources.black_king;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            default:
                return null;
        }
    }

    private void DrawTileLabels(Graphics g, Font font, int y, int x, int xpos, int ypos, SizeF characterSize)
    {
        if (y is 0 or 7)
        {
            switch (x)
            {
                case 0:
                    g.DrawString(y == 0 ? "a8" : "a1", font, Brushes.Black, xpos + 2,
                        ypos + _tileSize.Height - characterSize.Height - 2);
                    break;
                case 7:
                    g.DrawString(y == 0 ? "h8" : "h1", font, Brushes.Black, xpos + 2,
                        ypos + _tileSize.Height - characterSize.Height - 2);
                    break;
                default:
                    g.DrawString("abcdefgh".Substring(x, 1), font, Brushes.Black, xpos + 2,
                        ypos + _tileSize.Height - characterSize.Height - 2);
                    break;
            }
        }
        else if (x == 0)
        {
            g.DrawString("87654321".Substring(y, 1), font, Brushes.Black, xpos + 2,
                ypos + _tileSize.Height - characterSize.Height - 2);
        }
    }

    private void DrawSelection(Graphics g)
    {
        if (SelectedTile != null)
        {
            var x = SelectedTile.Location.Left;
            var y = SelectedTile.Location.Top;
            var w = _tileSize.Width;
            var h = _tileSize.Height;
            g.DrawRectangle(Pens.Blue, x - 1, y - 1, w + 2, h + 2);
            g.DrawRectangle(Pens.Blue, x, y, w, h);
            g.DrawRectangle(Pens.Blue, x + 1, y + 1, w - 2, h - 2);
        }
    }
}