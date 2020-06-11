using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Interop;
using webCamTest.MonitorBrightness;
using webCamTest.MovingWindows;
using webCamTest.ScreenCompare;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using webCamTest.Gauges;
using webCamTest.MapsEmulator;

namespace webCamTest
{
    public partial class Form1 : Form
    {
        private GaugeSymbols gaugeSymbolForm;
        private int ReversCamera;
        private int LeftGaugeIndex;
        private int BottomMiddleGaugeIndex;
        private int TopMiddleGaugeIndex;

        private int RightGaugeIndex;
        private int morningTime;
        private int eveningTime;
        private string templatePicPath;
        private bool showGauges = true;
        private OpenCvVersion openCvVersionLeft;
        private OpenCvVersion openCvVersionRight;
        private OpenCvVersion openCvVersionBottomMiddle;
       // private OpenCvVersion openCvVersionTopMiddle;

        private OpenCvVersion openCvVersionReverse;
        private Start_MonitorBrightness start_MonitorBrightness;
        private EmulatorScreenCapture emulatorScreenCapture;

        public Form1()
        {
            InitializeComponent();

            InitializeStuff();
           
        }

        private void InitializeStuff()
        {
            this.WindowState = FormWindowState.Maximized;
           

            CaptureLeftGaugeBtn.Visible = false;
            CaptureBottomMiddletGaugeBtn.Visible = false;
            CaptureRightGaugeBtn.Visible = false;

            reverseCamPicBx.Visible = true;

            leftGuagePicBx.Visible = false;
            bottomMiddleGuagePicBx.Visible = false;
            //topMiddleGuagePicBx.Visible = false;
            rightGuagePicBx.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SwitchToGaugesOrReverse();
        }

        private void SwitchToGaugesOrReverse()
        {
            if (showGauges)
            {

                reverseCamPicBx.Visible = false;

                CaptureLeftGaugeBtn.Visible = true;
                CaptureBottomMiddletGaugeBtn.Visible = true;

                CaptureRightGaugeBtn.Visible = true;
                leftGuagePicBx.Visible = true;
                bottomMiddleGuagePicBx.Visible = true;
              //  topMiddleGuagePicBx.Visible = true;

                rightGuagePicBx.Visible = true;
                showGauges = false;
            }
            else
            {

                reverseCamPicBx.Visible = true;

                CaptureLeftGaugeBtn.Visible = false;
                CaptureBottomMiddletGaugeBtn.Visible = false;

                CaptureRightGaugeBtn.Visible = false;
                leftGuagePicBx.Visible = false;
                bottomMiddleGuagePicBx.Visible = false;
              //  topMiddleGuagePicBx.Visible = false;

                rightGuagePicBx.Visible = false;
                showGauges = true;
            }
        }

        Start_MovingWindows movingWindows;
        private void Form1_Load(object sender, EventArgs e)
        {


            //Start Emulator
            emulatorScreenCapture = new EmulatorScreenCapture();
            emulatorScreenCapture.beginProgam();

            ReadSettingsFile();


            //Start Streming Gauges
            SetUpReverseCameraPicBx();
            openCvVersionLeft = new OpenCvVersion("Left", LeftGaugeIndex, leftGuagePicBx, this);
            openCvVersionRight = new OpenCvVersion("Right", RightGaugeIndex, rightGuagePicBx,this);
            openCvVersionBottomMiddle = new OpenCvVersion("BottomMiddle", BottomMiddleGaugeIndex, bottomMiddleGuagePicBx, this);
            openCvVersionReverse = new OpenCvVersion("Reverse", ReversCamera, reverseCamPicBx, this);

            ////Start Brightness Monitoring
            IntPtr windowHandle = this.Handle;
            start_MonitorBrightness = new Start_MonitorBrightness();
            start_MonitorBrightness.StartMonitorBrightness(windowHandle, morningTime, eveningTime);

            //Start GaugeSymbol Window
            gaugeSymbolForm = new GaugeSymbols();
            gaugeSymbolForm.Show();


            //Moving Windows
            movingWindows = new Start_MovingWindows();
            movingWindows.StartMovingWindows();
            movingWindows.emulatorName = emulatorScreenCapture.emulatorName;

            Thread thread = new Thread(SendMessagesAndUpdateGUI);
            thread.Start();


        }
      
        private void CheckReverseSymbol()
        {
            if (openCvVersionBottomMiddle.message != null && openCvVersionBottomMiddle.message.Contains("ReverseSymbol"))
            {
                if (openCvVersionBottomMiddle.message.Contains("true"))
                {
                    if (this.TopMost != true)
                    {
                        this.Invoke((Action)delegate
                        {
                            //SwitchToGaugesOrReverse();
                            this.TopMost = true;
                            this.BringToFront();                        
                        });
                    }
                }
                else if (openCvVersionBottomMiddle.message.Contains("false"))
                {
                    if (this.TopMost != false)
                    {
                        this.Invoke((Action)delegate
                        {
                            // SwitchToGaugesOrReverse();
                            this.TopMost = false;
                            this.SendToBack();
                        });
                    }
                }
            }
        }
    

