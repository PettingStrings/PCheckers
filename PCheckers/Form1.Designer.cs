namespace PCheckers
{
    partial class PCheckers
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picChessBoard = new System.Windows.Forms.PictureBox();
            this.lblB = new System.Windows.Forms.Label();
            this.lblTurn = new System.Windows.Forms.Label();
            this.lblW = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.rdbBgMusic = new System.Windows.Forms.RadioButton();
            this.picKing = new System.Windows.Forms.PictureBox();
            this.timWalk = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picChessBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKing)).BeginInit();
            this.SuspendLayout();
            // 
            // picChessBoard
            // 
            this.picChessBoard.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picChessBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picChessBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picChessBoard.Location = new System.Drawing.Point(318, 5);
            this.picChessBoard.Margin = new System.Windows.Forms.Padding(0);
            this.picChessBoard.Name = "picChessBoard";
            this.picChessBoard.Size = new System.Drawing.Size(480, 480);
            this.picChessBoard.TabIndex = 0;
            this.picChessBoard.TabStop = false;
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.BackColor = System.Drawing.Color.Transparent;
            this.lblB.Font = new System.Drawing.Font("Deltarune", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblB.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblB.Location = new System.Drawing.Point(82, 127);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(198, 24);
            this.lblB.TabIndex = 12;
            this.lblB.Text = "Neri in campo: 12";
            // 
            // lblTurn
            // 
            this.lblTurn.AutoSize = true;
            this.lblTurn.BackColor = System.Drawing.Color.Transparent;
            this.lblTurn.Font = new System.Drawing.Font("Deltarune", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTurn.Location = new System.Drawing.Point(57, 29);
            this.lblTurn.Name = "lblTurn";
            this.lblTurn.Size = new System.Drawing.Size(174, 70);
            this.lblTurn.TabIndex = 10;
            this.lblTurn.Text = "Turno Dei\r\nBianchi";
            this.lblTurn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblW
            // 
            this.lblW.AutoSize = true;
            this.lblW.BackColor = System.Drawing.Color.Transparent;
            this.lblW.Font = new System.Drawing.Font("Deltarune", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblW.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblW.Location = new System.Drawing.Point(82, 151);
            this.lblW.Name = "lblW";
            this.lblW.Size = new System.Drawing.Size(233, 24);
            this.lblW.TabIndex = 11;
            this.lblW.Text = "Bianchi in campo: 12";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Black;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Deltarune", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnReset.Location = new System.Drawing.Point(120, 362);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(111, 32);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Restart";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSkip
            // 
            this.btnSkip.BackColor = System.Drawing.Color.Black;
            this.btnSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkip.Font = new System.Drawing.Font("Deltarune", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSkip.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSkip.Location = new System.Drawing.Point(120, 188);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(111, 23);
            this.btnSkip.TabIndex = 13;
            this.btnSkip.Text = "Skip Turn";
            this.btnSkip.UseVisualStyleBackColor = false;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // rdbBgMusic
            // 
            this.rdbBgMusic.AutoCheck = false;
            this.rdbBgMusic.AutoSize = true;
            this.rdbBgMusic.BackColor = System.Drawing.Color.Transparent;
            this.rdbBgMusic.Checked = true;
            this.rdbBgMusic.Font = new System.Drawing.Font("Deltarune", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbBgMusic.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbBgMusic.Location = new System.Drawing.Point(95, 102);
            this.rdbBgMusic.Name = "rdbBgMusic";
            this.rdbBgMusic.Size = new System.Drawing.Size(60, 17);
            this.rdbBgMusic.TabIndex = 14;
            this.rdbBgMusic.TabStop = true;
            this.rdbBgMusic.Text = "Music";
            this.rdbBgMusic.UseVisualStyleBackColor = false;
            this.rdbBgMusic.Click += new System.EventHandler(this.rdbBgMusic_Click);
            // 
            // picKing
            // 
            this.picKing.Location = new System.Drawing.Point(131, 400);
            this.picKing.Name = "picKing";
            this.picKing.Size = new System.Drawing.Size(100, 105);
            this.picKing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picKing.TabIndex = 15;
            this.picKing.TabStop = false;
            // 
            // timWalk
            // 
            this.timWalk.Tick += new System.EventHandler(this.timWalk_Tick);
            // 
            // PCheckers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(804, 489);
            this.Controls.Add(this.picKing);
            this.Controls.Add(this.rdbBgMusic);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblTurn);
            this.Controls.Add(this.lblW);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.picChessBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "PCheckers";
            this.Text = "Checker King\'s Checker Game";
            this.Load += new System.EventHandler(this.DamaGame_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DamaGame_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.picChessBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKing)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picChessBoard;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label lblTurn;
        private System.Windows.Forms.Label lblW;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.RadioButton rdbBgMusic;
        private System.Windows.Forms.PictureBox picKing;
        private System.Windows.Forms.Timer timWalk;
    }
}

