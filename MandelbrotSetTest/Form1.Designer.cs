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
            this.mandelbrotControl1 = new MandelbrotSetTest.MandelbrotControl();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 368);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(70, 368);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 32);
            this.button2.TabIndex = 2;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 412);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mandelbrotControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private MandelbrotControl mandelbrotControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

