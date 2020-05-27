using ChessEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChessBoardAnalyzer
{
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
}
