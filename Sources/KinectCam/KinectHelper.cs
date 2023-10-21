using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Kinect;
using VirtualCam.Properties;

namespace KinectCam
{
    public static class KinectHelper
    {
        class KinectCamApplicationContext : ApplicationContext
        {
            private NotifyIcon TrayIcon;
            private ContextMenuStrip TrayIconContextMenu;
            private ToolStripMenuItem MirroredMenuItem;
            private ToolStripMenuItem DesktopMenuItem;

            public KinectCamApplicationContext()
            {
                Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
                InitializeComponent();
                TrayIcon.Visible = true;
                TrayIcon.ShowBalloonTip(30000);
            }

            private void InitializeComponent()
            {
                TrayIcon = new NotifyIcon();

                TrayIcon.BalloonTipIcon = ToolTipIcon.Info;
                TrayIcon.BalloonTipText =
                  "For options use this tray icon.";
                TrayIcon.BalloonTipTitle = "KinectCam";
                TrayIcon.Text = "KinectCam";

                TrayIcon.Icon = IconExtractor.Extract(117, false);

                TrayIcon.DoubleClick += TrayIcon_DoubleClick;

                TrayIconContextMenu = new ContextMenuStrip();
                MirroredMenuItem = new ToolStripMenuItem();
                DesktopMenuItem = new ToolStripMenuItem();
                TrayIconContextMenu.SuspendLayout();

                // 
                // TrayIconContextMenu
                // 
                this.TrayIconContextMenu.Items.AddRange(new ToolStripItem[] {
                this.MirroredMenuItem,
                this.DesktopMenuItem
                });
                this.TrayIconContextMenu.Name = "TrayIconContextMenu";
                this.TrayIconContextMenu.Size = new Size(153, 70);
                // 
                // MirroredMenuItem
                // 
                this.MirroredMenuItem.Name = "Mirrored";
                this.MirroredMenuItem.Size = new Size(152, 22);
                this.MirroredMenuItem.Text = "Mirrored";
                this.MirroredMenuItem.Click += new EventHandler(this.MirroredMenuItem_Click);

                // 
                // DesktopMenuItem
                // 
                this.DesktopMenuItem.Name = "Desktop";
                this.DesktopMenuItem.Size = new Size(152, 22);
                this.DesktopMenuItem.Text = "Desktop";
                this.DesktopMenuItem.Click += new EventHandler(this.DesktopMenuItem_Click);

                TrayIconContextMenu.ResumeLayout(false);
                TrayIcon.ContextMenuStrip = TrayIconContextMenu;
            }

            private void OnApplicationExit(object sender, EventArgs e)
            {
                TrayIcon.Visible = false;
            }

            private void TrayIcon_DoubleClick(object sender, EventArgs e)
            {
                TrayIcon.ShowBalloonTip(30000);
            }

            private void MirroredMenuItem_Click(object sender, EventArgs e)
            {                
                Settings.Default.Mirrored = !Settings.Default.Mirrored;
            }

            private void DesktopMenuItem_Click(object sender, EventArgs e)
            {
                Settings.Default.Desktop = !Settings.Default.Desktop;
            }

            public void Exit()
            {
                TrayIcon.Visible = false;
            }
        }

        static KinectCamApplicationContext context;
        static Thread contexThread;
        static Thread refreshThread;
        static KinectSensor Sensor;

        static void InitializeSensor()
        {
            var sensor = Sensor;
            if (sensor != null) return;

            try
            {
                if (KinectSensor.KinectSensors != null && KinectSensor.KinectSensors.Count == 0) return;

                sensor = KinectSensor.KinectSensors[0];
                sensor.Start();
                SetAngle(sensor, GetAngle());
                sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                sensor.ColorFrameReady += Sensor_ColorFrameReady;
                Sensor = sensor;

                if (context == null)
                {
                    contexThread = new Thread(() =>
                    {
                        context = new KinectCamApplicationContext();
                        Application.Run(context);
                    });
                    refreshThread = new Thread(() =>
                    {
                        while (true)
                        {
                            Thread.Sleep(250);
                            Application.DoEvents();
                        }
                    });
                    contexThread.IsBackground = true;
                    refreshThread.IsBackground = true;
                    contexThread.SetApartmentState(ApartmentState.STA);
                    refreshThread.SetApartmentState(ApartmentState.STA);
                    contexThread.Start();
                    refreshThread.Start();
                }
            }
            catch
            {
                Trace.WriteLine("Error of enable the Kinect sensor!");
            }
        }

