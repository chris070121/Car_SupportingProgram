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

        public GaugeSymbols()
        {
            InitializeComponent();
            Thread a = new Thread(UpdateGUI);
            a.IsBackground = true;
            a.Start();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void UpdateGUI()
        {
            while (true)
            {
                try
                {
                    label1.Invoke((Action)delegate
                    {
                        label1.Text = _leftSignal;
                        
                    });

                    label2.Invoke((Action)delegate
                    {
                        label2.Text = _rightSignal;
                    });
                    label3.Invoke((Action)delegate
                    {
                        label3.Text = _checkEngineSymbol;
                    });
                    label4.Invoke((Action)delegate
                    {
                        label4.Text = _emergencyBrake;
                    });
                    label5.Invoke((Action)delegate
                    {
                        label5.Text = _reverseSymbol;
                    });
                    label6.Invoke((Action)delegate
                    {
                        label6.Text = _seatBelt;
                    });
                    label7.Invoke((Action)delegate
                    {
                        label7.Text = _highBeamLightSymbol;
                    });
                    label8.Invoke((Action)delegate
                    {
                        label8.Text = _gasLightSymbol;
                    });
                    label9.Invoke((Action)delegate
                    {
                        label9.Text = _lightSymbol;
                    });
                    this.Update();
                    Thread.Sleep(100);
                }
                catch(Exception ex)
                {

                }

            }
        }

        public void SetLabels(string buffer)
        {
            string[] message = buffer.Split(',');
            foreach (string a in message)
            {
                string[] result = a.Split('=');
                if (result[0].Contains("LightSymbol"))
                {
                    _lightSymbol = "LightSymbol= " + result[1];
                }
                else if (result[0].Contains("SeatBeltSymbol"))
                {
                    _seatBelt = "SeatBeltSymbol= " + result[1];
                }
                else if (result[0].Contains("ReverseSymbol"))
                {
                    _reverseSymbol = "ReverseSymbol= " + result[1];
                }
                else if (result[0].Contains("RightTurnSignal"))
                {
                    _rightSignal = "RightTurnSignal= " + result[1];
                }
                else if (result[0].Contains("LeftTurnSignal"))
                {
                    _leftSignal = "LeftTurnSignal = " + result[1];
                }
                else if (result[0].Contains("EmergencyBrake"))
                {
                    _emergencyBrake = "EmergencyBrake = " + result[1];
                }
                else if (result[0].Contains("CheckEngine"))
                {
                    _checkEngineSymbol = "CheckEngine = " + result[1];
                }
                else if (result[0].Contains("GasLightSymbol"))
                {
                    _gasLightSymbol = "GasLightSymbol = " + result[1];
                }
                else if (result[0].Contains("HiBeamLightSymbol"))
                {
                    _highBeamLightSymbol = "HiBeam = " + result[1];
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
        //private void InitializeComponent()
        //{
        //    this.groupBox1 = new System.Windows.Forms.GroupBox();
        //    this.label1 = new System.Windows.Forms.Label();
        //   // this.pictureBox1 = new System.Windows.Forms.PictureBox();
        //    this.pictureBox3 = new System.Windows.Forms.PictureBox();
        //    this.pictureBox4 = new System.Windows.Forms.PictureBox();
        //    this.pictureBox5 = new System.Windows.Forms.PictureBox();
        //    this.pictureBox6 = new System.Windows.Forms.PictureBox();
        //    this.pictureBox8 = new System.Windows.Forms.PictureBox();
        //    this.pictureBox2 = new System.Windows.Forms.PictureBox();
        //    this.pictureBox7 = new System.Windows.Forms.PictureBox();
        //    this.label2 = new System.Windows.Forms.Label();
        //    this.label3 = new System.Windows.Forms.Label();
        //    this.label4 = new System.Windows.Forms.Label();
        //    this.label5 = new System.Windows.Forms.Label();
        //    this.label6 = new System.Windows.Forms.Label();
        //    this.label7 = new System.Windows.Forms.Label();
        //    this.label8 = new System.Windows.Forms.Label();
        //    this.label9 = new System.Windows.Forms.Label();
        //    this.label10 = new System.Windows.Forms.Label();
        //    this.groupBox1.SuspendLayout();
        //   // ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
        //    this.SuspendLayout();
        //    // 
        //    // groupBox1
        //    // 
        //    this.groupBox1.BackColor = System.Drawing.Color.Black;
        //    this.groupBox1.Controls.Add(this.label10);
        //    this.groupBox1.Controls.Add(this.label9);
        //    this.groupBox1.Controls.Add(this.label8);
        //    this.groupBox1.Controls.Add(this.label7);
        //    this.groupBox1.Controls.Add(this.label6);
        //    this.groupBox1.Controls.Add(this.label5);
        //    this.groupBox1.Controls.Add(this.label4);
        //    this.groupBox1.Controls.Add(this.label3);
        //    this.groupBox1.Controls.Add(this.label2);
        //    this.groupBox1.Controls.Add(this.pictureBox7);
        //    this.groupBox1.Controls.Add(this.pictureBox2);
        //    this.groupBox1.Controls.Add(this.pictureBox8);
        //    this.groupBox1.Controls.Add(this.pictureBox6);
        //    this.groupBox1.Controls.Add(this.pictureBox5);
        //    this.groupBox1.Controls.Add(this.pictureBox4);
        //    this.groupBox1.Controls.Add(this.pictureBox3);
        //  //  this.groupBox1.Controls.Add(this.pictureBox1);
        //    this.groupBox1.Controls.Add(this.label1);
        //    this.groupBox1.Location = new System.Drawing.Point(0, 0);
        //    this.groupBox1.Name = "groupBox1";
        //    this.groupBox1.Size = new System.Drawing.Size(416, 412);
        //    this.groupBox1.TabIndex = 0;
        //    this.groupBox1.TabStop = false;
        //    this.groupBox1.Text = "groupBox1";
        //    // 
        //    // label1
        //    // 
        //    this.label1.AutoSize = true;
        //    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.label1.ForeColor = System.Drawing.Color.White;
        //    this.label1.Location = new System.Drawing.Point(27, 46);
        //    this.label1.Name = "label1";
        //    this.label1.Size = new System.Drawing.Size(64, 25);
        //    this.label1.TabIndex = 0;
        //    this.label1.Text = "label1";
        //    // 
        //    // pictureBox1
        //    // 
        //    //this.pictureBox1.BackColor = System.Drawing.Color.Aqua;
        //    //this.pictureBox1.Location = new System.Drawing.Point(6, 83);
        //    //this.pictureBox1.Name = "pictureBox1";
        //    //this.pictureBox1.Size = new System.Drawing.Size(410, 28);
        //    //this.pictureBox1.TabIndex = 1;
        //    //this.pictureBox1.TabStop = false;
        //    // 
        //    // pictureBox3
        //    // 
        //    this.pictureBox3.BackColor = System.Drawing.Color.Aqua;
        //    this.pictureBox3.Location = new System.Drawing.Point(6, 233);
        //    this.pictureBox3.Name = "pictureBox3";
        //    this.pictureBox3.Size = new System.Drawing.Size(410, 28);
        //    this.pictureBox3.TabIndex = 3;
        //    this.pictureBox3.TabStop = false;
        //    // 
        //    // pictureBox4
        //    // 
        //    this.pictureBox4.BackColor = System.Drawing.Color.Aqua;
        //    this.pictureBox4.Location = new System.Drawing.Point(3, 158);
        //    this.pictureBox4.Name = "pictureBox4";
        //    this.pictureBox4.Size = new System.Drawing.Size(410, 28);
        //    this.pictureBox4.TabIndex = 4;
        //    this.pictureBox4.TabStop = false;
        //    // 
        //    // pictureBox5
        //    // 
        //    this.pictureBox5.BackColor = System.Drawing.Color.Aqua;
        //    this.pictureBox5.Location = new System.Drawing.Point(11, 15);
        //    this.pictureBox5.Name = "pictureBox5";
        //    this.pictureBox5.Size = new System.Drawing.Size(410, 28);
        //    this.pictureBox5.TabIndex = 5;
        //    this.pictureBox5.TabStop = false;
        //    // 
        //    // pictureBox6
        //    // 
        //    this.pictureBox6.BackColor = System.Drawing.Color.Aqua;
        //    this.pictureBox6.Location = new System.Drawing.Point(6, 309);
        //    this.pictureBox6.Name = "pictureBox6";
        //    this.pictureBox6.Size = new System.Drawing.Size(410, 28);
        //    this.pictureBox6.TabIndex = 6;
        //    this.pictureBox6.TabStop = false;
        //    // 
        //    // pictureBox8
        //    // 
        //    this.pictureBox8.BackColor = System.Drawing.Color.Aqua;
        //    this.pictureBox8.Location = new System.Drawing.Point(6, 384);
        //    this.pictureBox8.Name = "pictureBox8";
        //    this.pictureBox8.Size = new System.Drawing.Size(410, 28);
        //    this.pictureBox8.TabIndex = 8;
        //    this.pictureBox8.TabStop = false;
        //    // 
        //    // pictureBox2
        //    // 
        //    this.pictureBox2.BackColor = System.Drawing.Color.Aqua;
        //    this.pictureBox2.Location = new System.Drawing.Point(400, 15);
        //    this.pictureBox2.Name = "pictureBox2";
        //    this.pictureBox2.Size = new System.Drawing.Size(21, 409);
        //    this.pictureBox2.TabIndex = 9;
        //    this.pictureBox2.TabStop = false;
        //    // 
        //    // pictureBox7
        //    // 
        //    this.pictureBox7.BackColor = System.Drawing.Color.Aqua;
        //    this.pictureBox7.Location = new System.Drawing.Point(0, 12);
        //    this.pictureBox7.Name = "pictureBox7";
        //    this.pictureBox7.Size = new System.Drawing.Size(21, 409);
        //    this.pictureBox7.TabIndex = 10;
        //    this.pictureBox7.TabStop = false;
        //    // 
        //    // label2
        //    // 
        //    this.label2.AutoSize = true;
        //    this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.label2.ForeColor = System.Drawing.Color.White;
        //    this.label2.Location = new System.Drawing.Point(246, 46);
        //    this.label2.Name = "label2";
        //    this.label2.Size = new System.Drawing.Size(64, 25);
        //    this.label2.TabIndex = 11;
        //    this.label2.Text = "label2";
        //    // 
        //    // label3
        //    // 
        //    this.label3.AutoSize = true;
        //    this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.label3.ForeColor = System.Drawing.Color.White;
        //    this.label3.Location = new System.Drawing.Point(246, 194);
        //    this.label3.Name = "label3";
        //    this.label3.Size = new System.Drawing.Size(64, 25);
        //    this.label3.TabIndex = 12;
        //    this.label3.Text = "label3";
        //    // 
        //    // label4
        //    // 
        //    this.label4.AutoSize = true;
        //    this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.label4.ForeColor = System.Drawing.Color.White;
        //    this.label4.Location = new System.Drawing.Point(246, 114);
        //    this.label4.Name = "label4";
        //    this.label4.Size = new System.Drawing.Size(64, 25);
        //    this.label4.TabIndex = 13;
        //    this.label4.Text = "label4";
        //    // 
        //    // label5
        //    // 
        //    this.label5.AutoSize = true;
        //    this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.label5.ForeColor = System.Drawing.Color.White;
        //    this.label5.Location = new System.Drawing.Point(27, 114);
        //    this.label5.Name = "label5";
        //    this.label5.Size = new System.Drawing.Size(64, 25);
        //    this.label5.TabIndex = 14;
        //    this.label5.Text = "label5";
        //    // 
        //    // label6
        //    // 
        //    this.label6.AutoSize = true;
        //    this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.label6.ForeColor = System.Drawing.Color.White;
        //    this.label6.Location = new System.Drawing.Point(27, 194);
        //    this.label6.Name = "label6";
        //    this.label6.Size = new System.Drawing.Size(64, 25);
        //    this.label6.TabIndex = 15;
        //    this.label6.Text = "label6";
        //    // 
        //    // label7
        //    // 
        //    this.label7.AutoSize = true;
        //    this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.label7.ForeColor = System.Drawing.Color.White;
        //    this.label7.Location = new System.Drawing.Point(27, 264);
        //    this.label7.Name = "label7";
        //    this.label7.Size = new System.Drawing.Size(64, 25);
        //    this.label7.TabIndex = 16;
        //    this.label7.Text = "label7";
        //    // 
        //    // label8
        //    // 
        //    this.label8.AutoSize = true;
        //    this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.label8.ForeColor = System.Drawing.Color.White;
        //    this.label8.Location = new System.Drawing.Point(27, 340);
        //    this.label8.Name = "label8";
        //    this.label8.Size = new System.Drawing.Size(64, 25);
        //    this.label8.TabIndex = 17;
        //    this.label8.Text = "label8";
        //    // 
        //    // label9
        //    // 
        //    this.label9.AutoSize = true;
        //    this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.label9.ForeColor = System.Drawing.Color.White;
        //    this.label9.Location = new System.Drawing.Point(246, 340);
        //    this.label9.Name = "label9";
        //    this.label9.Size = new System.Drawing.Size(64, 25);
        //    this.label9.TabIndex = 18;
        //    this.label9.Text = "label9";
        //    // 
        //    // label10
        //    // 
        //    this.label10.AutoSize = true;
        //    this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //    this.label10.ForeColor = System.Drawing.Color.White;
        //    this.label10.Location = new System.Drawing.Point(246, 264);
        //    this.label10.Name = "label10";
        //    this.label10.Size = new System.Drawing.Size(75, 25);
        //    this.label10.TabIndex = 19;
        //    this.label10.Text = "label10";
        //    // 
        //    // GaugeSymbols
        //    // 
        //    this.BackColor = System.Drawing.Color.Black;
        //    this.ClientSize = new System.Drawing.Size(428, 409);
        //    this.Controls.Add(this.groupBox1);
        //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        //    this.Name = "GaugeSymbols";
        //    this.groupBox1.ResumeLayout(false);
        //    this.groupBox1.PerformLayout();
        //  //  ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
        //    this.ResumeLayout(false);

        //}
    }
}
