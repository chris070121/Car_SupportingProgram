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
    //public  class CropAndSendBitmap
    //{
    //     TcpClient client = null;
    //     int port = 9997;
    //    public  void SendBitmap(Bitmap bitmap)
    //    {
    //        if (client != null && client.Connected && bitmap != null)
    //        {
    //            try
    //            {
    //                NetworkStream nwStream = client.GetStream();
    //                MemoryStream ms = new MemoryStream();
    //                bitmap.Save(ms, ImageFormat.Png);
    //                nwStream.Write(ms.GetBuffer(), 0, (int)ms.Length);

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

    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine(ex.Message);
    //            }
    //        }
    //    }


    //}


    public static class CropAndSendBitmap
    {
        static TcpClient client = null;
        static int port = 9997;
         public static byte[] message;
        public static void SendBitmap(Bitmap bitmap)//, byte[] message)
        {
            if (client != null && client.Connected && bitmap != null)
            {
                try
                {
                    NetworkStream nwStream = client.GetStream();
                    MemoryStream ms = new MemoryStream();
                    bitmap.Save(ms, ImageFormat.Png);

                  byte[] temp = Combine(ms.ToArray(), message);

                    nwStream.Write(temp, 0, (int)temp.Length);

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
        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
            return bytes;
        }

    }
}
