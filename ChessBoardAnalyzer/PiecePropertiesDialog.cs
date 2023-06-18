using ChessEngine;
using System;
using System.Windows.Forms;

namespace ChessBoardAnalyzer;

public partial class PiecePropertiesDialog : Form
{
    public Piece Piece { get; set; }

    public PiecePropertiesDialog()
    {
        InitializeComponent();
    }

    private void PiecePropertiesDialog_Load(object sender, EventArgs e)
    {
        txtPieceName.Text = Piece.ToFriendlyString();
    }
}