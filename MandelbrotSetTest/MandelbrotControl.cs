using System.Windows.Forms;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using System;

namespace MandelbrotSetTest
{
/*
 * Mouse event
    mx1 = e.X
    my1 = e.Y

    double scaleX, scaleY;
	scaleX = (cx1 - cx0) / (double )panelMain.Width;
	scaleY = (cy1 - cy0) / (double)panelMain.Height;
	cx1 = (double )mx1*scaleX + cx0;
	cy1 = (double)my1*scaleY + cy0;
	cx0 = (double)mx0 * scaleX + cx0;
	cy0 = (double)my0 * scaleY + cy0;
*/

    class MandelbrotControl : Control
    {
        public MandelbrotControl()
        {
            BackColor = Color.Black;
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint, true);
        }

        public double CX0 = -2.0;
        public double CX1 = 3.0;
        public double CY0 = -1.0;
        public double CY1 = 2.0;

        public void Render()
        {
            Invalidate();
        }

        Color MapColor(double t)
        {
            double t2 = 1.0 - t;

            return Color.FromArgb((int)(9.0 * t2 * t * t * t * 255.0),
                                  (int)(15.0 * t2 * t2 * t * t * 255.0),
                                  (int)(8.5 * t2 * t2 * t2 * t * 255.0));
        }

        protected unsafe override void OnPaint(PaintEventArgs e)
        {
            SuspendLayout();
            
            double w = Width;
            double h = Height;
            Bitmap b = new Bitmap((int)w, (int)h);
            // Maximum iterations
            const int m = 30;
            Complex z = new Complex();
            Complex c = new Complex();

            FastBitmap f = new FastBitmap(b); // Well, at least faster than Bitmap
            f.LockImage();

            // Tried to not block the main thread.
            Invoke(new Action(() =>
            {
                for (int y = 0; y < (int)h; ++y)
                {
                    for (int x = 0; x < w; x++)
                    {
                        z = 0;
                        c = new Complex(CX0 + x / w * CX1, CY0 + y / h * CY1); // 3.0, 2.0
                        int i = 0;

                        while (i < m)
                        {
                            z = Complex.Pow(z, 2) + c;

                            if (Complex.Abs(z) > 2)
                                break;

                            ++i;
                        }

                        f.SetPixel(x, y, MapColor((double)i / m));
                    }
                }
            }));

            f.UnlockImage();
            e.Graphics.DrawImageUnscaled(f.Image, 0, 0);
            f.Image.Dispose();

            ResumeLayout(true);
        }
    }
}
