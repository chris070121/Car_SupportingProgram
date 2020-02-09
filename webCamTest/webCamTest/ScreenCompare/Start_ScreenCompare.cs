using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using webCamTest.ScreenCompare;

namespace webCamTest.ScreenCompare
{
    class Start_ScreenCompare
    {
        private VideoCapture captureLeft;        //takes images from camera as image frames
        private VideoCapture captureMiddle;        //takes images from camera as image frames
        private VideoCapture captureRight;        //takes images from camera as image frames
        private string templatePicsPath;
        private Image<Bgr, byte> _sourceLeft;
        private Image<Bgr, byte> _sourceMiddle;
        private Image<Bgr, byte> _sourceRight;

        private PictureBox leftGaugePB;
        private PictureBox middleGaugePB;
        private PictureBox rightGaugePB;
        private int leftCamIndex;
        private int middleCamIndex;
        private int rightCamIndex;


        private void ProcessFrameLeft()
        {
            while (true)
            {
                _sourceLeft = captureLeft.QueryFrame().ToImage<Bgr, byte>();
                //pictureBox1.Image = matImage.Bitmap; // Directly show Mat object in *ImageBox*
                leftGaugePB.Image = _sourceLeft.Bitmap; // Show Image<,> object in *ImageBox*            
            }
        }

        private void ProcessFrameMiddle()
        {
            while (true)
            {
                Mat matImage = captureMiddle.QueryFrame();
                _sourceMiddle = matImage.ToImage<Bgr, byte>();
                //pictureBox1.Image = matImage.Bitmap; // Directly show Mat object in *ImageBox*
                middleGaugePB.Image = _sourceMiddle.Bitmap; // Show Image<,> object in *ImageBox*            
            }
        }

        private void ProcessFrameRight()
        {
            while (true)
            {
                _sourceRight = captureRight.QueryFrame().ToImage<Bgr, byte>();
                //pictureBox1.Image = matImage.Bitmap; // Directly show Mat object in *ImageBox*
                rightGaugePB.Image = _sourceRight.Bitmap; // Show Image<,> object in *ImageBox*            
            }
        }

        public void SetCamIndex(int leftCam, int middleCam, int rightCam, string _templatePicsPath)
        {
            leftCamIndex = leftCam;
            middleCamIndex = middleCam;
            rightCamIndex = rightCam;
            templatePicsPath = _templatePicsPath;
        }


