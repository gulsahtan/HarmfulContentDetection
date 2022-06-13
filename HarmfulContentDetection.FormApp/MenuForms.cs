using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HarmfulContentDetection.FormApp
{
    public partial class MenuForms : Form
    {
        public MenuForms()
        {
            InitializeComponent();
        }

        private void realTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RealTimeForm to = new RealTimeForm();
            to.Show();
            this.Hide();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WebCamForm to = new WebCamForm();
            to.Show();
            this.Hide();
        }

        private void MenuForms_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MenuForms to = new MenuForms();
                    to.Close();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }  

        private void videoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VideoWAudio to = new VideoWAudio();
            to.Show();
            this.Hide();
        }
    }
}
