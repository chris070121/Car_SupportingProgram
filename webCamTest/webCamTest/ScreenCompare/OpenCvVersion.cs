using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace webCamTest.ScreenCompare
{
    public class OpenCvVersion
    {
        string name;
        VideoCapture capture;
        Mat frame;
        Bitmap image;
        private Thread camera;
        public bool takePic = false;
        int camIndex;
        private PictureBox pictureBox;
        private string message;
        private Form1 mainWindow;

        public OpenCvVersion(string _name, int _camIndex, PictureBox _pictureBox, Form1 _mainWindow)
        {
            mainWindow = _mainWindow;
            pictureBox = _pictureBox;
            name = _name;
            camIndex = _camIndex;
            CaptureCamera();
        }


        private void CaptureCamera()
        {
            camera = new Thread(new ThreadStart(CaptureCameraCallback));
            camera.Start();
        }
        private void CaptureCameraCallback()
        {

            frame = new Mat();
            capture = new VideoCapture(camIndex);
            capture.Open(camIndex);

            while (true)
            {
                capture.Read(frame);
                image = BitmapConverter.ToBitmap(frame);
                if(takePic)
                {
                    Start_ScreenCompare.CaptureOrigImage(image, name);
                    takePic = false;
                }
                ProcessFrames(image);
            }
            
        }
        int counter = 0;

        private void ProcessFrames(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                Bitmap imageToShow;
                if (counter % 15 == 0)
                {
                    Start_ScreenCompare.findImage(name, bitmap, out imageToShow, out message);
                }
                else
                {
                    imageToShow = bitmap;
                }

                pictureBox.Invoke((Action)delegate
                {
                    pictureBox.Image = imageToShow;
                });

                if (counter == 50)
                {
                    counter = 0;
                }
                else
                {
                    counter++;
                }
                Console.WriteLine(message);

                if (message.Equals("EmergencyBrake = true"))
                {
                    mainWindow.Invoke((Action)delegate
                    {
                        mainWindow.BringToFront();
                    });
                }
                else if (message.Equals("EmergencyBrake = false"))
                {
                    
                    mainWindow.Invoke((Action)delegate
                    {
                        mainWindow.SendToBack();
                    });
                }

            }
           
        }
    }
}
