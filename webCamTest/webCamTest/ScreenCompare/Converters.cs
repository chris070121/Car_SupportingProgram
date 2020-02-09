using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webCamTest
{
    public static class Converters
    {
        public static Bitmap processingImages(TemplateObject temp, Image<Bgr, byte> source)
        {
            Image<Bgr, byte> imageToShow = new Image<Bgr, byte>(@"C:\Users\chris\Desktop\origImage.png");
            if (temp != null)
            {
                if (source != null)
                {
                    Image<Bgr, byte> template = new Image<Bgr, byte>(temp.filepath);
                    imageToShow = source.Copy();

                    using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
                    {
                        double[] minValues, maxValues;
                        System.Drawing.Point[] minLocations, maxLocations;
                        result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                        // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                        if (maxValues[0] > 0.8)
                        {
                            // This is a match. Do something with it, for example draw a rectangle around it.
                            Rectangle match = new Rectangle(maxLocations[0], template.Size);
                            imageToShow.Draw(match, new Bgr(temp.color), 3);
                            Console.WriteLine(temp.proMessage);
                        }
                        else
                        {
                            Console.WriteLine(temp.badMessage);
                        }
                    }
                }
            }
            return imageToShow.ToBitmap();

        }
    }
}
