namespace HarmfulContentDetection.FormApp
{
    partial class WebCamForm
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
            this.cboCamera = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pic = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.Alcohol = new System.Windows.Forms.CheckBox();
            this.Violence = new System.Windows.Forms.CheckBox();
            this.Cigarette = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // cboCamera
            // 
            this.cboCamera.FormattingEnabled = true;
            this.cboCamera.Location = new System.Drawing.Point(93, 29);
            this.cboCamera.Name = "cboCamera";
            this.cboCamera.Size = new System.Drawing.Size(333, 23);
            this.cboCamera.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Camera :";
            // 
            // pic
            // 
            this.pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic.Location = new System.Drawing.Point(33, 130);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(726, 500);
            this.pic.TabIndex = 2;
            this.pic.TabStop = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(684, 646);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Alcohol
            // 
            this.Alcohol.AutoSize = true;
            this.Alcohol.Location = new System.Drawing.Point(547, 21);
            this.Alcohol.Name = "Alcohol";
            this.Alcohol.Size = new System.Drawing.Size(67, 19);
            this.Alcohol.TabIndex = 4;
            this.Alcohol.Text = "Alcohol";
            this.Alcohol.UseVisualStyleBackColor = true;
            // 
            // Violence
            // 
            this.Violence.AutoSize = true;
            this.Violence.Location = new System.Drawing.Point(547, 44);
            this.Violence.Name = "Violence";
            this.Violence.Size = new System.Drawing.Size(71, 19);
            this.Violence.TabIndex = 5;
            this.Violence.Text = "Violence";
            this.Violence.UseVisualStyleBackColor = true;
            // 
            // Cigarette
            // 
            this.Cigarette.AutoSize = true;
            this.Cigarette.Location = new System.Drawing.Point(546, 67);
            this.Cigarette.Name = "Cigarette";
            this.Cigarette.Size = new System.Drawing.Size(74, 19);
            this.Cigarette.TabIndex = 6;
            this.Cigarette.Text = "Cigarette";
            this.Cigarette.UseVisualStyleBackColor = true;
            // 
            // WebCamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 857);
            this.Controls.Add(this.Cigarette);
            this.Controls.Add(this.Violence);
            this.Controls.Add(this.Alcohol);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboCamera);
            this.Name = "WebCamForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Object Detection-WebCam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebCamForm_FormClosing);
            this.Load += new System.EventHandler(this.WebCamForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboCamera;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox Alcohol;
        private System.Windows.Forms.CheckBox Violence;
        private System.Windows.Forms.CheckBox Cigarette;
    }
}