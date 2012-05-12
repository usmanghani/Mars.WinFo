using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Devices.Sensors;

namespace PhoneApp1
{
    public partial class MotionPage : PhoneApplicationPage
    {
        public MotionPage()
        {
            InitializeComponent();
        }

        Motion motion = new Motion();
        bool motionCaptureStarted = false;
        private void btnMotion_Click(object sender, RoutedEventArgs e)
        {
            if (!Motion.IsSupported)
            {
                MessageBox.Show("Motion capture is not supported on this device.", "Error", MessageBoxButton.OK);
                return;
            }

            motion.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<MotionReading>>(motion_CurrentValueChanged);

            if (!motionCaptureStarted)
            {
                motion.Start();

            }
            else
            {
                motion.Stop();
            }

            motionCaptureStarted = !motionCaptureStarted;

            if (motionCaptureStarted)
            {
                btnMotion.Content = "Stop Motion Capture";
            }
            else
            {
                btnMotion.Content = "Start Motion Capture";
            }
        }

        void motion_CurrentValueChanged(object sender, SensorReadingEventArgs<MotionReading> e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                txtAccel.Text = string.Format("{0}, {1}, {2}", e.SensorReading.DeviceAcceleration.X, e.SensorReading.DeviceAcceleration.Y, e.SensorReading.DeviceAcceleration.Z);
                txtGravity.Text = string.Format("{0}, {1}, {2}", e.SensorReading.Gravity.X, e.SensorReading.Gravity.Y, e.SensorReading.Gravity.Z);
                txtRotRate.Text = string.Format("{0}, {1}, {2}", e.SensorReading.DeviceRotationRate.X, e.SensorReading.DeviceRotationRate.Y, e.SensorReading.DeviceRotationRate.Z);

                txtPitch.Text = e.SensorReading.Attitude.Pitch.ToString();
                txtRoll.Text = e.SensorReading.Attitude.Roll.ToString();
                txtYaw.Text = e.SensorReading.Attitude.Yaw.ToString();
            });

        }
    }
}
