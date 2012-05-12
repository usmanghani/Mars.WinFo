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
    public partial class AccelPage : PhoneApplicationPage
    {
        // Constructor
        public AccelPage()
        {
            InitializeComponent();
        }

        bool accelStarted = false;
        Accelerometer accel = new Accelerometer();
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!Accelerometer.IsSupported)
            {
                MessageBox.Show("Accelerometer not supported on this device.", "Error", MessageBoxButton.OK);
                return;
            }

            accel.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accel_CurrentValueChanged);

            if (!accelStarted)
            {
                accel.Start();
            }
            else
            {
                accel.Stop();
            }

            accelStarted = !accelStarted;

            if (accelStarted)
            {
                btnAccel.Content = "Stop Accelerometer";
            }
            else
            {
                btnAccel.Content = "Start Accelerometer";
            }
        }

        void accel_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                txtAccel.Text = string.Format("{0},{1},{2}", e.SensorReading.Acceleration.X, e.SensorReading.Acceleration.Y, e.SensorReading.Acceleration.Z);
                txtTimestamp.Text = e.SensorReading.Timestamp.ToString();
            });
        }
    }
}