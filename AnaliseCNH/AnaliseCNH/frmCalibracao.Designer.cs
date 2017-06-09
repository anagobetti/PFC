namespace AnaliseCNH
{
    partial class frmCalibracao
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.th2 = new System.Windows.Forms.TrackBar();
            this.th4 = new System.Windows.Forms.TrackBar();
            this.th1 = new System.Windows.Forms.TrackBar();
            this.th3 = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imgCinza0 = new Emgu.CV.UI.ImageBox();
            this.imgCinza1 = new Emgu.CV.UI.ImageBox();
            this.imgCanny0 = new Emgu.CV.UI.ImageBox();
            this.imgCanny1 = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.th2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.th4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.th1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.th3)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCinza0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCinza1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCanny0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCanny1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.80207F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.19794F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1301, 581);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.th2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.th4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.th1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.th3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1295, 80);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // th2
            // 
            this.th2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.th2.Location = new System.Drawing.Point(3, 43);
            this.th2.Maximum = 255;
            this.th2.Name = "th2";
            this.th2.Size = new System.Drawing.Size(641, 34);
            this.th2.TabIndex = 9;
            this.th2.Scroll += new System.EventHandler(this.th2_Scroll);
            // 
            // th4
            // 
            this.th4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.th4.Location = new System.Drawing.Point(650, 43);
            this.th4.Maximum = 255;
            this.th4.Name = "th4";
            this.th4.Size = new System.Drawing.Size(642, 34);
            this.th4.TabIndex = 8;
            this.th4.Scroll += new System.EventHandler(this.th4_Scroll);
            // 
            // th1
            // 
            this.th1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.th1.Location = new System.Drawing.Point(3, 3);
            this.th1.Maximum = 255;
            this.th1.Name = "th1";
            this.th1.Size = new System.Drawing.Size(641, 34);
            this.th1.TabIndex = 5;
            this.th1.Scroll += new System.EventHandler(this.th1_Scroll);
            // 
            // th3
            // 
            this.th3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.th3.Location = new System.Drawing.Point(650, 3);
            this.th3.Maximum = 255;
            this.th3.Name = "th3";
            this.th3.Size = new System.Drawing.Size(642, 34);
            this.th3.TabIndex = 6;
            this.th3.Scroll += new System.EventHandler(this.th3_Scroll);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.imgCinza0, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.imgCinza1, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.imgCanny0, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.imgCanny1, 1, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 89);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.492187F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.75391F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.75391F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1295, 489);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "To 0 ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(650, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To Bin";
            // 
            // imgCinza0
            // 
            this.imgCinza0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgCinza0.Location = new System.Drawing.Point(3, 24);
            this.imgCinza0.Name = "imgCinza0";
            this.imgCinza0.Size = new System.Drawing.Size(641, 227);
            this.imgCinza0.TabIndex = 2;
            this.imgCinza0.TabStop = false;
            // 
            // imgCinza1
            // 
            this.imgCinza1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgCinza1.Location = new System.Drawing.Point(650, 24);
            this.imgCinza1.Name = "imgCinza1";
            this.imgCinza1.Size = new System.Drawing.Size(642, 227);
            this.imgCinza1.TabIndex = 2;
            this.imgCinza1.TabStop = false;
            // 
            // imgCanny0
            // 
            this.imgCanny0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgCanny0.Location = new System.Drawing.Point(3, 257);
            this.imgCanny0.Name = "imgCanny0";
            this.imgCanny0.Size = new System.Drawing.Size(641, 229);
            this.imgCanny0.TabIndex = 2;
            this.imgCanny0.TabStop = false;
            // 
            // imgCanny1
            // 
            this.imgCanny1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgCanny1.Location = new System.Drawing.Point(650, 257);
            this.imgCanny1.Name = "imgCanny1";
            this.imgCanny1.Size = new System.Drawing.Size(642, 229);
            this.imgCanny1.TabIndex = 2;
            this.imgCanny1.TabStop = false;
            // 
            // frmCalibracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 581);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmCalibracao";
            this.Text = "frmCalibracao";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.th2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.th4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.th1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.th3)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCinza0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCinza1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCanny0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCanny1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar th3;
        private System.Windows.Forms.TrackBar th1;
        private System.Windows.Forms.TrackBar th4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TrackBar th2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Emgu.CV.UI.ImageBox imgCinza0;
        private Emgu.CV.UI.ImageBox imgCinza1;
        private Emgu.CV.UI.ImageBox imgCanny0;
        private Emgu.CV.UI.ImageBox imgCanny1;
    }
}