        private void SendMessagesAndUpdateGUI()
        {
            this.Invoke((Action)delegate
            {
                // SwitchToGaugesOrReverse();
                this.TopMost = false;
                this.SendToBack();
            });

            while (true)
            {
                try
                { 
                    string sendMes = openCvVersionLeft.message + " , " + openCvVersionRight.message + " , " +
                                                                                                       openCvVersionBottomMiddle.message + " , "  + start_MonitorBrightness.msg;
                    if (openCvVersionBottomMiddle.message.Contains("LowLightSymbol"))
                    {
                        if (openCvVersionBottomMiddle.message.Contains("true"))
                        {
                            openCvVersionBottomMiddle.lightsOn = true;
                        }
                        else
                        {
                            openCvVersionBottomMiddle.lightsOn = false;
                        }
                    }

                    gaugeSymbolForm.SetLabels(sendMes);
                    if (openCvVersionBottomMiddle.croppedBitmap != null)
                    {
                        gaugeSymbolForm.SetBitmap(openCvVersionBottomMiddle.croppedBitmap);
                    }

                    if (emulatorScreenCapture.finalBitmap != null)
                    {
                        Bitmap x = (Bitmap) emulatorScreenCapture.finalBitmap.Clone();
                        gaugeSymbolForm.SetBitmapForMap(x);
                    }


                        bool isVisible = leftGuagePicBx.Visible;
                    if ( isVisible && openCvVersionLeft.finalImage != null)
                    {

                        leftGuagePicBx.Invoke((Action)delegate
                        {
                            leftGuagePicBx.Image = openCvVersionLeft.finalImage;

                        });

                        rightGuagePicBx.Invoke((Action)delegate
                        {
                            rightGuagePicBx.Image = openCvVersionRight.finalImage;

                        });
                  
                        bottomMiddleGuagePicBx.BeginInvoke((Action)delegate
                        {
                            bottomMiddleGuagePicBx.Image = openCvVersionBottomMiddle.finalImage;

                        });
                      
                       Thread.Sleep(10);
                    }
                    else
                    {

                      
                        reverseCamPicBx.Invoke((Action)delegate
                        {
                            reverseCamPicBx.Image = openCvVersionReverse.finalImage;
                        });
                        Thread.Sleep(10);


                    }
               
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }

                CheckReverseSymbol();
            }
        }
      
        public byte[] Combine(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
            return bytes;
        }

        private void SetUpReverseCameraPicBx()
        {
            reverseCamPicBx.Location = new Point(0, 31);
            reverseCamPicBx.SizeMode = PictureBoxSizeMode.StretchImage;
            reverseCamPicBx.Width = this.Width;
            reverseCamPicBx.Height = this.Height;
            reverseCamPicBx.BackColor = Color.Green;
        }


        private void ReadSettingsFile()
        {
            string[] lines = File.ReadAllLines("Settings.txt");
            foreach(string line in lines)
            {
                string[] result = line.Split('=');

                if(line.Contains("Reverse"))
                {
                    ReversCamera = Convert.ToInt32(result[1]);
                }
                else if (line.Contains("Left"))
                {
                    LeftGaugeIndex = Convert.ToInt32(result[1]);
                }
                else if (line.Contains("BottomMiddle"))
                {
                    BottomMiddleGaugeIndex = Convert.ToInt32(result[1]);
                }
                else if (line.Contains("TopMiddle"))
                {
                    TopMiddleGaugeIndex = Convert.ToInt32(result[1]);
                }
                else if (line.Contains("Right"))
                {
                    RightGaugeIndex = Convert.ToInt32(result[1]);
                }
                else if (line.Contains("Template"))
                {
                    templatePicPath = result[1];
                }
                else if (line.Contains("Morning"))
                {
                    morningTime = Convert.ToInt32(result[1]);
                }
                else if (line.Contains("Evening"))
                {
                    eveningTime = Convert.ToInt32(result[1]);
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
          
            leftGuagePicBx.SizeMode = PictureBoxSizeMode.StretchImage;
            leftGuagePicBx.Height = control.Size.Height;
            leftGuagePicBx.Width = control.Size.Width / 3;

            bottomMiddleGuagePicBx.Location = new Point(control.Size.Width / 3, 31);
            bottomMiddleGuagePicBx.SizeMode = PictureBoxSizeMode.StretchImage;
            bottomMiddleGuagePicBx.Height = control.Size.Height;
            bottomMiddleGuagePicBx.Width = control.Size.Width / 3;
            bottomMiddleGuagePicBx.BackColor = Color.Black;


            //topMiddleGuagePicBx.Location = new Point(control.Size.Width / 3, 31);
            //topMiddleGuagePicBx.SizeMode = PictureBoxSizeMode.StretchImage;
            //topMiddleGuagePicBx.Height = control.Size.Height / 2;
            //topMiddleGuagePicBx.Width = control.Size.Width / 3;
            //topMiddleGuagePicBx.BackColor = Color.Blue;


            rightGuagePicBx.Location = new Point((control.Size.Width / 3) * 2, 31);
            rightGuagePicBx.SizeMode = PictureBoxSizeMode.StretchImage;
            rightGuagePicBx.Height = control.Size.Height;
            rightGuagePicBx.Width = control.Size.Width / 3;
            rightGuagePicBx.BackColor = Color.Red;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openCvVersionLeft.takePic = true;
        }

        private void CaptureRightGaugeBtn_Click(object sender, EventArgs e)
        {
            openCvVersionRight.takePic = true;
        }
   
        private void CaptureBottomMiddletGaugeBtn_Click(object sender, EventArgs e)
        {
            openCvVersionBottomMiddle.takePic = true;
        }

        bool isMapOn = false;
        private void MapsButton_Click(object sender, EventArgs e)
        {
            if(isMapOn)
            {
                gaugeSymbolForm.TurnMapOn(isMapOn);
                isMapOn = false;
            }
            else
            {
                gaugeSymbolForm.TurnMapOn(isMapOn);
                isMapOn = true;
            }

        }
    }
}
