namespace Solitaire
{
    partial class FormGame
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTableaus = new System.Windows.Forms.Panel();
            this.pnlFoundations = new System.Windows.Forms.Panel();
            this.tblScoring = new System.Windows.Forms.TableLayoutPanel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblMoves = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tblScoring.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pnlTableaus, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlFoundations, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tblScoring, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1578, 844);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlTableaus
            // 
            this.pnlTableaus.BackColor = System.Drawing.Color.ForestGreen;
            this.pnlTableaus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTableaus.Location = new System.Drawing.Point(318, 45);
            this.pnlTableaus.Name = "pnlTableaus";
            this.pnlTableaus.Size = new System.Drawing.Size(940, 595);
            this.pnlTableaus.TabIndex = 0;
            // 
            // pnlFoundations
            // 
            this.pnlFoundations.BackColor = System.Drawing.Color.ForestGreen;
            this.pnlFoundations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFoundations.Location = new System.Drawing.Point(1264, 45);
            this.pnlFoundations.Name = "pnlFoundations";
            this.pnlFoundations.Size = new System.Drawing.Size(311, 595);
            this.pnlFoundations.TabIndex = 1;
            // 
            // tblScoring
            // 
            this.tblScoring.ColumnCount = 3;
            this.tblScoring.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblScoring.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblScoring.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblScoring.Controls.Add(this.lblMoves, 2, 0);
            this.tblScoring.Controls.Add(this.lblScore, 1, 0);
            this.tblScoring.Controls.Add(this.lblTimer, 0, 0);
            this.tblScoring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblScoring.Location = new System.Drawing.Point(318, 3);
            this.tblScoring.Name = "tblScoring";
            this.tblScoring.RowCount = 1;
            this.tblScoring.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblScoring.Size = new System.Drawing.Size(940, 36);
            this.tblScoring.TabIndex = 2;
            // 
            // lblTimer
            // 
            this.lblTimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.Location = new System.Drawing.Point(3, 0);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(307, 32);
            this.lblTimer.TabIndex = 0;
            this.lblTimer.Text = "0:00:00";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScore
            // 
            this.lblScore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(316, 0);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(307, 32);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "Score: 0";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMoves
            // 
            this.lblMoves.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMoves.AutoSize = true;
            this.lblMoves.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoves.Location = new System.Drawing.Point(629, 0);
            this.lblMoves.Name = "lblMoves";
            this.lblMoves.Size = new System.Drawing.Size(308, 32);
            this.lblMoves.TabIndex = 2;
            this.lblMoves.Text = "Moves: 0";
            this.lblMoves.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(1578, 844);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormGame";
            this.Text = "FormGame";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tblScoring.ResumeLayout(false);
            this.tblScoring.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlTableaus;
        private System.Windows.Forms.Panel pnlFoundations;
        private System.Windows.Forms.TableLayoutPanel tblScoring;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblMoves;
    }
}

