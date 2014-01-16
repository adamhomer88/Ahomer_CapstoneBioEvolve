using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SeeingEye.Resources;
using Microsoft.Devices;
using System.Windows.Media.Imaging;
using System.IO;
using SeeingEye.BitmapTransformers;
using Windows.Devices.Sensors;
using Microsoft.Xna.Framework;
using Microsoft.Devices.Sensors;

namespace SeeingEye
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhotoCamera camera;
        Motion motionSensor;
        int accelerometerReadingCount = 0;
        const int SIZE_PER_5_MILLISECONDS = 200;
        Vector3[] vectorReadings = new Vector3[SIZE_PER_5_MILLISECONDS];
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void ViewFinderEdge_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //camera.Focus();
            camera.CaptureImage();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                motionSensor = new Motion();
                Dispatcher.BeginInvoke(delegate
                {
                    AccelerometerStatus.Text = "Accelerometer Initialized";
                });
                AccelerometerConfiguration();
                motionSensor.Start();
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(delegate
                {
                    AccelerometerStatus.Text = ex.Message;
                });
            }
            if (PhotoCamera.IsCameraTypeSupported(CameraType.Primary))
            {
                CameraConfiguration();
            }
        }

        private void AccelerometerConfiguration()
        {
            motionSensor.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
            motionSensor.CurrentValueChanged += motionSensor_CurrentValueChanged;
        }

        void motionSensor_CurrentValueChanged(object sender, SensorReadingEventArgs<MotionReading> e)
        {
            Vector3 acceleration = e.SensorReading.DeviceAcceleration;
            Vector3 accelerationMinusGravity = acceleration + e.SensorReading.Gravity;
            Vector3 rotation = e.SensorReading.DeviceRotationRate;
            Dispatcher.BeginInvoke(delegate
            {
                AccelerometerStatus.Text = "X: " + accelerationMinusGravity.X + " \nY: " + accelerationMinusGravity.Y + " \nZ: " + accelerationMinusGravity.Z;
            });
        }

        //private void accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        //{
        //    AccelerometerReading reading = args.Reading;
        //    Vector3 vectorReading = new Vector3((float)reading.AccelerationX, (float)reading.AccelerationY, (float)reading.AccelerationZ);
        //    vectorReadings[accelerometerReadingCount] = vectorReading;
        //    accelerometerReadingCount++;
        //    if (accelerometerReadingCount == SIZE_PER_5_MILLISECONDS)
        //    {
        //        accelerometerReadingCount = 0;
        //        Dispatcher.BeginInvoke(delegate
        //        {
        //                float x = 0.0f;
        //                float y = 0.0f;
        //                float z = 0.0f;
        //                for (int i = 0; i < vectorReadings.Length; i++)
        //                {
        //                    x += vectorReadings[i].X;
        //                    y += vectorReadings[i].Y;
        //                    z += vectorReadings[i].Z;
        //                    vectorReadings[i] = new Vector3();
        //                }
        //                AccelerometerStatus.Text = "X: " + x + " \nY: " + y + " \nZ: " + z;
        //        });
        //    }
        //}

        private void CameraConfiguration()
        {
            camera = new PhotoCamera(CameraType.Primary);
            camera.CaptureImageAvailable += new EventHandler<ContentReadyEventArgs>(camera_CaptureAvailabe);
            camera.AutoFocusCompleted += new EventHandler<CameraOperationCompletedEventArgs>(camera_AutoFocusCompleted);
            camera.Initialized += new EventHandler<CameraOperationCompletedEventArgs>(camera_InitializationCompleted);
            ViewFinderBrush.SetSource(camera);
        }

        private void camera_InitializationCompleted(object sender, CameraOperationCompletedEventArgs e)
        {
           camera.FlashMode = FlashMode.Off;
        }

        private void camera_AutoFocusCompleted(object sender, CameraOperationCompletedEventArgs e)
        {
            camera.CaptureImage();
        }

        private void camera_CaptureAvailabe(object sender, ContentReadyEventArgs e)
        {
            Stream stream = e.ImageStream;
            WriteableBitmap image = null;
            Dispatcher.BeginInvoke(delegate
            {
                image = new WriteableBitmap((int)camera.Resolution.Width, (int)camera.Resolution.Height);
                image.SetSource(e.ImageStream);
                EdgeAnalyzer analyzeEdges = new EdgeAnalyzer(image);
                ViewFinderImage.Source = analyzeEdges.Transform();
            });
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            
        }
    }
}