namespace HarmfulContentDetection.FormApp
{
    partial class WebCamPipelineForm
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
            this.btnStart = new System.Windows.Forms.Button();
            this.Alcohol = new System.Windows.Forms.CheckBox();
            this.Violence = new System.Windows.Forms.CheckBox();
            this.Cigarette = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(724, 44);
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
            this.Alcohol.Location = new System.Drawing.Point(33, 44);
            this.Alcohol.Name = "Alcohol";
            this.Alcohol.Size = new System.Drawing.Size(67, 19);
            this.Alcohol.TabIndex = 4;
            this.Alcohol.Text = "Alcohol";
            this.Alcohol.UseVisualStyleBackColor = true;
            // 
            // Violence
            // 
            this.Violence.AutoSize = true;
            this.Violence.Location = new System.Drawing.Point(128, 44);
            this.Violence.Name = "Violence";
            this.Violence.Size = new System.Drawing.Size(71, 19);
            this.Violence.TabIndex = 5;
            this.Violence.Text = "Violence";
            this.Violence.UseVisualStyleBackColor = true;
            // 
            // Cigarette
            // 
            this.Cigarette.AutoSize = true;
            this.Cigarette.Location = new System.Drawing.Point(220, 44);
            this.Cigarette.Name = "Cigarette";
            this.Cigarette.Size = new System.Drawing.Size(74, 19);
            this.Cigarette.TabIndex = 6;
            this.Cigarette.Text = "Cigarette";
            this.Cigarette.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(33, 82);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(766, 638);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // WebCamPipelineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 857);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Cigarette);
            this.Controls.Add(this.Violence);
            this.Controls.Add(this.Alcohol);
            this.Controls.Add(this.btnStart);
            this.Name = "WebCamPipelineForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebCam-Pipeline";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebCamPipeline_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboCamera;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox Alcohol;
        private System.Windows.Forms.CheckBox Violence;
        private System.Windows.Forms.CheckBox Cigarette;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}