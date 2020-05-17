using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace webCamTest.Gauges
{
    public partial class GaugeSymbols : Form
    {
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
      
        private Label label1;
        private string _lightSymbol;
        private string _seatBelt;
        private string _reverseSymbol;
        private string _rightSignal;
        private string _leftSignal;
        private string _emergencyBrake;
        private string _checkEngineSymbol;
        private bool _brightnessUp;
        private string _gasLightSymbol;
        private string _highBeamLightSymbol;
        List<PictureBox> pictureBoxList;
        Point point = new Point(-2280, 0);
        private Bitmap bitmap;
        private string _tirePressureSymbol;

        public GaugeSymbols()
        {
            InitializeComponent();
            AddPictureBoxesToList();
            infoPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = point;
            Thread a = new Thread(UpdateGUI);
            a.IsBackground = true;
            a.Start();
        }

        public void SetBitmap(Bitmap _bitmap)
        {
            bitmap = _bitmap;
        }

        private void UpdateGUI()
        {
            while (true)
            {
                try
                {

                    infoPicBox.Invoke((Action)delegate
                    {
                        //pictureBox1.Width = bitmap.Width;
                        //pictureBox1.Height = bitmap.Height;         
                        infoPicBox.Image = bitmap;

                    });
                    label1.Invoke((Action)delegate
                    {
                        label1.Text = _leftSignal;
                        SetColors(label1);
                    });

                    label2.Invoke((Action)delegate
                    {
                        label2.Text = _rightSignal;
                        SetColors(label2);

                    });
                    label3.Invoke((Action)delegate
                    {
                        label3.Text = _checkEngineSymbol;
                        SetColors(label3);

                    });
                    label4.Invoke((Action)delegate
                    {
                        label4.Text = _emergencyBrake;
                        SetColors(label4);

                    });
                    label5.Invoke((Action)delegate
                    {
                        label5.Text = _reverseSymbol;
                        SetColors(label5);

                    });
                    label6.Invoke((Action)delegate
                    {
                        label6.Text = _seatBelt;
                        SetColors(label6);

                    });
                    label7.Invoke((Action)delegate
                    {
                        label7.Text = _highBeamLightSymbol;
                        SetColors(label7);

                    });
                    label8.Invoke((Action)delegate
                    {
                        label8.Text = _gasLightSymbol;
                        SetColors(label8);

                    });
                    label9.Invoke((Action)delegate
                    {
                        label9.Text = _lightSymbol;
                        SetColors(label9);
                    });
                    label10.Invoke((Action)delegate
                    {
                        label10.Text = _tirePressureSymbol;
                        SetColors(label9);
                    });
                    this.Invoke((Action)delegate
                    {
                        // pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        //this.Width = pictureBox1.Width;
                        //this.Height = pictureBox1.Height;
                        this.Update();

                    });
                    CheckBrightness();
                    CheckLocation();
                    Thread.Sleep(10);
                }
                catch(Exception ex)
                {

                }

            }
        }

        private void CheckLocation()
        {
            if (this.Location != point)
            {
                this.Location = point;
            }
        }

        private void CheckBrightness()
        {
            if(_brightnessUp && pictureBoxList[0].BackColor != Color.Aqua)
            {
                foreach(PictureBox pictureBox in pictureBoxList)
                {
                    pictureBox.Invoke((Action)delegate
                    {
                        pictureBox.BackColor = Color.Aqua;
                    });
                }
            }
            else if (!_brightnessUp && pictureBoxList[0].BackColor != Color.DarkSlateGray)
            {
                foreach (PictureBox pictureBox in pictureBoxList)
                {
                    pictureBox.Invoke((Action)delegate
                    {
                        pictureBox.BackColor = Color.DarkSlateGray;
                    });
                }
            }
        }

        private void SetColors(Label label)
        {
            if (label.Text.Contains("true"))
            {
                label.ForeColor = Color.Red;
            }
            else
            {
                label.ForeColor = Color.White;
            }
        }
        public void SetLabels(string buffer)
        {
            string[] message = buffer.Split(',');
            foreach (string a in message)
            {
                string[] result = a.Split('=');
                if (result[0].Contains("LowLightSymbol"))
                {
                    _lightSymbol = "Low Light\n=" + result[1];
                }
                else if (result[0].Contains("SeatBeltSymbol"))
                {
                    _seatBelt = "Seat Belt\n= " + result[1];
                }
                else if (result[0].Contains("LightReverseSymbol"))
                {
                    _reverseSymbol = "ReverseSymbol\n= " + result[1];
                }
                else if (result[0].Contains("RightTurnSignal"))
                {
                    _rightSignal = "Right Turn\n= " + result[1];
                }
                else if (result[0].Contains("LeftTurnSignal"))
                {
                    _leftSignal = "Left Turn\n= " + result[1];
                }
                else if (result[0].Contains("EmergencyBrake"))
                {
                    _emergencyBrake = "EmergencyBrake\n = " + result[1];
                }
                else if (result[0].Contains("CheckEngine"))
                {
                    _checkEngineSymbol = "CheckEngine \n= " + result[1];
                }
                else if (result[0].Contains("GasLightSymbol"))
                {
                    _gasLightSymbol = "Gas Light\n = " + result[1];
                }
                else if (result[0].Contains("HiBeamLightSymbol"))
                {
                    _highBeamLightSymbol = "High Beam \n= " + result[1];
                }
                else if (result[0].Contains("TirePressureSymbol"))
                {
                    _tirePressureSymbol = "TirePressure\n = " + result[1];
                }
                else if (result[0].Contains("BrightnessUp"))
                {
                    if (result[1].Contains("true"))
                    {
                        _brightnessUp = true;
                    }
                    else
                    {
                        _brightnessUp = false;
                    }
                }
            }
        }


        private void AddPictureBoxesToList()
        {
            pictureBoxList = new List<PictureBox>();
            pictureBoxList.Add(pictureBox1);
            pictureBoxList.Add(pictureBox2);
            pictureBoxList.Add(pictureBox3);
            pictureBoxList.Add(pictureBox4);
            pictureBoxList.Add(pictureBox5);
            pictureBoxList.Add(pictureBox6);
            pictureBoxList.Add(pictureBox7);
            pictureBoxList.Add(pictureBox8);
            pictureBoxList.Add(pictureBox9);


        }
    }
}
