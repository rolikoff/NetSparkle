﻿using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NetSparkle;
using NetSparkle.Enums;

namespace NetSparkleTestApp
{
    public partial class Form1 : Form
    {
        private readonly Sparkle _sparkle;

        public Form1()
        {
            InitializeComponent();

            _sparkle = new Sparkle("https://deadpikle.github.io/NetSparkle/files/sample-app/appcast.xml", SystemIcons.Application);
            // TLS 1.2 required by GitHub (https://developer.github.com/changes/2018-02-01-weak-crypto-removal-notice/)
            _sparkle.SecurityProtocolType = System.Net.SecurityProtocolType.Tls12;

            _sparkle.UpdateDetected += new UpdateDetected(_sparkle_updateDetected);
            //_sparkle.EnableSilentMode = true;
            //_sparkle.HideReleaseNotes = true;

            _sparkle.StartLoop(true);
        }

        public static string DirectoryOfTheApplicationExecutable
        {
            get
            {
                string path;
                path = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
                path = Uri.UnescapeDataString(path);
                return Directory.GetParent(path).FullName;
            }
        }


        void _sparkle_updateDetected(object sender, UpdateDetectedEventArgs e)
        {
            DialogResult res = MessageBox.Show("Update detected, perform unattended", "Update", MessageBoxButtons.YesNoCancel);

            if (res == System.Windows.Forms.DialogResult.Yes)
                e.NextAction = NextUpdateAction.PerformUpdateUnattended;
            else if (res == System.Windows.Forms.DialogResult.Cancel)
                e.NextAction = NextUpdateAction.ProhibitUpdate;
            else
                e.NextAction = NextUpdateAction.ShowStandardUserInterface;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _sparkle.StopLoop();
        }

        private void btnStopLoop_Click(object sender, EventArgs e)
        {
            _sparkle.StopLoop();
        }

        private void btnTestLoop_Click(object sender, EventArgs e)
        {
            if (_sparkle.IsUpdateLoopRunning)
                MessageBox.Show("Loop is running");
            else
                MessageBox.Show("Loop is not running");
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
