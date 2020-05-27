using System.Windows.Forms;
using ChessEngine;
using ChessGraphicsRenderer;

namespace ChessBoardAnalyzer
{
    public class SelectTileRuleSet
    {
        private readonly Renderer _renderer;
        private readonly Board _board;
        
        public SelectTileRuleSet(Renderer renderer, Board board)
        {
            _renderer = renderer;
            _board = board;
        }
        
        public void Apply(int mouseX, int mouseY, ToolStripMenuItem menuItem)
        {
            var t = _renderer.GetTileAt(mouseX, mouseY);
            if (t == null)
            {
                menuItem.Enabled = false;
            }
            else
            {
                if (_renderer.SelectedTile == null)
                {
                    menuItem.Enabled = true;
                    menuItem.Text = _board.GetPieceUsingPhysicalCoordinates(t) == null
                        ? @"Select tile"
                        : @"Select piece";
                }
                else
                {
                    if (_renderer.SelectedTile.IsSameLocationAs(t))
                    {
                        menuItem.Enabled = false;
                    }
                    else
                    {
                        menuItem.Enabled = true;
                        menuItem.Text = _board.GetPieceUsingPhysicalCoordinates(t) == null
                            ? @"Select tile"
                            : @"Select piece";
                    }
                }
            }
        }
    }
}