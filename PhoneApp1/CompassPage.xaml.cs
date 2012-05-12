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
    public partial class CompassPage : PhoneApplicationPage
    {
        public CompassPage()
        {
            InitializeComponent();
        }

        bool compassStarted = false;
        Compass compass = new Compass();
        private void btnCompass_Click(object sender, RoutedEventArgs e)
        {
            if (!Compass.IsSupported)
            {
                MessageBox.Show("Compass is not supported on this device.", "Error", MessageBoxButton.OK);
                return;
            }

            compass.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<CompassReading>>(compass_CurrentValueChanged);

            if (!compassStarted)
            {
                compass.Start();
            }
            else
            {
                compass.Stop();
            }

            compassStarted = !compassStarted;

            if (compassStarted)
            {
                btnCompass.Content = "Stop Compass";

            }
            else
            {
                btnCompass.Content = "Start Compass";
            }
        }

        void compass_CurrentValueChanged(object sender, SensorReadingEventArgs<CompassReading> e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                txtHeadingAccuracy.Text = e.SensorReading.HeadingAccuracy.ToString();
                txtMagneticHeading.Text = e.SensorReading.MagneticHeading.ToString();
                txtMagnetometerReading.Text = string.Format("{0},{1},{2}", e.SensorReading.MagnetometerReading.X, e.SensorReading.MagnetometerReading.Y, e.SensorReading.MagnetometerReading.Z);
                txtTrueHeading.Text = e.SensorReading.TrueHeading.ToString();
                txtTimestamp.Text = e.SensorReading.Timestamp.ToString();
            });

        }
    }
}