using System;
using System.Drawing;
using System.Windows.Forms;
using ChessEngine;
using ChessEngine.Rules;
using ChessGraphicsRenderer;

namespace ChessBoardAnalyzer;

public partial class MainWindow : Form
{
    private readonly Board _board;
    private readonly Renderer _renderer;
    private readonly ContextMenuStrip _contextMenuStrip;
    private int _mouseX;
    private int _mouseY;
    private ToolStripMenuItem _selectPiece;
    private ToolStripMenuItem _clearSelectionMenuItem;
    private ToolStripMenuItem _addPiece;
    private ToolStripMenuItem _editPiece;
    private ToolStripMenuItem _removePiece;
    private MoveList _possibleMoves;
        
    public MainWindow()
    {
        InitializeComponent();
        _board = new Board();
        _renderer = new Renderer(Size, new Point(0, 0));
        Load += MainWindowLoad;
        Resize += MainWindowResize;
        Paint += MainWindowPaint;
        MouseDown += CollectMousePosition;
        _contextMenuStrip = new ContextMenuStrip();
        _contextMenuStrip.Opening += MenuOpening;
        CreateContextMenuItems();
    }

    private void CollectMousePosition(object sender, MouseEventArgs e)
    {
        _mouseX = e.X;
        _mouseY = e.Y;
    }
        
    private void CreateContextMenuItems()
    {
        var menuBuilder = new MenuBuilder(_contextMenuStrip);
            
        _addPiece = menuBuilder.CreateItem("Add piece", (sender, e) => AddPiece());
        _editPiece = menuBuilder.CreateItem("Edit piece", (sender, e) => EditPiece());
        _removePiece = menuBuilder.CreateItem("Remove piece", (sender, e) => RemovePiece());
        menuBuilder.CreateSeparator();
        _selectPiece = menuBuilder.CreateItem("Select tile", (sender, e) => SelectTileOrPiece());  //Select tile / select piece.
        _clearSelectionMenuItem = menuBuilder.CreateItem("Clear selection", (sender, e) =>
        {
            if (_renderer.SelectedTile == null)
                return;
                
            _renderer.SelectedTile = null;
            _possibleMoves = null;
            Invalidate();
        });
    }
        
    private void MenuOpening(object sender, EventArgs e)
    {
        _clearSelectionMenuItem.Enabled = _renderer.SelectedTile != null;
        _addPiece.Enabled = _renderer.SelectedTile != null && !_renderer.GetTileWithPieceIsSelected(_board);
        _editPiece.Enabled = _renderer.SelectedTile != null && _renderer.GetTileWithPieceIsSelected(_board);
        _removePiece.Enabled = _renderer.SelectedTile != null && _renderer.GetTileWithPieceIsSelected(_board);
            
        new SelectTileRuleSet(_renderer, _board)
            .Apply(_mouseX, _mouseY, _selectPiece);
    }

    private void AddPiece()
    {
            
    }
        
    private void EditPiece()
    {
        var tile = _renderer.SelectedTile;
        if (tile == null)
            return;

        var piece = _board.GetPieceUsingPhysicalCoordinates(tile.PhysicalX, tile.PhysicalY);
        if (piece == null)
            return;

        using var x = new PiecePropertiesDialog();
        x.Piece = piece;
        if (x.ShowDialog(this) == DialogResult.OK)
            Invalidate();
    }

    private void RemovePiece()
    {
        if (_renderer.SelectedTile == null)
            return;
        _possibleMoves = null;
        _board.SetPieceUsingPhysicalCoordinates(_renderer.SelectedTile, null);
    }

    private void SelectTileOrPiece()
    {
        var t = _renderer.GetTileAt(_mouseX, _mouseY);
        _renderer.SelectedTile = t;
            
        _possibleMoves = new ChessRuleEngine(_board)
            .GetPossibleMoves(_renderer.SelectedTile);
            
        Invalidate();
    }
        
    private void PositionBoard()
    {
        var width = (int)(ClientRectangle.Width * 0.9);
        var height = (int)(ClientRectangle.Height * 0.9);
            
        if (width < height)
            height = width;
        else
            width = height;
            
        _renderer.BoardSize = new Size(width, height);
        var x = ClientRectangle.Width / 2 - width / 2;
        var y = ClientRectangle.Height / 2 - height / 2;
        _renderer.BoardPosition = new Point(x, y);
        Invalidate();
    }
        
    private void MainWindowLoad(object sender, EventArgs e)
    {
        ContextMenuStrip = _contextMenuStrip;
        _board.Reset();
        PositionBoard();
        Invalidate();
    }
        
    private void MainWindowResize(object sender, EventArgs e)
    {
        Invalidate();
    }

    private void MainWindowPaint(object sender, PaintEventArgs e)
    {
        PositionBoard();
        _renderer.Draw(e.Graphics, Font, _board, _possibleMoves); 
    }

    private void MainWindow_KeyDown(object sender, KeyEventArgs e)
    {
            
    }

    private void MainWindow_MouseClick(object sender, MouseEventArgs e)
    {
        var newSelection = _renderer.GetTileAt(e.X, e.Y);
        if (_renderer.SelectedTile == null)
        {
            _renderer.SelectedTile = newSelection;

            _possibleMoves = new ChessRuleEngine(_board)
                .GetPossibleMoves(_renderer.SelectedTile);
                
            Invalidate();
            return;
        }
        if (_renderer.SelectedTile.IsSameLocationAs(newSelection))
        {
            _renderer.SelectedTile = null;
            _possibleMoves = null;
            Invalidate();
            return;
        }
        var piece = _board.GetPieceUsingPhysicalCoordinates(_renderer.SelectedTile);
        if (piece == null)
        {
            _renderer.SelectedTile = newSelection;

            _possibleMoves = new ChessRuleEngine(_board)
                .GetPossibleMoves(_renderer.SelectedTile);
        }
        else
        {
            _board.SetPieceUsingPhysicalCoordinates(_renderer.SelectedTile, _board.GetPieceUsingPhysicalCoordinates(newSelection));
            _renderer.SelectedTile = newSelection;
            _board.SetPieceUsingPhysicalCoordinates(newSelection, piece);
                
            _possibleMoves = new ChessRuleEngine(_board)
                .GetPossibleMoves(_renderer.SelectedTile);

        }
        Invalidate();
    }
}