        public void StartCamera(PictureBox leftPB, PictureBox middlePB, PictureBox rightPB )
        {
            leftGaugePB = leftPB;
            middleGaugePB = middlePB;
            rightGaugePB = rightPB;

            #region if capture is not created, create it now
            if (captureLeft == null)
            {
                try
                {
                    captureLeft = new VideoCapture(leftCamIndex);
                    captureLeft.Start();
                    if (captureLeft.IsOpened)
                    { 
                        Thread x = new Thread(ProcessFrameLeft);
                        x.Start();
                        Thread a = new Thread(findImageLeft);
                        a.Start();
                    }
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }

            if (captureMiddle == null)
            {
                try
                {
                    captureMiddle = new VideoCapture(middleCamIndex);
                    captureMiddle.Start();
                    if (captureMiddle.IsOpened)
                    {
                        Thread x = new Thread(ProcessFrameMiddle);
                        x.Start();
                        Thread a = new Thread(findImageMiddle);
                        a.Start();
                    }

                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }

            if (captureRight == null)
            {
                try
                {
                    captureRight = new VideoCapture(rightCamIndex);
                    captureRight.Start();
                    if (captureRight.IsOpened)
                    {
                        Thread x = new Thread(ProcessFrameRight);
                        x.Start();
                        Thread a = new Thread(findImageRight);
                        a.Start();
                    }
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }
            #endregion
        }


        public void CaptureOrigImage(PictureBox _pictureBox)
        {
            Image<Bgr, byte> iplImage = null;
            string name = "";
            if (_pictureBox.Name.Contains("left"))
            {
                name = "left";
                iplImage = captureLeft.QueryFrame().ToImage<Bgr, byte>();
            }
            if (_pictureBox.Name.Contains("middle"))
            {
                name = "middle";
                iplImage = captureMiddle.QueryFrame().ToImage<Bgr, byte>();
            }
            if (_pictureBox.Name.Contains("right"))
            {
                name = "right";
                iplImage = captureRight.QueryFrame().ToImage<Bgr, byte>();
            }

            //pictureBox2.Image = iplImage.ToBitmap();
            iplImage.ToBitmap().Save(@"C:\Users\chris\Desktop\" + name + "origImage.png", ImageFormat.Png);
        }


        private void findImageLeft()
        {
            while (true)
            {
                //leftGaugePB.Image = Converters.processingImages(EmergencyBrake(), _source);
                //leftGaugePB.Image = Converters.processingImages(LeftTurnSignal(), _source);
                //leftGaugePB.Image = Converters.processingImages(RightTurnSignal(), _source);
                //leftGaugePB.Image = Converters.processingImages(ParkSymbol(), _source);
                //leftGaugePB.Image = Converters.processingImages(ReverseSymbol(), _source);
                //leftGaugePB.Image = Converters.processingImages(NeutralSymbol(), _source);
                //leftGaugePB.Image = Converters.processingImages(DriveSymbol(), _source);
                //leftGaugePB.Image = Converters.processingImages(LightSymbol(), _source);
                //leftGaugePB.Image = Converters.processingImages(SeatBeltSymbol(), _source);

            }
        }

        private void findImageMiddle()
        {
            while (true)
            {
                //middleGaugePB.Image = Converters.processingImages(EmergencyBrake(), _source);
                //middleGaugePB.Image = Converters.processingImages(LeftTurnSignal(), _source);
                //middleGaugePB.Image = Converters.processingImages(RightTurnSignal(), _source);
                //middleGaugePB.Image = Converters.processingImages(ParkSymbol(), _source);
                //middleGaugePB.Image = Converters.processingImages(ReverseSymbol(), _source);
                //middleGaugePB.Image = Converters.processingImages(NeutralSymbol(), _source);
                //middleGaugePB.Image = Converters.processingImages(DriveSymbol(), _source);
                //middleGaugePB.Image = Converters.processingImages(LightSymbol(), _source);
                //middleGaugePB.Image = Converters.processingImages(SeatBeltSymbol(), _source);

            }
        }


        private void findImageRight()
        {
            while (true)
            {
                //rightGaugePB.Image = Converters.processingImages(EmergencyBrake(), _source);
                //rightGaugePB.Image = Converters.processingImages(LeftTurnSignal(), _source);
                //rightGaugePB.Image = Converters.processingImages(RightTurnSignal(), _source);
                //rightGaugePB.Image = Converters.processingImages(ParkSymbol(), _source);
                //rightGaugePB.Image = Converters.processingImages(ReverseSymbol(), _source);
                //rightGaugePB.Image = Converters.processingImages(NeutralSymbol(), _source);
                //rightGaugePB.Image = Converters.processingImages(DriveSymbol(), _source);
                //rightGaugePB.Image = Converters.processingImages(LightSymbol(), _source);
                //rightGaugePB.Image = Converters.processingImages(SeatBeltSymbol(), _source);

            }
        }




        private TemplateObject ebrake;
        private TemplateObject leftTurnSignal;
        private TemplateObject rightTurnSignal;
        private TemplateObject parkSymbol;
        private TemplateObject reverseSymbol;
        private TemplateObject neutralSymbol;
        private TemplateObject driveSymbol;
        private TemplateObject lightSymbol;
        private TemplateObject seatBeltSymbol;


        private TemplateObject EmergencyBrake()
        {
            if (ebrake == null)
            {
                string topic = "EmergencyBrake";

                TemplateObject temp = new TemplateObject();
                temp.filepath = templatePicsPath + topic + ".png"; 
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Red;
                ebrake = temp;
            }
            return ebrake;
        }

        private TemplateObject LeftTurnSignal()
        {
            if (leftTurnSignal == null)
            {
                string topic = "LeftTurnSignal";

                TemplateObject temp = new TemplateObject();
                temp.filepath = templatePicsPath + topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Blue;
                leftTurnSignal = temp;
            }
            return leftTurnSignal;
        }

        private TemplateObject RightTurnSignal()
        {
            if (rightTurnSignal == null)
            {
                string topic = "RightTurnSignal";

                TemplateObject temp = new TemplateObject();
                temp.filepath = templatePicsPath + topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Yellow;
                rightTurnSignal = temp;
            }
            return rightTurnSignal;
        }

        private TemplateObject ParkSymbol()
        {
            if (parkSymbol == null)
            {
                string topic = "ParkSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = templatePicsPath + topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.White;
                parkSymbol = temp;
            }
            return parkSymbol;
        }

        private TemplateObject ReverseSymbol()
        {
            if (reverseSymbol == null)
            {
                string topic = "ReverseSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = templatePicsPath + topic + ".png";
                temp.proMessage = topic + "  = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Green;
                reverseSymbol = temp;
            }
            return reverseSymbol;
        }

        private TemplateObject NeutralSymbol()
        {
            if (neutralSymbol == null)
            {
                string topic = "NeutralSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = templatePicsPath + topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Black;
                neutralSymbol = temp;
            }
            return neutralSymbol;
        }

        private TemplateObject DriveSymbol()
        {
            if (driveSymbol == null)
            {
                string topic = "DriveSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = templatePicsPath + topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Orange;
                driveSymbol = temp;
            }
            return driveSymbol;
        }

        private TemplateObject LightSymbol()
        {
            if (lightSymbol == null)
            {
                string topic = "LightSymbol";
                TemplateObject temp = new TemplateObject();
                temp.filepath = templatePicsPath + topic + ".png";
                temp.proMessage = topic + "  = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Pink;
                lightSymbol = temp;
            }
            return lightSymbol;
        }

        private TemplateObject SeatBeltSymbol()
        {
            if (seatBeltSymbol == null)
            {
                string topic = "SeatBeltSymbol";
                TemplateObject temp = new TemplateObject();
                temp.filepath = templatePicsPath + topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Pink;
                seatBeltSymbol = temp;
            }
            return seatBeltSymbol;
        }
    }
}
