using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DirectShow.BaseClasses;
using Microsoft.Kinect;

namespace KinectCam
{
    [ComVisible(true)]
    [Guid("3EFB481C-F35F-434c-A085-C3DFEFF65D94")]
    public partial class AboutForm : BasePropertyPage
    {
        public AboutForm()
        {
            InitializeComponent();
            angleTrackBar.Value = KinectHelper.GetAngle();
        }

        private void angleTrackBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                KinectHelper.SetAngle(KinectSensor.KinectSensors[0], angleTrackBar.Value);
            }
            catch { }
        }
    }
}
