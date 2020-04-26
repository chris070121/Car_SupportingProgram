using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace webCamTest
{
    public static class Converters
    {
        public static void processingImages(TemplateObject templateObject, Bitmap source, out Bitmap imageToShow, out string message, out Bitmap croppedImage)
        {
                Image<Bgr, byte> temp = new Image<Bgr, byte>(source);
                Image<Bgr, byte> _croppedImage = null;
                string messageTemp = "";
                if (temp != null)
                {
               
                    if (source != null && File.Exists(templateObject.filepath))
                    {

                        Image<Bgr, byte> template = new Image<Bgr, byte>(templateObject.filepath);


                        using (Image<Gray, float> result = temp.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
                        {
                            double[] minValues, maxValues;
                            System.Drawing.Point[] minLocations, maxLocations;
                            result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                            if (templateObject.filepath.Contains("Information"))
                            {
                                if (maxValues[0] >= .7)
                                {
                                    // This is a match. Do something with it, for example draw a rectangle around it.
                                    Rectangle match = new Rectangle(maxLocations[0], template.Size);
                                    Rectangle croppedrectangle = new Rectangle(match.X, match.Y + 50, match.Width + 200, match.Height - 350);
                                    _croppedImage = new Image<Bgr, byte>(CropImage(croppedrectangle, temp.ToBitmap()));
                                    //temp.Draw(croppedrectangle, new Bgr(templateObject.color), 3);
                                    messageTemp = templateObject.proMessage;
                                }
                                else
                                {
                                    messageTemp = templateObject.badMessage;
                                }
                            }
                            else
                            {
                                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                                if (maxValues[0] >= .8)
                                {
                                    // This is a match. Do something with it, for example draw a rectangle around it.
                                    Rectangle match = new Rectangle(maxLocations[0], template.Size);
                                    temp.Draw(match, new Bgr(templateObject.color), 3);
                                    messageTemp = templateObject.proMessage;
                                }
                                else
                                {
                                    messageTemp = templateObject.badMessage;
                                }
                            }
                        }
                    }


                    else
                    {
                        messageTemp = templateObject.badMessage;
                    }
                
               
            }
                message = messageTemp;
                imageToShow = temp.ToBitmap();
                if (_croppedImage != null)
                {
                    croppedImage = _croppedImage.ToBitmap();
                }
                else
                {
                    croppedImage = null;
                }
            
        }

        private static Bitmap CropImage(Rectangle croppedRect, Bitmap origBitmap)
        {
            int width =Math.Abs(croppedRect.Width);
            int heigh = Math.Abs(croppedRect.Height);
            Bitmap nb = new Bitmap(width,heigh);
            Graphics g = Graphics.FromImage(nb);
            g.DrawImage(origBitmap, croppedRect.X -175, croppedRect.Y-75, croppedRect, GraphicsUnit.Pixel);
            return nb;

        }
    }
}
