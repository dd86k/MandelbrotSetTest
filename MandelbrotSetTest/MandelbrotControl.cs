using System.Windows.Forms;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

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

    unsafe class MandelbrotControl : Control
    {
        public MandelbrotControl()
        {
            BackColor = Color.Black;
            sw = new Stopwatch();
            /*SetStyle(ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint, true);*/
        }

        const double CX0 = -2.0, CX1 = 3.0, CY0 = -1.0, CY1 = 2.0;

        public void Render()
        {
            Invalidate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Color MapColor(double t)
        {
            double t2 = 1.0 - t;

            return Color.FromArgb((int)(9.0 * t2 * t * t * t * 255.0),
                                  (int)(15.0 * t2 * t2 * t * t * 255.0),
                                  (int)(8.5 * t2 * t2 * t2 * t * 255.0));
        }

        protected unsafe override void OnPaint(PaintEventArgs e)
        {
            //SuspendLayout();

            e.Graphics.DrawImageUnscaled(GenerateImage(), 0, 0);
            
            //ResumeLayout(true);
        }

        static Stopwatch sw;
        public Image GenerateImage()
        {
            sw.Restart();
            double w = Width, h = Height, i;

            // Maximum iterations
            const int m = 50;
            Complex z, c;


            FastBitmap f = new FastBitmap((int)w, (int)h);
            f.LockImage();

            unchecked
            {
                for (int y = 0; y < h; ++y)
                {
                    for (int x = 0; x < w; ++x)
                    {
                        c = new Complex(CX0 + x / w * CX1, CY0 + y / h * CY1);
                        z = 0;
                        i = 0;

                        while (i < m)
                        {
                            z = Complex.Pow(z, 2) + c;

                            if (Complex.Abs(z) > 2)
                                break;

                            ++i;
                        }

                        double t = i / m, t2 = 1.0 - t;

                        f.SetPixel(x, y,
                            (byte)(9.0 * t2 * t * t * t * 255.0),
                            (byte)(15.0 * t2 * t2 * t * t * 255.0),
                            (byte)(8.5 * t2 * t2 * t2 * t * 255.0)
                        );
                    }
                }
            }

            /*Parallel.For(0, (int)h, y =>
            {
                Parallel.For(0, (int)w, x =>
                {

                });
            });*/

            f.UnlockImage();

            sw.Stop();
            Console.WriteLine(sw.Elapsed);

            return f.Image;
        }
    }
}
