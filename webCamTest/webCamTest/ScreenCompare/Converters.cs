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
        public static void processingImages(TemplateObject templateObject, Bitmap source, out Bitmap imageToShow, out string message)
        {
            Image<Bgr, byte> temp = new Image<Bgr, byte>(source);
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
                else
                {
                    messageTemp = templateObject.badMessage;
                }
            }
            message = messageTemp;
            imageToShow = temp.ToBitmap();

        }
    }
}
