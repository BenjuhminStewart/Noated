using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace BenDraw.PanelTools
{
    internal class SaveSystem
    {
        public SaveSystem()
        {

        }

        public void Save(Bitmap bm, PictureBox pic)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*| *.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, pic.Width, pic.Height), bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
                MessageBox.Show("Image Saved Successfully...");
            }
        }
    }
}