        private static void Sensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            sensor_ColorFrameReady(e.OpenColorImageFrame());
        }

        public static int GetAngle()
        {
            return Settings.Default.Angle;
        }

        public static void SetAngle(KinectSensor sensor, int value)
        {
            try
            {
                Settings.Default.Angle = value;
                sensor.ElevationAngle = value;
            }
            catch { }
        }

        static void sensor_ColorFrameReady(ColorImageFrame image)
        {
            if (image == null) return;
            image.CopyPixelDataTo(sensorColorFrameData);
            image.Dispose();
            image = null;
        }

        public static void DisposeSensor()
        {
            try
            {
                var sensor = Sensor;
                if (sensor != null && sensor.IsRunning)
                {
                    if (sensor.ColorStream.IsEnabled)
                    {
                        sensor.ColorStream.Disable();
                    }
                    if (sensor.DepthStream.IsEnabled)
                    {
                        sensor.DepthStream.Disable();

                    }
                    sensor.ColorFrameReady -= Sensor_ColorFrameReady;
                    sensor.Stop();
                    sensor.Dispose();
                    sensor = null;
                    Sensor = null;
                }
                if (context != null)
                {
                    context.Exit();
                    context.Dispose();
                    context = null;

                    contexThread.Abort();
                    refreshThread.Abort();
                }
            }
            catch {
                Trace.WriteLine("Error of disable the Kinect sensor!");
            }
        }

        public static int Width = 640;
        public static int Height = 480;

        static readonly byte[] sensorColorFrameData = new byte[640 * 480 * 4];

        public static byte[] GetColorSensorFrame()
        {
            InitializeSensor();
            return sensorColorFrameData;
        }

        public unsafe static void GenerateFrame(IntPtr _ptr, int length, bool mirrored)
        {
            byte[] colorFrame = sensorColorFrameData;
            void* camData = _ptr.ToPointer();

            try
            {
                InitializeSensor();

                if (colorFrame != null)
                {
                    if (!mirrored)
                    {
                        fixed (byte* sDataB = &colorFrame[0])
                        fixed (byte* sDataE = &colorFrame[colorFrame.Length - 1])
                        {
                            byte* pData = (byte*)camData;
                            byte* sData = (byte*)sDataE;

                            for (; sData > sDataB;)
                            {
                                for (var i = 0; i < Width; ++i)
                                {
                                    var p = sData - 3;
                                    *pData++ = *p++;
                                    *pData++ = *p++;
                                    *pData++ = *p++;
                                    p = (sData -= 4);
                                }
                            }
                        }
                    }
                    else
                    {
                        fixed (byte* sDataB = &colorFrame[0])
                        fixed (byte* sDataE = &colorFrame[colorFrame.Length - 1])
                        {
                            byte* pData = (byte*)camData;
                            byte* sData = (byte*)sDataE;

                            var sDataBE = sData;
                            var p = sData;
                            var r = sData;

                            while (sData == (sDataBE = sData) &&
                                   sDataB <= (sData -= (Width * 4 - 1)))
                            {
                                r = sData;
                                do
                                {
                                    p = sData;
                                    *pData++ = *p++;
                                    *pData++ = *p++;
                                    *pData++ = *p++;
                                }
                                while ((sData += 4) <= sDataBE);
                                sData = r - 1;
                            }
                        }
                    }
                }
            }
            catch
            {
                byte* pData = (byte*)camData;
                for (int i = 0; i < length; ++i)
                    *pData++ = 0;
            }
        }
    }
}
