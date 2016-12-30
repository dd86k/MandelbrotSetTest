using System.Drawing;
using System.Windows.Forms;

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
        }

        public void Render()
        {
            Invalidate();
        }

        Image img;

        protected unsafe override void OnPaint(PaintEventArgs e)
        {
            if (Enabled)
            {
                img = Renderer.GenerateImage(Width, Height);
                e.Graphics.DrawImageUnscaled(img, 0, 0);
                img.Dispose();
            }
        }
        
    }
}
