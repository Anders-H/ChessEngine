namespace ChessBoardAnalyzer
{
    partial class PiecePropertiesDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPieceName = new System.Windows.Forms.Label();
            this.txtPieceName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblPieceName
            // 
            this.lblPieceName.AutoSize = true;
            this.lblPieceName.Location = new System.Drawing.Point(8, 8);
            this.lblPieceName.Name = "lblPieceName";
            this.lblPieceName.Size = new System.Drawing.Size(38, 15);
            this.lblPieceName.TabIndex = 0;
            this.lblPieceName.Text = "Piece:";
            // 
            // txtPieceName
            // 
            this.txtPieceName.Location = new System.Drawing.Point(8, 24);
            this.txtPieceName.Name = "txtPieceName";
            this.txtPieceName.ReadOnly = true;
            this.txtPieceName.Size = new System.Drawing.Size(392, 23);
            this.txtPieceName.TabIndex = 1;
            // 
            // PiecePropertiesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 328);
            this.Controls.Add(this.txtPieceName);
            this.Controls.Add(this.lblPieceName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PiecePropertiesDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Piece properties";
            this.Load += new System.EventHandler(this.PiecePropertiesDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPieceName;
        private System.Windows.Forms.TextBox txtPieceName;
    }
}