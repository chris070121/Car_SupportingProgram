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
        private Bitmap mapBitmap;
        private string _tirePressureSymbol;

        public GaugeSymbols()
        {
            InitializeComponent();
            AddPictureBoxesToList();
            infoPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = point;
            mapsPictureBox.Visible = false;
            mapsPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void SetBitmap(Bitmap _bitmap)
        {
            bitmap = _bitmap;
        }

        public void TurnMapOn(bool isMapOn)
        {
            mapsPictureBox.Visible = isMapOn;
        }

        public void SetBitmapForMap(Bitmap bitmap)
        {
            mapBitmap = bitmap;
        }




        private void UpdateGUI()
        {
            while (true)
            {
                try
                {
                        label1.Invoke((Action)delegate
                        {
                            
                            SetColors(label1, _leftSignal);
                        });

                        label2.Invoke((Action)delegate
                        {
                            SetColors(label2, _rightSignal);

                        });
                        label3.Invoke((Action)delegate
                        {
                            SetColors(label3, _checkEngineSymbol);

                        });
                        label4.Invoke((Action)delegate
                        {
                            SetColors(label4, _emergencyBrake);

                        });
                        label6.Invoke((Action)delegate
                        {
                            SetColors(label6, _seatBelt);

                        });
                        label7.Invoke((Action)delegate
                        {
                            SetColors(label7, _highBeamLightSymbol);

                        });
                        label8.Invoke((Action)delegate
                        {
                            SetColors(label8, _gasLightSymbol);

                        });
                        label9.Invoke((Action)delegate
                        {
                            SetColors(label9, _lightSymbol);
                        });
                        label10.Invoke((Action)delegate
                        {
                            SetColors(label10, _tirePressureSymbol);
                        });

                    if (mapsPictureBox.Visible == false)
                    {
                        infoPicBox.Invoke((Action)delegate
                        {
                            //pictureBox1.Width = bitmap.Width;
                            //pictureBox1.Height = bitmap.Height;         
                            infoPicBox.Image = bitmap;

                        });
                    }
                    else
                    {
                        mapsPictureBox.Invoke((Action)delegate
                        {
                            mapsPictureBox.Image = mapBitmap;
                        });
                    }

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

     
        private void SetColors(Label label , string message)
        {
            if (message != null && message.Contains("true"))
            {
                if (message.Contains("CE") || message.Contains("SB") || message.Contains("EB") || message.Contains("GL"))
                {
                    label.ForeColor = Color.Red;
                }
                else
                {
                    label.ForeColor = Color.LimeGreen;
                }
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
                    _lightSymbol = "LL=" + result[1];
                }
                else if (result[0].Contains("SeatBeltSymbol"))
                {
                    _seatBelt = "SB=" + result[1];
                }
                else if (result[0].Contains("LightReverseSymbol"))
                {
                    _reverseSymbol = "Rev=" + result[1];
                }
                else if (result[0].Contains("RightTurnSignal"))
                {
                    _rightSignal = "RT=" + result[1];
                }
                else if (result[0].Contains("LeftTurnSignal"))
                {
                    _leftSignal = "LT=" + result[1];
                }
                else if (result[0].Contains("EmergencyBrake"))
                {
                    _emergencyBrake = "EB=" + result[1];
                }
                else if (result[0].Contains("CheckEngine"))
                {
                    _checkEngineSymbol = "CE=" + result[1];
                }
                else if (result[0].Contains("GasLightSymbol"))
                {
                    _gasLightSymbol = "GL=" + result[1];
                }
                else if (result[0].Contains("HiBeamLightSymbol"))
                {
                    _highBeamLightSymbol = "HB=" + result[1];
                }
                else if (result[0].Contains("TirePressureSymbol"))
                {
                    _tirePressureSymbol = "TP=" + result[1];
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
            pictureBoxList.Add(pictureBox7);
            pictureBoxList.Add(pictureBox9);

            foreach (PictureBox x in pictureBoxList)
            {
                x.BringToFront();
            }

        }

        private void GaugeSymbols_Load(object sender, EventArgs e)
        {
            Thread a = new Thread(UpdateGUI);
            a.IsBackground = true;
            a.Start();

        }
    }
}
