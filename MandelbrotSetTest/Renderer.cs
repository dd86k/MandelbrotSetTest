using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MandelbrotSetTest
{
    class Renderer
    {
        const double CX0 = -2.0, CX1 = 3.0, CY0 = -1.0, CY1 = 2.0;

        public static Image GenerateImage(int width, int height)
        {
            double w = width, h = height;

            // Maximum iterations
            const int m = 100;

            FastBitmap f = new FastBitmap((int)w, (int)h);
            f.LockImage();

            ParallelOptions o = new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
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

                        if (Complex.Abs(z) > 2) break;

                        ++i;
                    }

                    double t = i / m, t2 = 1.0 - t;

                    f.SetPixel(x, y, (byte)(9.0 * t2 * t * t * t * 255.0),
                                     (byte)(15.0 * t2 * t2 * t * t * 255.0),
                                     (byte)(8.5 * t2 * t2 * t2 * t * 255.0));
                });
            });

            f.UnlockImage();

            return f.Image;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Color MapColor(double t)
        {
            double t2 = 1.0 - t;

            return Color.FromArgb((int)(9.0 * t2 * t * t * t * 255.0),
                                  (int)(15.0 * t2 * t2 * t * t * 255.0),
                                  (int)(8.5 * t2 * t2 * t2 * t * 255.0));
        }
    }
}
