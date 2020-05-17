﻿using Emgu.CV;
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
    public class Start_ScreenCompare
    {
     
        public  void CaptureOrigImage(Bitmap image, string name)
        {
            image.Save(name + "origImage.png", ImageFormat.Png);
        }

         int counter = 0;
        public  void findImage(string name, Bitmap _source, out Bitmap imageToShow, out string message)
        {
            Bitmap temp = null;
            string tempMessage = "";
            try
            {
                if (name == "Left")
                {
                    if (counter == 0)
                    {
                        counter++;
                        Converters.processingImages(EmergencyBrake(), _source, out temp, out tempMessage);
                    }
                    else
                    {
                        counter = 0;
                        Converters.processingImages(LeftTurnSignal(), _source, out temp, out tempMessage);
                    }
                }
                else if (name == "Right")
                {
                    if (counter == 0)
                    {
                        counter++;
                        Converters.processingImages(RightTurnSignal(), _source, out temp, out tempMessage);
                    }
                   else if (counter == 1)
                    {
                        counter++;
                        Converters.processingImages(CheckEngine(), _source, out temp, out tempMessage);
                    }
                    else if (counter == 2)
                    {
                        counter ++;
                        Converters.processingImages(TirePressureSymbol(), _source, out temp, out tempMessage);
                    }
                    else if (counter == 3)
                    {
                        counter = 0;
                        Converters.processingImages(SeatBeltSymbol(), _source, out temp, out tempMessage);
                    }
                    
                }
                else if (name == "BottomMiddle")
                {
                    if (counter == 0)
                    {
                        counter++;
                       // Converters.processingImages(DarkReverseSymbol(), _source, out temp, out tempMessage);

                    }
                    else if (counter == 1)
                    {
                        counter++;
                        Converters.processingImages(LightReverseSymbol(), _source, out temp, out tempMessage);
                    }
                    else if (counter == 2)
                    {
                        counter++;
                        Converters.processingImages(LowLightSymbol(), _source, out temp, out tempMessage);
                    }
                    else if (counter == 3)
                    {
                        counter++;
                        Converters.processingImages(HighBeamLightSymbol(), _source, out temp, out tempMessage);
                    }
                    else if (counter == 4)
                    {
                        counter = 0;
                        Converters.processingImages(GasLightSymbol(), _source, out temp, out tempMessage);
                    }

                }
                if (name == "Reverse")
                {
                    temp = _source;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            message = tempMessage;
            imageToShow = temp;
        }




        private TemplateObject ebrake;
        private TemplateObject leftTurnSignal;
        private TemplateObject rightTurnSignal;
        private TemplateObject parkSymbol;
        private TemplateObject lightReverseSymbol;
        private TemplateObject darkReverseSymbol;
        private TemplateObject neutralSymbol;
        private TemplateObject driveSymbol;
        private TemplateObject lowLightSymbol;
        private TemplateObject seatBeltSymbol;
        private TemplateObject infoSymbol;
        private TemplateObject checkEngineSymbol;
        private TemplateObject highBeamLightSymbol;
        private TemplateObject gasLightSymbol;
        private TemplateObject tirePressureSymbol;


        private TemplateObject TirePressureSymbol()
        {
            if (tirePressureSymbol == null)
            {
                string topic = "TirePressureSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Red;
                tirePressureSymbol = temp;
            }
            return tirePressureSymbol;
        }


        private TemplateObject EmergencyBrake()
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

        private TemplateObject GasLightSymbol()
        {
            if (gasLightSymbol == null)
            {
                string topic = "GasLightSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Red;
                gasLightSymbol = temp;
            }
            return gasLightSymbol;
        }

        private  TemplateObject CheckEngine()
        {
            if (checkEngineSymbol == null)
            {
                string topic = "CheckEngine";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Red;
                checkEngineSymbol = temp;
            }
            return checkEngineSymbol;
        }

        private  TemplateObject InformationSymbol()
        {
            if (infoSymbol == null)
            {
                string topic = "InformationSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + " = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Red;
                infoSymbol = temp;
            }
            return infoSymbol;
        }

        private  TemplateObject LeftTurnSignal()
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

        private  TemplateObject RightTurnSignal()
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

        private  TemplateObject ParkSymbol()
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
        
        private TemplateObject DarkReverseSymbol()
        {
            if (darkReverseSymbol == null)
            {
                string topic = "DarkReverseSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + "  = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Green;
                darkReverseSymbol = temp;
            }
            return darkReverseSymbol;
        }
        private  TemplateObject LightReverseSymbol()
        {
            if (lightReverseSymbol == null)
            {
                string topic = "LightReverseSymbol";

                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + "  = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Green;
                lightReverseSymbol = temp;
            }
            return lightReverseSymbol;
        }

        private  TemplateObject NeutralSymbol()
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

        private  TemplateObject DriveSymbol()
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

        private  TemplateObject LowLightSymbol()
        {
            if (lowLightSymbol == null)
            {
                string topic = "LowLightSymbol";
                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + "  = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Pink;
                lowLightSymbol = temp;
            }
            return lowLightSymbol;
        }

        private  TemplateObject HighBeamLightSymbol()
        {
            if (highBeamLightSymbol == null)
            {
                string topic = "HiBeamLightSymbol";
                TemplateObject temp = new TemplateObject();
                temp.filepath = topic + ".png";
                temp.proMessage = topic + "  = true";
                temp.badMessage = topic + " = false";
                temp.color = Color.Pink;
                highBeamLightSymbol = temp;
            }
            return highBeamLightSymbol;
        }

        private  TemplateObject SeatBeltSymbol()
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
