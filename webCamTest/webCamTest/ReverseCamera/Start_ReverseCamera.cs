using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace webCamTest.ReverseCamera
{
    class Start_ReverseCamera
    {
        private VideoCapture capture;        //takes images from camera as image frames
        private PictureBox reverseCamPB;
        private int revCamIndex;
        private Image<Bgr, byte> _sourceReverse;

        public void SetCamIndex(int _revCamIndex)
        {
            revCamIndex = _revCamIndex;
        }

        public void StartReverseCamera(PictureBox _reverseCamPB)
        {
            reverseCamPB = _reverseCamPB;

            if (capture == null)
            {
                try
                {
                    capture = new VideoCapture(revCamIndex);
                    capture.Start();
                    if (capture.IsOpened)
                    {
                        Thread x = new Thread(ProcessFrameReverse);
                        x.Start();
                    }
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }

        }

        private void ProcessFrameReverse()
        {
            while (true)
            {
                try
                {
                    _sourceReverse = capture.QueryFrame().ToImage<Bgr, byte>();
                    //pictureBox1.Image = matImage.Bitmap; // Directly show Mat object in *ImageBox*
                    reverseCamPB.Image = _sourceReverse.Bitmap; // Show Image<,> object in *ImageBox*            
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

    }
}