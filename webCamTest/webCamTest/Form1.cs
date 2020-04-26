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

namespace webCamTest
{
    public partial class Form1 : Form
    {
        private Gauges.GaugeInfo gaugeInfoForm;
        private Rectangle workingRectangle;
        private int ReversCamera = 0;
        private int LeftGaugeIndex = 1;
        private int MiddleGaugeIndex = 2;
        private int RightGaugeIndex = 3;
        private int morningTime;
        private int eveningTime;
        private string templatePicPath;
        private bool showGauges = false;
        private OpenCvVersion openCvVersionLeft;
        private OpenCvVersion openCvVersionRight;
        private OpenCvVersion openCvVersionMiddle;
        private OpenCvVersion openCvVersionReverse;
        private Start_MonitorBrightness start_MonitorBrightness;

        public Form1()
        {
            InitializeComponent();

            InitializeStuff();
           
        }

        private void InitializeStuff()
        {
            CaptureLeftGaugeBtn.Visible = false;
            CaptureMiddletGaugeBtn.Visible = false;
            CaptureRightGaugeBtn.Visible = false;

            reverseCamPicBx.Visible = true;

            leftGuagePicBx.Visible = false;
            middleGuagePicBx.Visible = false;
            rightGuagePicBx.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(showGauges)
            {
                this.WindowState = FormWindowState.Maximized;

                reverseCamPicBx.Visible = false;

                CaptureLeftGaugeBtn.Visible = true;
                CaptureMiddletGaugeBtn.Visible = true;
                CaptureRightGaugeBtn.Visible = true;
                leftGuagePicBx.Visible = true;
                middleGuagePicBx.Visible = true;
                rightGuagePicBx.Visible = true;
                showGauges = false;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;

                reverseCamPicBx.Visible = true;
                
                CaptureLeftGaugeBtn.Visible = false;
                CaptureMiddletGaugeBtn.Visible = false;
                CaptureRightGaugeBtn.Visible = false;
                leftGuagePicBx.Visible = false;
                middleGuagePicBx.Visible = false;
                rightGuagePicBx.Visible = false;
                showGauges = true;
            }
        }

        Start_MovingWindows movingWindows;
        private void Form1_Load(object sender, EventArgs e)
        {

            workingRectangle = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(295, 0);
            this.Size = new Size((workingRectangle.Width - 275), workingRectangle.Height + 10);


            ////Moving Windows
            //movingWindows = new Start_MovingWindows();
            //movingWindows.StartMovingWindows();

            ReadSettingsFile();

            //Start Streming Gauges
            SetUpReverseCameraPicBx();
            openCvVersionLeft = new OpenCvVersion("Left", LeftGaugeIndex, leftGuagePicBx, this);
            openCvVersionRight = new OpenCvVersion("Right", RightGaugeIndex, rightGuagePicBx,this);
            openCvVersionMiddle = new OpenCvVersion("Middle", MiddleGaugeIndex, middleGuagePicBx, this);
            openCvVersionReverse = new OpenCvVersion("Reverse", ReversCamera, reverseCamPicBx, this);

            ////Start Brightness Monitoring
            IntPtr windowHandle = this.Handle;
            start_MonitorBrightness = new Start_MonitorBrightness();
            start_MonitorBrightness.StartMonitorBrightness(windowHandle, morningTime, eveningTime);

          
            Thread thread = new Thread(SendMessagesAndUpdateGUI);
            thread.Start();

        }
        Gauges.GaugeInfo gaugeInfo;
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);
        IntPtr windowHandle;
        private void CheckReverseSymbol()
        {
                if (openCvVersionMiddle.message != null && openCvVersionMiddle.message.Contains("ReverseSymbol"))
                {
                    if (openCvVersionMiddle.message.Contains("true"))
                    {
                       
                       SetWindowPos(windowHandle, (IntPtr)SpecialWindowHandles.HWND_TOP, 300, 0, (workingRectangle.Width / 2) + 420, workingRectangle.Height, SetWindowPosFlags.SWP_SHOWWINDOW);
                        
                    }
                    else if (openCvVersionMiddle.message.Contains("false"))
                    {
                        SetWindowPos(windowHandle, (IntPtr)SpecialWindowHandles.HWND_BOTTOM, 300, 0, (workingRectangle.Width / 2) + 420, workingRectangle.Height, SetWindowPosFlags.SWP_SHOWWINDOW);
                    }
                }
            
        }
    

