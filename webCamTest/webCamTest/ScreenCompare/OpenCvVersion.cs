﻿using OpenCvSharp;
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
        public Bitmap croppedBitmap;
        Start_ScreenCompare screenCompare;
        public bool lightsOn = false;

        public OpenCvVersion(string _name, int _camIndex, PictureBox _pictureBox, Form1 _mainWindow)
        {
            mainWindow = _mainWindow;
            pictureBox = _pictureBox;
            name = _name;
            camIndex = _camIndex;
            screenCompare = new Start_ScreenCompare();
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
            //  capture.Fps = 30;
            ////opens a settings window for the cameras
            if (name == "Left")
            {
              
                    capture.Exposure = -6;
                    capture.Brightness = -64;
          
            }
            try
            {
                capture.Open(camIndex);
            }
            catch(Exception ex)
            {

            }


            while (true)
            {
                try
                {
                    if (name == "BottomMiddle" && lightsOn == true)
                    {
                        capture.Exposure = -6;
                        capture.Brightness = -64;
                    }
                    else if (name == "BottomMiddle" && lightsOn == false)
                    {
                        capture.Exposure = -7;
                        capture.Brightness = 64;
                    }
                    capture.Read(frame);
                    image = BitmapConverter.ToBitmap(frame);
                  
                    if(name!="Reverse" && name != "BottomMiddle")
                    {
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    else if (name == "BottomMiddle")
                    {
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);

                    }
                    if (takePic)
                    {
                        screenCompare.CaptureOrigImage(image, name);
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
                try
                {
                    if (name == "BottomMiddle")
                    {
                        croppedBitmap = CropImage(bitmap);
                    }

                    if (counter % 2 == 0 && name != "Reverse")
                    {
                        screenCompare.findImage(name, bitmap, out imageToShow, out temp);
                        message = temp;

                    }
                    //if(name == "BottomMiddle")
                    //{
                    //    screenCompare.findImage(name, bitmap, out imageToShow, out temp);
                    //    message = temp;
                    //}
                    else
                    {
                        imageToShow = bitmap;
                    }
                    if (imageToShow != null)
                    {
                        finalImage = (Bitmap)imageToShow.Clone();
                     
                    }
                }
                catch(Exception ex)
                {

                }

                if (counter == 50)
                {
                    counter = 0;
                }
                else
                {
                    counter++;
                }
            }
         
        }
        private Bitmap CropImage(Bitmap origBitmap)
        {
           // Rec{ X = 88 Y = 179 Width = 43 Height = 65}
           // Rectangle croppedrectangle = new Rectangle(match.X - 20, match.Y - 20, match.Width + 350, match.Height + 200);
            Rectangle croppedRect = new Rectangle(210, 175, 300, 190);
            int width = Math.Abs(croppedRect.Width);
            int heigh = Math.Abs(croppedRect.Height);
            Bitmap nb = new Bitmap(width, heigh);
            Graphics g = Graphics.FromImage(nb);
            g.DrawImage(origBitmap, 0, 0, croppedRect, GraphicsUnit.Pixel);
            return nb;

        }
    }
}
