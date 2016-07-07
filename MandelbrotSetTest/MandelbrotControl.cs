// Usage: S<SolutionNumber>
#define S2

using System.Windows.Forms;
using System.Drawing;
using System.Numerics;
#if S3
using static System.Threading.Tasks.Parallel;
#endif
using System.Drawing.Imaging;

namespace MandelbrotSetTest
{
    class MandelbrotControl : Control
    {
        public MandelbrotControl()
        {
            BackColor = Color.Black;
        }

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
            const int m = 50;
            Complex z = new Complex();
            Complex c = new Complex();
            
#if S1
            /**
             * #1 - Supposedly fast, works when PixelFormat is Format24bppRgb, but is squished (X)
             * Taken from an online post 
             */

            BitmapData bitmapData =
                b.LockBits(new Rectangle(0,0,(int)w,(int)h), ImageLockMode.WriteOnly, b.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(b.PixelFormat) / 8;
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;

            byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

            for (int y = 0; y < h; y++)
            {
                byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                {
                    z = 0;
                    c = new Complex(-2.0 + x / w * 3, -1 + y / h * 2);
                    int i = 0;

                    while (i < m)
                    {
                        z = Complex.Pow(z, 2) + c;

                        if (Complex.Abs(z) > 2)
                            break;

                        ++i;
                    }

                    Color g = MapColor((double)i / m);

                    currentLine[x + 2] = g.R;
                    currentLine[x + 1] = g.G;
                    currentLine[x] = g.B;
                }
            }
            b.UnlockBits(bitmapData);
            e.Graphics.DrawImageUnscaled(b, 0, 0);
            b.Dispose();
#endif
#if S2
            /**
             * #2 FastBitmap, works best for now
             */

            FastBitmap f = new FastBitmap(b);
            f.LockImage();
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    z = 0;
                    c = new Complex(-2.0 + x / w * 3, -1 + y / h * 2);
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
            f.UnlockImage();
            e.Graphics.DrawImageUnscaled(f.Image, 0, 0);
            f.Image.Dispose();
#endif
#if S3
            /**
             * #3 FastBitmap + Parrarel, fastest, broken (colored TV static)
             */

            FastBitmap f = new FastBitmap(b);
            f.LockImage();
            For(0, (int)h, y =>
            {
                for (int x = 0; x < w; x++)
                {
                    z = 0;
                    c = new Complex(-2.0 + x / w * 3, -1 + y / h * 2);
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
            });
            f.UnlockImage();
            e.Graphics.DrawImageUnscaled(f.Image, 0, 0);
            f.Image.Dispose();
#endif

            e.Graphics.DrawRectangle(new Pen(Color.Red), 0, 0, (int)w - 1, (int)h - 1);
            ResumeLayout();
        }
    }
}
