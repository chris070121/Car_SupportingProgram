using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace webCamTest.Gauges
{
    public partial class GaugeInfo : Form
    {
        private Bitmap bitmap;
        public GaugeInfo()
        {
            InitializeComponent();
        }

        public void SetBitmap(Bitmap _bitmap)
        {
            bitmap = _bitmap;
        }

        private void GaugeInfo_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            Thread a = new Thread(updateGui);
            a.Start();
        }

        private void updateGui()
        {
            
            while (true)
            {
                if (bitmap != null)
                {

                    pictureBox1.Invoke((Action)delegate
                    {
                        pictureBox1.Width = bitmap.Width;
                        pictureBox1.Height = bitmap.Height;
                        pictureBox1.Image = bitmap;

                    });
                    //pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                }
                Thread.Sleep(100);
            }
        }
    }
}
