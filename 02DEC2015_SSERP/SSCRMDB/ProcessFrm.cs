using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading; 
namespace SSCRMDB
{
    public partial class ProcessFrm : Form
    {
        // Declare our worker thread
        private Thread processThread = null;

        // Boolean flag used to stop the 
        private bool stopProcess = false;

        public ProcessFrm()
        {
            InitializeComponent();
            Fullscreen();
        }

        //private void Form1_Load(object sender, EventArgs e)
        //{

        //    // Initialise the delegate
            
        //    this.stopProcess = false;

        //    // Initialise and start worker thread
        //    this.processThread = new Thread(new ThreadStart(this.HeavyOperation));
        //    this.processThread.Start();

        //}

        //private void ProcessFrm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    this.stopProcess = true;
        //}
        //private void HeavyOperation()
        //{
        //    // Example heavy operation
        //    for (int i = 0; i <= 999999; i++)
        //    {
        //        // Check if Stop button was clicked
        //        if (!this.stopProcess)
        //        {
        //            // Show progress
        //            this.Invoke(this.updateStatusDelegate);
        //        }
        //        else
        //        {
        //            // Stop heavy operation
        //            this.workerThread.Abort();
        //        }
        //    }
        //}

        struct clientRect
        {
            public Point location;
            public int width;
            public int height;
        };
        // this should be in the scope your class
        clientRect restore;
        bool fullscreen = false;

        /// <summary>
        /// Makes the form either fullscreen, or restores it to it's original size/location
        /// </summary>
        void Fullscreen()
        {
            if (fullscreen == false)
            {
                this.restore.location = this.Location;
                this.restore.width = this.Width;
                this.restore.height = this.Height;
                this.TopMost = true;
                //this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height/2);
                //this.FormBorderStyle = FormBorderStyle.None;
                //this.Width = Screen.PrimaryScreen.Bounds.Width;
                //this.Height = Screen.PrimaryScreen.Bounds.Height;
                Application.DoEvents();
            }
            else
            {
                this.TopMost = false;
                this.Location = this.restore.location;
                this.Width = this.restore.width;
                this.Height = this.restore.height;
                // these are the two variables you may wish to change, depending
                // on the design of your form (WindowState and FormBorderStyle)
                this.WindowState = FormWindowState.Normal ;
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
            Application.DoEvents();
        }

        private void ProcessFrm_Load(object sender, EventArgs e)
        {
            Fullscreen();
            pictureBox1.Update();
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProcessFrm_Activated(object sender, EventArgs e)
        {
            Fullscreen();
            pictureBox1.Refresh();
        }

       
        private void ProcessFrm_Paint(object sender, PaintEventArgs e)
        {
            //Fullscreen();
            //pictureBox1.Update();
            pictureBox1.Refresh();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);

                //run in back thread
                backgroundWorker1.ReportProgress(i);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = progressBar1.Maximum;
        }
    }
}
