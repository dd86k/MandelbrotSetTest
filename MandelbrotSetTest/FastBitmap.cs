using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MandelbrotSetTest
{
    /// <summary>
    /// A faster bitmap class.
    /// </summary>
    unsafe public class FastBitmap : IDisposable
    {
        struct PixelData
        {
            public byte Alpha, Red, Green, Blue;

            public override string ToString()
            {
                return $"#{Alpha:X2}{Red:X2}{Green:X2}{Blue:X2}";
            }
        }

        Bitmap workingBitmap;
        BitmapData bitmapData;
        PixelData* pixelData;
        byte* basep;
        int width;

        /**
         * Constructors
         */

        public FastBitmap(Bitmap image)
        {
            workingBitmap = image;
        }

        public FastBitmap(int width, int height)
        {
            workingBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
        }

        /**
         * Public methods
         */

        public void LockImage()
        {
            Rectangle bounds = new Rectangle(Point.Empty, workingBitmap.Size);
            width = bounds.Width * sizeof(PixelData);

            if (width % 4 != 0)
                width = 4 * (width / 4 + 1);

            bitmapData =
                workingBitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            basep = (byte*)bitmapData.Scan0.ToPointer();
        }

        public void UnlockImage()
        {
            workingBitmap.UnlockBits(bitmapData);
            bitmapData = null;
            basep = null;
        }

        public Color GetPixel(int x, int y)
        {
            pixelData = (PixelData*)(basep + y * width + x * sizeof(PixelData));
            return Color.FromArgb(pixelData->Alpha,
                                  pixelData->Red,
                                  pixelData->Green,
                                  pixelData->Blue);
        }

        public Color GetNextPixel()
        {
            pixelData++;
            return Color.FromArgb(pixelData->Alpha,
                                  pixelData->Red,
                                  pixelData->Green,
                                  pixelData->Blue);
        }

        public void SetPixel(int x, int y, Color color)
        {
            PixelData* p = (PixelData*)(basep + y * width + x * sizeof(PixelData));
            // PixelFormat.Format32bppArgb
            p->Alpha = color.A;
            p->Red = color.R;
            p->Green = color.G;
            p->Blue = color.B;
        }

        public void SetPixel(int x, int y, byte a, byte r, byte g, byte b)
        {
            PixelData* p = (PixelData*)(basep + y * width + x * sizeof(PixelData));
            p->Alpha = a;
            p->Red = r;
            p->Green = g;
            p->Blue = b;
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            PixelData* p = (PixelData*)(basep + y * width + x * sizeof(PixelData));
            p->Alpha = 0xff;
            p->Red = r;
            p->Green = g;
            p->Blue = b;
        }

        public void Dispose()
        {
            workingBitmap.Dispose();
            workingBitmap = null;
            GC.Collect();
        }

        /// <summary>
        /// Gets current image.
        /// Note that the image must be unlocked from memory first.
        /// </summary>
        public Image Image => workingBitmap;
    }
}
