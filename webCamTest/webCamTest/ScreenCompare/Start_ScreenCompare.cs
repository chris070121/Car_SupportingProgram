using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using webCamTest.ScreenCompare;

namespace webCamTest.ScreenCompare
{
    public static class Start_ScreenCompare
    {
     

        public static void CaptureOrigImage(Bitmap image, string name)
        {
            image.Save(name + "origImage.png", ImageFormat.Png);
        }

        static int counter = 0;
        public static void findImage(string name, Bitmap _source, out Bitmap imageToShow, out string message)
        {
            //if (name == "Left")
            //{
                if (counter == 0)
                {
                    counter++;
                    Converters.processingImages(EmergencyBrake(), _source, out imageToShow, out message);
                    return;
                }
                else 
                {
                    counter = 0;
                    Converters.processingImages(LeftTurnSignal(), _source, out imageToShow, out message);
                    return;

                }
            //}
            //if (name == "Right")
            //{
            //    if (counter == 0)
            //    {
            //        counter++;
            //        return Converters.processingImages(RightTurnSignal(), _source);
            //    }
            //    else
            //    {
            //        counter = 0;
            //        return Converters.processingImages(SeatBeltSymbol(), _source);
            //    }

            //}
            //if (name == "Middle")
            //{
            //    if (counter == 0)
            //    {
            //        counter++;
            //        return Converters.processingImages(ParkSymbol(), _source);
            //    }
            //    else if (counter == 1)
            //    {
            //        counter++;
            //        return Converters.processingImages(ReverseSymbol(), _source);
            //    }
            //    else if (counter == 2)
            //    {
            //        counter++;
            //        return Converters.processingImages(NeutralSymbol(), _source);
            //    }
            //    else if (counter == 3)
            //    {
            //        counter++;
            //        return Converters.processingImages(DriveSymbol(), _source);
            //    }
            //    else if (counter == 4)
            //    {
            //        counter = 0;
            //        return Converters.processingImages(LightSymbol(), _source);
            //    }
            //}
        
            // return _source;
           
        }




        private static  TemplateObject ebrake;
        private static  TemplateObject leftTurnSignal;
        private static  TemplateObject rightTurnSignal;
        private static  TemplateObject parkSymbol;
        private static  TemplateObject reverseSymbol;
        private static  TemplateObject neutralSymbol;
        private static  TemplateObject driveSymbol;
        private static  TemplateObject lightSymbol;
        private static  TemplateObject seatBeltSymbol;


        private static TemplateObject EmergencyBrake()
        {
            if (ebrake == null)
            {
                string topic = "EmergencyBrake";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png"; 
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Red;
                ebrake = temp;
            }
            return ebrake;
        }

        private static TemplateObject LeftTurnSignal()
        {
            if (leftTurnSignal == null)
            {
                string topic = "LeftTurnSignal";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Blue;
                leftTurnSignal = temp;
            }
            return leftTurnSignal;
        }

        private static TemplateObject RightTurnSignal()
        {
            if (rightTurnSignal == null)
            {
                string topic = "RightTurnSignal";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Yellow;
                rightTurnSignal = temp;
            }
            return rightTurnSignal;
        }

        private static TemplateObject ParkSymbol()
        {
            if (parkSymbol == null)
            {
                string topic = "ParkSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.White;
                parkSymbol = temp;
            }
            return parkSymbol;
        }

        private static TemplateObject ReverseSymbol()
        {
            if (reverseSymbol == null)
            {
                string topic = "ReverseSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + "  = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Green;
                reverseSymbol = temp;
            }
            return reverseSymbol;
        }

        private static TemplateObject NeutralSymbol()
        {
            if (neutralSymbol == null)
            {
                string topic = "NeutralSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Black;
                neutralSymbol = temp;
            }
            return neutralSymbol;
        }

        private static TemplateObject DriveSymbol()
        {
            if (driveSymbol == null)
            {
                string topic = "DriveSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Orange;
                driveSymbol = temp;
            }
            return driveSymbol;
        }

        private static TemplateObject LightSymbol()
        {
            if (lightSymbol == null)
            {
                string topic = "LightSymbol";
                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + "  = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Pink;
                lightSymbol = temp;
            }
            return lightSymbol;
        }

        private static TemplateObject SeatBeltSymbol()
        {
            if (seatBeltSymbol == null)
            {
                string topic = "SeatBeltSymbol";
                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Pink;
                seatBeltSymbol = temp;
            }
            return seatBeltSymbol;
        }
    }
}
