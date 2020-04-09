using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace webCamTest.ScreenCompare
{
    //public class CropAndSendBitmap
    //{
    //    TcpClient client = null;
    //    NetworkStream nwStream = null;
    //    int port = 9997;
    //    public void  SendBitmap(Bitmap bitmap)
    //    {
    //        if (client != null && client.Connected && bitmap != null)
    //        {
    //            try
    //            {
    //                nwStream = client.GetStream();
    //                MemoryStream ms = new MemoryStream();
    //                bitmap.Save(ms, ImageFormat.Png);
    //                nwStream.Write(ms.GetBuffer(), 0, (int)ms.Position);

    //                ms.Close();
    //                nwStream.Close();
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine(ex.Message);
    //            }
    //        }
    //        else
    //        {
    //            try
    //            {
    //                client = new TcpClient("127.0.0.1", port);
    //                nwStream = client.GetStream();

    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine(ex.Message);
    //            }
    //        }
    //    }



        public static class CropAndSendBitmap
        {
        static TcpClient client = null;
        static int port = 9997;
        public static void SendBitmap(Bitmap bitmap)
            {
                if (client != null && client.Connected && bitmap != null)
                {
                    try
                    {
                        NetworkStream nwStream = client.GetStream();
                        MemoryStream ms = new MemoryStream();
                        bitmap.Save(ms, ImageFormat.Png);
                        nwStream.Write(ms.GetBuffer(), 0, (int)ms.Position);

                        ms.Close();
                        nwStream.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        client = new TcpClient("127.0.0.1", port);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }


        }
    }
