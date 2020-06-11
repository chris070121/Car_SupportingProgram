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
        private IntPtr windowHandle;
        public string msg;

        public void StartMonitorBrightness(IntPtr _windowHandle, int morningTime, int eveningTime)
        {
            morning = new TimeSpan(morningTime, 0, 0); //10 o'clock
            night = new TimeSpan(eveningTime, 0, 0); //12 o'clock
            windowHandle = _windowHandle;

            Thread monitor = new Thread(MonitorBrightnessProcedure);
            monitor.Start();
        }

        private void MonitorBrightnessProcedure()
        {
            BrightnessController br = new BrightnessController(windowHandle);
            TimeSpan now = DateTime.Now.TimeOfDay;

            while (true)
            {
                for (int i = 0; i < 50; i++)
                {
                    try
                    {
                        if ((now > morning) && (now < night))
                        {
                            msg = "BrightnessUp = true";
                            br.SetBrightness(100);
                        }
                        else
                        {
                            msg = "BrightnessUp = false";
                            br.SetBrightness(0);
                        }
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x.Message);
                    }
                }
                //1 minutes
                Thread.Sleep(5000);
            }
        }
    }
}
   