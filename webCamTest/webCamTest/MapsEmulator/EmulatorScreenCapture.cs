using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace webCamTest.MapsEmulator
{
    class EmulatorScreenCapture
    {
        public Bitmap finalBitmap;
        int ExitCode;
        private static ProcessStartInfo ProcessInfo;
        private static Process Process;
        private static Process Process1;
        static TelnetConnection tc;
        public IntPtr hWnd;
        public string emulatorName = "Pixel_3a_API_25";//"Nexus_10_API_25";
        private string gpsComPort = "COM8";

        public void beginProgam()
        {
            Thread oThreadone = new Thread(StartParseNMEA);
            StartEmulator();
            Thread captureScreen = new Thread(StartCapturingScreen);
            captureScreen.Start();
            oThreadone.Start();
        }

        bool gotHandle = false;
        private void StartCapturingScreen()
        {
            while (true)
            {
                Bitmap x = createBitmap();
                if (x != null)
                {
                    finalBitmap = (Bitmap)x.Clone();
                }
                Thread.Sleep(10);
            }

        }


        private Bitmap createBitmap()
        {
            Bitmap bmp = null;
            IntPtr hdcFrom = GetDC(hWnd);
            IntPtr hdcTo = CreateCompatibleDC(hdcFrom);
            //X and Y coordinates of window
            RECT Rect = new RECT();
            GetWindowRect(hWnd, ref Rect);
            //Find the height and width of the process
            int Width = Rect.right - Rect.left;
            int Height = Rect.bottom - Rect.top;
            IntPtr hBitmap = CreateCompatibleBitmap(hdcFrom, Width-150, Height-50);
            if (hBitmap != IntPtr.Zero)
            {
                // adjust and copy
                IntPtr hLocalBitmap = SelectObject(hdcTo, hBitmap);
                BitBlt(hdcTo, 0, 0, Width, Height,
                    hdcFrom, 0, 30, SRCCOPY);
                SelectObject(hdcTo, hLocalBitmap);
                //We delete the memory device context.
                DeleteDC(hdcTo);
                //We release the screen device context.
                ReleaseDC(hWnd, hdcFrom);
                //Image is created by Image bitmap handle and assigned to Bitmap variable.
                bmp = System.Drawing.Image.FromHbitmap(hBitmap);
                //Delete the compatible bitmap object. 
                DeleteObject(hBitmap);
            }
            return bmp;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        public const int SRCCOPY = 13369376;
        public const int WM_CLICK = 0x00F5;
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT Rect);
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "GetDC")]
        internal extern static IntPtr GetDC(IntPtr hWnd);
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        internal extern static IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        internal extern static IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        internal extern static IntPtr DeleteDC(IntPtr hDc);
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        internal extern static IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        internal extern static bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, int RasterOp);
        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        internal extern static IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        internal extern static IntPtr DeleteObject(IntPtr hDc);
        [DllImport("user32.dll")]
        public static extern int SendMessage(
              int hWnd,      // handle to destination window
              uint Msg,       // message
              long wParam,  // first message parameter
              long lParam   // second message parameter
              );

    




        private SerialPort _port;
        private byte[] _buffer;

        public void StartParseNMEA()
        {
            //Set serial-port
            _port = new SerialPort();
            _port.PortName = gpsComPort;
            _port.BaudRate = 4800;

            _port.Open();
            while (_port.IsOpen)
            {
                //_buffer = new byte[_port.BytesToRead];
                //_port.Read(_buffer, 0, _buffer.Length);
                string x = _port.ReadLine();
                ////Parse buffer
                //string sdata = "";
                //Encoding encoding = ASCIIEncoding.GetEncoding(1252);
                //if (null != _buffer)
                //{
                //    sdata = encoding.GetString(_buffer);
                //}
                string[] string_array = x.Split('$');
                string Gpgga = null;
                for (int i = 0; i < string_array.Length; i++)
                {
                    string stringTemp = string_array[i];
                    string[] line_array = stringTemp.Split(',');
                    if (line_array[0] == "GPGGA")
                    {
                        Gpgga = string.Join(",", string_array[i]);
                    }
                }
                LastGpgga = Gpgga;
               Thread.Sleep(100);
                portOpen = _port.IsOpen;

                if (!String.IsNullOrEmpty(LastGpgga))
                {
                    string latitude = null;
                    string longitude = null;
                    try
                    {
                        string[] line_array = LastGpgga.Split(',');
                        double degrees1 = Convert.ToDouble(line_array[4].Substring(0, 3));
                        double minutes1 = Convert.ToDouble(line_array[4].Substring(3, 6));
                        longitude = Math.Round(degrees1 + (minutes1 / 60.0), 6).ToString();
                        longitude = longitude.Insert(0, "-");

                        double degrees = Convert.ToDouble(line_array[2].Substring(0, 2));
                        double minutes = Convert.ToDouble(line_array[2].Substring(2, 7));
                        latitude = Math.Round(degrees + (minutes / 60.0), 6).ToString();

                        Write("geo fix " + longitude + " " + latitude);// - 122.084 37.40" + i + " ");
                        Read();



                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                        //Console.WriteLine("PORT OPEN =  " + portOpen);
                        //Console.WriteLine("ERROR =  " + longitude + " " + latitude);
                    }
                }

            }

        }

        public string LastGpgga { get; set; }
        public bool portOpen { get; set; }



        private void StartEmulator()
        {
            string command = "emulator -avd " + emulatorName;
            ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            Process = Process.Start(ProcessInfo);

            while (gotHandle == false)
            {
                //Process[] headUnitProcess = Process.GetProcesses();// ("emulator");//("Android Emulator - " + emulatorName + ":5554");
                foreach (Process pList in Process.GetProcesses())
                {
                    if (pList.MainWindowTitle.Contains("Android Emulator - " + emulatorName + ":5554"))
                    {
                        hWnd = pList.MainWindowHandle;
                        gotHandle = true;
                    }
                }
            }

            // defaultHostName is host name to use if one is not specified on the command line.

            string defaultHostName = "localhost";

            string hostName = defaultHostName;

            try

            {

                tc = new TelnetConnection();

                tc.ReadTimeout = 10000; // 10 sec

                // open socket on hostName, which can be an IP address, or use host name (e.g. "A-N9912A-22762") used in lieu of IPaddress

                tc.Open(hostName);

                if (tc.IsOpen)

                {

                    //Start your program here
                    Read();
                    Write("auth t91IYEPHUHZcejDr");
                    Read();


                }

                else

                {

                    Console.WriteLine("Error opening " + hostName);

                    return;

                }

                //FieldFox Programming Guide 5

            }

            catch (Exception e)

            {

                Console.WriteLine(e.ToString());

                return;

            }

            // exit normally.

            return;

        }

        /// <summary>

        /// Write a SCPI command to the telnet connection.

        /// If the command has a '?', then read back the response and print

        /// it to the Console.

        /// </summary>

        /// <remarks>

        /// Note the '?' detection is naive, as a ? could occur in the middle

        /// of a SCPI string argument, and not actually signify a SCPI query.

        /// </remarks>

        /// <param name="s"></param>

        void Write(string s)

        {

            Console.WriteLine(s);

            tc.WriteLine(s);

            if (s.IndexOf('?') >= 0)

                Read();

        }

        /// <summary>

        /// Read the telnet connection for a response, and print the response to the

        /// Console.

        /// </summary>

        void Read()

        {
            Console.WriteLine(tc.Read());

        }


        #region TelnetConnection - no need to edit

        /// <summary>

        /// Telnet Connection on port 5025 to an instrument

        /// </summary>

        public class TelnetConnection : IDisposable

        {

            System.Net.Sockets.TcpClient m_Client;

            System.Net.Sockets.NetworkStream m_Stream;

            bool m_IsOpen = false;

            string m_Hostname;

            int m_ReadTimeout = 1000; // ms

            public delegate void ConnectionDelegate();

            public event ConnectionDelegate Opened;

            public event ConnectionDelegate Closed;

            public bool IsOpen { get { return m_IsOpen; } }

            public TelnetConnection() { }

            public TelnetConnection(bool open) : this("localhost", true) { }

            public TelnetConnection(string host, bool open)

            {

                if (open)

                    Open(host);

            }

            void CheckOpen()

            {

                if (!IsOpen)

                    throw new Exception("Connection not open.");

            }

            public string Hostname

            {

                get { return m_Hostname; }

            }

            public int ReadTimeout

            {

                set { m_ReadTimeout = value; if (IsOpen) m_Stream.ReadTimeout = value; }

                get { return m_ReadTimeout; }

            }

            public void Write(string str)

            {

                //FieldFox Programming Guide 6

                CheckOpen();

                byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(str);

                m_Stream.Write(bytes, 0, bytes.Length);

                m_Stream.Flush();

            }

            public void WriteLine(string str)

            {

                CheckOpen();

                byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(str);

                m_Stream.Write(bytes, 0, bytes.Length);

                WriteTerminator();

            }

            void WriteTerminator()

            {

                byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes("\r\n");

                m_Stream.Write(bytes, 0, bytes.Length);

                m_Stream.Flush();

            }

            public string Read()

            {

                CheckOpen();

                return System.Text.ASCIIEncoding.ASCII.GetString(ReadBytes());

            }

            /// <summary>

            /// Reads bytes from the socket and returns them as a byte[].

            /// </summary>

            /// <returns></returns>

            public byte[] ReadBytes()

            {

                int i = m_Stream.ReadByte();

                byte b = (byte)i;

                int bytesToRead = 0;

                var bytes = new List<byte>();

                if ((char)b == '#')

                {

                    bytesToRead = ReadLengthHeader();

                    if (bytesToRead > 0)

                    {

                        i = m_Stream.ReadByte();

                        if ((char)i != '\n') // discard carriage return after length header.

                            bytes.Add((byte)i);

                    }

                }

                if (bytesToRead == 0)

                {

                    while (i != -1 && b != (byte)'\n')

                    {

                        bytes.Add(b);

                        i = m_Stream.ReadByte();

                        b = (byte)i;

                    }

                }

                else

                {

                    int bytesRead = 0;

                    while (bytesRead < bytesToRead && i != -1)

                    {

                        i = m_Stream.ReadByte();

                        if (i != -1)

                        {

                            bytesRead++;

                            // record all bytes except \n if it is the last char.

                            if (bytesRead < bytesToRead || (char)i != '\n')

                                bytes.Add((byte)i);

                        }

                    }

                }

                return bytes.ToArray();

            }

            int ReadLengthHeader()

            {

                int numDigits = Convert.ToInt32(new string(new char[] { (char)m_Stream.ReadByte() }));

                string bytes = "";

                for (int i = 0; i < numDigits; ++i)

                    bytes = bytes + (char)m_Stream.ReadByte();

                return Convert.ToInt32(bytes);

            }

            public void Open(string hostname)

            {

                if (IsOpen)

                    Close();

                m_Hostname = hostname;

                m_Client = new System.Net.Sockets.TcpClient(hostname, 5554);

                m_Stream = m_Client.GetStream();

                m_Stream.ReadTimeout = ReadTimeout;

                m_IsOpen = true;

                if (Opened != null)

                    Opened();

            }

            public void Close()

            {

                if (!m_IsOpen)

                    //FieldFox Programming Guide 7

                    return;

                m_Stream.Close();

                m_Client.Close();

                m_IsOpen = false;

                if (Closed != null)

                    Closed();

            }

            public void Dispose()

            {

                Close();

            }
            #endregion


        }
    }
}
