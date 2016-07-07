using System.Drawing;
using System.Drawing.Imaging;

//TODO: IDisposable

namespace MandelbrotSetTest
{
    /// <summary>
    /// A faster bitmap class.
    /// </summary>
    /// <remarks>
    /// Something I took online but heavily modified for my taste and
    /// to make it actually usable.
    /// Original authors:
    /// http://www.vcskicks.com/fast-image-processing2.php
    /// </remarks>
    unsafe public class FastBitmap
    {
        private struct PixelData
        {
            public byte Alpha;
            public byte Red;
            public byte Blue;
            public byte Green;

            public override string ToString()
            {
                return $"#{Alpha:X2}{Red:X2}{Green:X2}{Blue:X2}";
            }
        }

        Bitmap workingBitmap;
        BitmapData bitmapData;
        PixelData* pixelData;
        byte* pBase;
        int width;

        /**
         * Constructors
         */

        public FastBitmap(Bitmap inputBitmap)
        {
            workingBitmap = inputBitmap;
        }

        /**
         * Public methods
         */

        public void LockImage()
        {
            Rectangle bounds = new Rectangle(Point.Empty, workingBitmap.Size);

            width = bounds.Width * sizeof(PixelData);
            if (width % 4 != 0) width = 4 * (width / 4 + 1);

            //Lock Image
            bitmapData = workingBitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            pBase = (byte*)bitmapData.Scan0.ToPointer();
        }

        public Color GetPixel(int x, int y)
        {
            pixelData = (PixelData*)(pBase + y * width + x * sizeof(PixelData));
            return Color.FromArgb(pixelData->Alpha, pixelData->Red, pixelData->Green, pixelData->Blue);
        }

        public Color GetPixelNext()
        {
            pixelData++;
            return Color.FromArgb(pixelData->Alpha, pixelData->Red, pixelData->Green, pixelData->Blue);
        }

        public void SetPixel(long x, int y, Color color)
        {
            PixelData* data = (PixelData*)(pBase + y * width + x * sizeof(PixelData));
            data->Alpha = color.A;
            data->Red = color.R;
            data->Green = color.G;
            data->Blue = color.B;
        }

        public void UnlockImage()
        {
            workingBitmap.UnlockBits(bitmapData);
            bitmapData = null;
            pBase = null;
        }

        /**
         * Public properties
         */

        public Bitmap Image
        {
            get
            {
                return workingBitmap;
            }
        }
    }
}
