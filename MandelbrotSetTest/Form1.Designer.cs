namespace MandelbrotSetTest
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.mandelbrotControl1 = new MandelbrotSetTest.MandelbrotControl();
            this.imgW = new System.Windows.Forms.TextBox();
            this.imgH = new System.Windows.Forms.TextBox();
            this.imgI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 368);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Render";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mandelbrotControl1
            // 
            this.mandelbrotControl1.BackColor = System.Drawing.Color.Black;
            this.mandelbrotControl1.Enabled = false;
            this.mandelbrotControl1.Location = new System.Drawing.Point(12, 12);
            this.mandelbrotControl1.Name = "mandelbrotControl1";
            this.mandelbrotControl1.Size = new System.Drawing.Size(500, 350);
            this.mandelbrotControl1.TabIndex = 1;
            this.mandelbrotControl1.Text = "mandelbrotControl1";
            // 
            // imgW
            // 
            this.imgW.Location = new System.Drawing.Point(62, 397);
            this.imgW.Name = "imgW";
            this.imgW.Size = new System.Drawing.Size(100, 20);
            this.imgW.TabIndex = 2;
            // 
            // imgH
            // 
            this.imgH.Location = new System.Drawing.Point(213, 397);
            this.imgH.Name = "imgH";
            this.imgH.Size = new System.Drawing.Size(100, 20);
            this.imgH.TabIndex = 2;
            // 
            // imgI
            // 
            this.imgI.Location = new System.Drawing.Point(369, 394);
            this.imgI.Name = "imgI";
            this.imgI.Size = new System.Drawing.Size(100, 20);
            this.imgI.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 400);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 397);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(319, 404);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 441);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgI);
            this.Controls.Add(this.imgH);
            this.Controls.Add(this.imgW);
            this.Controls.Add(this.mandelbrotControl1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private MandelbrotControl mandelbrotControl1;
        private System.Windows.Forms.TextBox imgW;
        private System.Windows.Forms.TextBox imgH;
        private System.Windows.Forms.TextBox imgI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

