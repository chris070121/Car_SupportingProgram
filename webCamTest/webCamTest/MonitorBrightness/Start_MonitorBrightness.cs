using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace webCamTest.MonitorBrightness
{
    class Start_MonitorBrightness
    {
        private TimeSpan morning = new TimeSpan(6, 0, 0); //10 o'clock
        private TimeSpan night = new TimeSpan(17, 0, 0); //12 o'clock
        public void StartMonitorBrightness(IntPtr windowHandle, int morningTime, int eveningTime)
        {
            morning = new TimeSpan(morningTime, 0, 0); //10 o'clock
            TimeSpan night = new TimeSpan(eveningTime, 0, 0); //12 o'clock
            
            BrightnessController br = new BrightnessController(windowHandle);
            TcpClient client = new TcpClient();
            NetworkStream stream = null;
            try
            {
                client = new TcpClient("127.0.0.1", 9997);
                stream = client.GetStream();
            }
            catch (Exception ey)
            {
                Console.WriteLine("***********" + ey.Message);
            }

            
            TimeSpan now = DateTime.Now.TimeOfDay;
            bool done = false;

            while (done == false)
            {
                if (!client.Connected)
                {
                    try
                    {
                        client = new TcpClient("127.0.0.1", 9997);
                        stream = client.GetStream();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("***********" + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        if ((now > morning) && (now < night))
                        {

                            string msg = "BrightnessUp";
                            int size = Encoding.ASCII.GetBytes(msg).Length;
                            stream.Write(Encoding.ASCII.GetBytes(msg), 0, size);
                            stream.Close();
                            br.SetBrightness(100);
                            done = true;
                        }
                        else
                        {
                            string msg = "BrightnessDown";
                            int size = Encoding.ASCII.GetBytes(msg).Length;
                            stream.Write(Encoding.ASCII.GetBytes(msg), 0, size);
                            stream.Close();
                            br.SetBrightness(0);

                            done = true;
                        }

                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x.Message);
                    }
                }

                //10 minutes
                Thread.Sleep(600000);
            }
        }
    }
}
   