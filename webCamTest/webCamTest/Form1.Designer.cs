namespace webCamTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.leftGuagePicBx = new System.Windows.Forms.PictureBox();
            this.SwitchBtn = new System.Windows.Forms.Button();
            this.CaptureLeftGaugeBtn = new System.Windows.Forms.Button();
            this.bottomMiddleGuagePicBx = new System.Windows.Forms.PictureBox();
            this.CaptureBottomMiddletGaugeBtn = new System.Windows.Forms.Button();
            this.CaptureRightGaugeBtn = new System.Windows.Forms.Button();
            this.rightGuagePicBx = new System.Windows.Forms.PictureBox();
            this.reverseCamPicBx = new System.Windows.Forms.PictureBox();
            this.mapsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.leftGuagePicBx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomMiddleGuagePicBx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightGuagePicBx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reverseCamPicBx)).BeginInit();
            this.SuspendLayout();
            // 
            // leftGuagePicBx
            // 
            this.leftGuagePicBx.Location = new System.Drawing.Point(0, 31);
            this.leftGuagePicBx.Name = "leftGuagePicBx";
            this.leftGuagePicBx.Size = new System.Drawing.Size(237, 371);
            this.leftGuagePicBx.TabIndex = 0;
            this.leftGuagePicBx.TabStop = false;
            // 
            // SwitchBtn
            // 
            this.SwitchBtn.Location = new System.Drawing.Point(0, 2);
            this.SwitchBtn.Name = "SwitchBtn";
            this.SwitchBtn.Size = new System.Drawing.Size(117, 23);
            this.SwitchBtn.TabIndex = 1;
            this.SwitchBtn.Text = "Switch";
            this.SwitchBtn.UseVisualStyleBackColor = true;
            this.SwitchBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // CaptureLeftGaugeBtn
            // 
            this.CaptureLeftGaugeBtn.Location = new System.Drawing.Point(332, 2);
            this.CaptureLeftGaugeBtn.Name = "CaptureLeftGaugeBtn";
            this.CaptureLeftGaugeBtn.Size = new System.Drawing.Size(128, 23);
            this.CaptureLeftGaugeBtn.TabIndex = 2;
            this.CaptureLeftGaugeBtn.Text = "CaptureLeftGauge";
            this.CaptureLeftGaugeBtn.UseVisualStyleBackColor = true;
            this.CaptureLeftGaugeBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // bottomMiddleGuagePicBx
            // 
            this.bottomMiddleGuagePicBx.Location = new System.Drawing.Point(243, 215);
            this.bottomMiddleGuagePicBx.Name = "bottomMiddleGuagePicBx";
            this.bottomMiddleGuagePicBx.Size = new System.Drawing.Size(154, 187);
            this.bottomMiddleGuagePicBx.TabIndex = 3;
            this.bottomMiddleGuagePicBx.TabStop = false;
            // 
            // CaptureBottomMiddletGaugeBtn
            // 
            this.CaptureBottomMiddletGaugeBtn.Location = new System.Drawing.Point(466, 2);
            this.CaptureBottomMiddletGaugeBtn.Name = "CaptureBottomMiddletGaugeBtn";
            this.CaptureBottomMiddletGaugeBtn.Size = new System.Drawing.Size(155, 23);
            this.CaptureBottomMiddletGaugeBtn.TabIndex = 4;
            this.CaptureBottomMiddletGaugeBtn.Text = "CaptureBottomMiddleGauge";
            this.CaptureBottomMiddletGaugeBtn.UseVisualStyleBackColor = true;
            this.CaptureBottomMiddletGaugeBtn.Click += new System.EventHandler(this.CaptureBottomMiddletGaugeBtn_Click);
            // 
            // CaptureRightGaugeBtn
            // 
            this.CaptureRightGaugeBtn.Location = new System.Drawing.Point(627, 2);
            this.CaptureRightGaugeBtn.Name = "CaptureRightGaugeBtn";
            this.CaptureRightGaugeBtn.Size = new System.Drawing.Size(128, 23);
            this.CaptureRightGaugeBtn.TabIndex = 5;
            this.CaptureRightGaugeBtn.Text = "CaptureRightGauge";
            this.CaptureRightGaugeBtn.UseVisualStyleBackColor = true;
            this.CaptureRightGaugeBtn.Click += new System.EventHandler(this.CaptureRightGaugeBtn_Click);
            // 
            // rightGuagePicBx
            // 
            this.rightGuagePicBx.Location = new System.Drawing.Point(403, 31);
            this.rightGuagePicBx.Name = "rightGuagePicBx";
            this.rightGuagePicBx.Size = new System.Drawing.Size(172, 371);
            this.rightGuagePicBx.TabIndex = 6;
            this.rightGuagePicBx.TabStop = false;
            // 
            // reverseCamPicBx
            // 
            this.reverseCamPicBx.Location = new System.Drawing.Point(581, 31);
            this.reverseCamPicBx.Name = "reverseCamPicBx";
            this.reverseCamPicBx.Size = new System.Drawing.Size(100, 50);
            this.reverseCamPicBx.TabIndex = 7;
            this.reverseCamPicBx.TabStop = false;
            // 
            // mapsButton
            // 
            this.mapsButton.Location = new System.Drawing.Point(141, 2);
            this.mapsButton.Name = "mapsButton";
            this.mapsButton.Size = new System.Drawing.Size(75, 23);
            this.mapsButton.TabIndex = 8;
            this.mapsButton.Text = "MAPS";
            this.mapsButton.UseVisualStyleBackColor = true;
            this.mapsButton.Click += new System.EventHandler(this.MapsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 602);
            this.Controls.Add(this.mapsButton);
            this.Controls.Add(this.reverseCamPicBx);
            this.Controls.Add(this.rightGuagePicBx);
            this.Controls.Add(this.CaptureRightGaugeBtn);
            this.Controls.Add(this.CaptureBottomMiddletGaugeBtn);
            this.Controls.Add(this.bottomMiddleGuagePicBx);
            this.Controls.Add(this.CaptureLeftGaugeBtn);
            this.Controls.Add(this.SwitchBtn);
            this.Controls.Add(this.leftGuagePicBx);
            this.Name = "Form1";
            this.Text = "CarSuppProgram";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.leftGuagePicBx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomMiddleGuagePicBx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightGuagePicBx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reverseCamPicBx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox leftGuagePicBx;
        private System.Windows.Forms.Button SwitchBtn;
        private System.Windows.Forms.Button CaptureLeftGaugeBtn;
        private System.Windows.Forms.PictureBox bottomMiddleGuagePicBx;
        private System.Windows.Forms.Button CaptureBottomMiddletGaugeBtn;
        private System.Windows.Forms.Button CaptureRightGaugeBtn;
        private System.Windows.Forms.PictureBox rightGuagePicBx;
        private System.Windows.Forms.PictureBox reverseCamPicBx;
        private System.Windows.Forms.Button mapsButton;
    }
}

