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
    public partial class GyroPage : PhoneApplicationPage
    {
        public GyroPage()
        {
            InitializeComponent();
        }

        Gyroscope gyro = new Gyroscope();
        bool gyroStarted = false;

        private void btnGyro_Click(object sender, RoutedEventArgs e)
        {
            if (!Gyroscope.IsSupported)
            {
                MessageBox.Show("Gyro is not supported on this device.", "Error", MessageBoxButton.OK);
                return;
            }

            gyro.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<GyroscopeReading>>(gyro_CurrentValueChanged);

            if (!gyroStarted)
            {
                gyro.Start();
            }
            else
            {
                gyro.Stop();
            }

            gyroStarted = !gyroStarted;

            if (gyroStarted)
            {
                btnGyro.Content = "Stop Gyro";
            }
            else
            {
                btnGyro.Content = "Start Gyro";
            }
        }

        void gyro_CurrentValueChanged(object sender, SensorReadingEventArgs<GyroscopeReading> e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                txtRotRate.Text = string.Format("{0},{1},{2}", e.SensorReading.RotationRate.X, e.SensorReading.RotationRate.Y, e.SensorReading.RotationRate.Z);
                txtTimestamp.Text = e.SensorReading.Timestamp.ToString();
            });
        }
    }
}