        private void SendMessagesAndUpdateGUI()
        {
            while (true)
            {
                try
                {
                    string sendMes = openCvVersionLeft.message + " , " + openCvVersionRight.message + " , " +
                                                                                                       openCvVersionMiddle.message + " , " + start_MonitorBrightness.msg;
                    byte[] bytesMiddle = ASCIIEncoding.ASCII.GetBytes(sendMes);
                    message = bytesMiddle;
                    if (openCvVersionMiddle.croppedBitmap != null)
                    {
                      //  bitmap = openCvVersionMiddle.croppedBitmap;

                    }
                  // SendBitmap();


                    bool isVisible = leftGuagePicBx.Visible;
                    if ( isVisible && openCvVersionLeft.finalImage != null)
                    {

                        leftGuagePicBx.Invoke((Action)delegate
                        {
                            leftGuagePicBx.Image = openCvVersionLeft.finalImage;
                            leftGuagePicBx.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                        });

                        rightGuagePicBx.Invoke((Action)delegate
                        {
                            rightGuagePicBx.Image = openCvVersionRight.finalImage;
                            rightGuagePicBx.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                        });

                     
                        middleGuagePicBx.BeginInvoke((Action)delegate
                        {
                            middleGuagePicBx.Image = openCvVersionMiddle.finalImage;
                            middleGuagePicBx.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                        });
                      
                       Thread.Sleep(100);
                    }
                    else
                    {
                        reverseCamPicBx.Invoke((Action)delegate
                        {
                            reverseCamPicBx.Image = openCvVersionReverse.finalImage;
                        });
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }

               // CheckReverseSymbol();
            }
        }
        
        static TcpClient client = null;
        static int port = 9997;
        public byte[] message;
        public Bitmap bitmap;
        public void SendBitmap()
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
                 
                   Thread.Sleep(100);
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
                if (line.Contains("Left"))
                {
                    LeftGaugeIndex = Convert.ToInt32(result[1]);
                }
                if (line.Contains("Middle"))
                {
                    MiddleGaugeIndex = Convert.ToInt32(result[1]);
                }
                if (line.Contains("Right"))
                {
                    RightGaugeIndex = Convert.ToInt32(result[1]);
                }
                if (line.Contains("Template"))
                {
                    templatePicPath = result[1];
                }
                if (line.Contains("Morning"))
                {
                    morningTime = Convert.ToInt32(result[1]);
                }
                if (line.Contains("Evening"))
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

            middleGuagePicBx.Location = new Point(control.Size.Width / 3, 31);
            middleGuagePicBx.SizeMode = PictureBoxSizeMode.StretchImage;
            middleGuagePicBx.Height = control.Size.Height;
            middleGuagePicBx.Width = control.Size.Width / 3;
            middleGuagePicBx.BackColor = Color.Blue;
          
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

        private void CaptureMiddletGaugeBtn_Click(object sender, EventArgs e)
        {
            openCvVersionMiddle.takePic = true;
        }

        private void CaptureRightGaugeBtn_Click(object sender, EventArgs e)
        {
            openCvVersionRight.takePic = true;
        }
        public enum SpecialWindowHandles
        {
            // ReSharper disable InconsistentNaming
            /// <summary>
            ///     Places the window at the top of the Z order.
            /// </summary>
            HWND_TOP = 0,
            /// <summary>
            ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
            /// </summary>
            HWND_BOTTOM = 1,
            /// <summary>
            ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
            /// </summary>
            HWND_TOPMOST = -1,
            /// <summary>
            ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
            /// </summary>
            HWND_NOTOPMOST = -2
            // ReSharper restore InconsistentNaming
        }
        [Flags]
        public enum SetWindowPosFlags : uint
        {
            // ReSharper disable InconsistentNaming

            /// <summary>
            ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000,

            /// <summary>
            ///     Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            ///     Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            ///     Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            ///     Retains the current position (ignores X and Y parameters).
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            ///     Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            ///     Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = 0x0200,

            /// <summary>
            ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            ///     Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            ///     Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,

            // ReSharper restore InconsistentNaming
        }
    }
}
