﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class PreStartedForm : Form, IProgress<float>, IStatusPeporter
    {
        private Prestarter prestarter;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public PreStartedForm()
        {
            InitializeComponent();
        }

        private void PreStartedForm_Shown(object sender, EventArgs e)
        {
            Console.WriteLine("GravitLauncher Prestarter v" + Config.VERSION + " for project " + Config.PROJECT);
            prestarter = new Prestarter(this, this);
            prestarter.run();
        }

        private void PreStartedForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void PreStartedForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void PreStartedForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void Report(float value)
        {
            progressBar1.Value = (int)(value * 100);
        }

        public void updateStatus(string status)
        {
            labelStatus.Text = status + "...";
        }

        public void requestWaitProgressbar()
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
        }

        public void requestNormalProgressbar()
        {
            progressBar1.Style = ProgressBarStyle.Continuous;
        }
    }
}
