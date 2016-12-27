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

    class MandelbrotControl : Control
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

        Image img;

        protected unsafe override void OnPaint(PaintEventArgs e)
        {
            if (Enabled)
            {
                GenerateImage();
                e.Graphics.DrawImageUnscaled(img, 0, 0);
                img.Dispose();
            }
        }

        static Stopwatch sw;
        void GenerateImage()
        {
            sw.Restart();
            double w = Width, h = Height;

            // Maximum iterations
            const int m = 100;

            FastBitmap f = new FastBitmap((int)w, (int)h);
            f.LockImage();

            ParallelOptions o = new ParallelOptions();
            o.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Parallel.For(0, (int)h, o, y =>
            {
                Parallel.For(0, (int)w, o, x =>
                {
                    Complex c = new Complex(CX0 + x / w * CX1, CY0 + y / h * CY1);
                    Complex z = 0;
                    double i = 0;

                    while (i < m)
                    {
                        z = Complex.Pow(z, 2) + c;

                        if (Complex.Abs(z) > 2)
                            break;

                        ++i;
                    }

                    double t = i / m, t2 = 1.0 - t;

                    f.SetPixel(x, y, (byte)(9.0 * t2 * t * t * t * 255.0),
                                     (byte)(15.0 * t2 * t2 * t * t * 255.0),
                                     (byte)(8.5 * t2 * t2 * t2 * t * 255.0));
                });                                                       
            });                      

            f.UnlockImage();

            sw.Stop();
            Console.WriteLine(sw.Elapsed);

            img = f.Image;
        }
    }
}
