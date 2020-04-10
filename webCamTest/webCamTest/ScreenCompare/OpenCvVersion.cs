using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Concurrent;
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
        public string message;
        private Form1 mainWindow;

        public OpenCvVersion(string _name, int _camIndex, PictureBox _pictureBox, Form1 _mainWindow)
        {
            mainWindow = _mainWindow;
            pictureBox = _pictureBox;
            name = _name;
            camIndex = _camIndex;
            CaptureCamera();

        }

        //public ConcurrentDictionary<string, Bitmap> concurrentDictionary;
        private void CaptureCamera()
        {
            camera = new Thread(new ThreadStart(CaptureCameraCallback));
            camera.Start();

            //if (name == "Middle")
            //{
            //    concurrentDictionary = new ConcurrentDictionary<string, Bitmap>();
            //    //Thread a = new Thread(SendBitmapOverTCP);
            //    //a.Start();
            //}
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
        Bitmap imageToShow;

        private void ProcessFrames(Bitmap bitmap)
        {
            string temp = "";
            if (bitmap != null)
            {
                if (counter % 2 == 0)
                {
                    Start_ScreenCompare.findImage(name, bitmap, out imageToShow, out temp);
                    message = temp;
                }
                else
                {
                    imageToShow = bitmap;
                }

                if (name == "Middle")
                {
                    CropAndSendBitmap.SendBitmap(imageToShow);
                    //concurrentDictionary.AddOrUpdate(name, imageToShow, (name, imageToShow) => imageToShow);
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

                //Console.WriteLine(message);


                if (message.Equals("ReverseSymbol = true"))
                {
                    mainWindow.Invoke((Action)delegate
                    {
                        mainWindow.BringToFront();
                    });
                }
                else if (message.Equals("ReverseSymbol = false"))
                {

                    mainWindow.Invoke((Action)delegate
                    {
                        mainWindow.SendToBack();
                    });
                }

            }
           
        }

        //private void SendBitmapOverTCP()
        //{
        //    //CropAndSendBitmap cropAndSendBitmap = new CropAndSendBitmap();
        //    while (true)
        //    {
        //        Bitmap x;
        //        concurrentDictionary.TryGetValue("Middle", out x);
        //        CropAndSendBitmap.SendBitmap(x);
        //        //Thread.Sleep(100);

        //    }
        //}
    }
}
