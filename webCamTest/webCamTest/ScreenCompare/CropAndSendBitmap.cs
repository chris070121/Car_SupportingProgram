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
 
    public static class CropAndSendBitmap
    {
        static TcpClient client = null;
        static int port = 9997;
         public static byte[] message;
        public static Bitmap bitmap;
       
    }
}
