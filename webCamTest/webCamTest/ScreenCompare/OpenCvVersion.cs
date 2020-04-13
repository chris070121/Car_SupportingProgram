using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public ConcurrentDictionary<string, Bitmap> concurrentDictionary;
        private void CaptureCamera()
        {
            camera = new Thread(new ThreadStart(CaptureCameraCallback));
            camera.Start();

        }
        private void CaptureCameraCallback()
        {

            frame = new Mat();
            capture = new VideoCapture(camIndex);
            ////opens a settings window for the cameras

            //if (name == "Middle")
            //{
            //    capture.Settings = 1;

            //}

            capture.Open(camIndex);

            while (true)
            {
                try
                {
                    capture.Read(frame);
                    image = BitmapConverter.ToBitmap(frame);
                    if (takePic)
                    {
                        Start_ScreenCompare.CaptureOrigImage(image, name);
                        takePic = false;
                    }
                    ProcessFrames(image);
                }
                catch(Exception ex)
                { }
            }
            
        }
        int counter = 0;
        Bitmap imageToShow;
        public Bitmap finalImage;
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
                    finalImage = (Bitmap)imageToShow.Clone();
                    //var Ocr = new IronOcr.AdvancedOcr();
                    //Ocr.CleanBackgroundNoise = true;
                    //Ocr.DetectWhiteTextOnDarkBackgrounds = true;
                    //Ocr.EnhanceContrast = true;
                    //Ocr.EnhanceResolution = true;
                    //var Result = Ocr.Read(@"C:\Users\chris\Desktop\Car_SupportingProgram\webCamTest\webCamTest\bin\Debug\oilLife.png");
                    //string temp1 = Regex.Replace(Result.Text, "[^a-zA-Z][^0-9]", " ");
                    //Console.WriteLine(temp1);
                    //// CropAndSendBitmap.SendBitmap();
                    //concurrentDictionary.AddOrUpdate(name, imageToShow, (name, imageToShow)=>imageToShow);
                }
                pictureBox.Invoke((Action)delegate
                {
                    pictureBox.Image = imageToShow;
                    if (name != "Reverse")
                    {
                        pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
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


            }
           
        }
    }
}
