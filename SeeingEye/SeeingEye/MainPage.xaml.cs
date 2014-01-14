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

namespace SeeingEye
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhotoCamera camera;
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
            if (PhotoCamera.IsCameraTypeSupported(CameraType.Primary))
            {
                camera = new PhotoCamera(CameraType.Primary);
                camera.CaptureImageAvailable += new EventHandler<ContentReadyEventArgs>(camera_CaptureAvailabe);
                camera.AutoFocusCompleted+= new EventHandler<CameraOperationCompletedEventArgs>(camera_AutoFocusCompleted);
                camera.Initialized += new EventHandler<CameraOperationCompletedEventArgs>(camera_InitializationCompleted);
                ViewFinderBrush.SetSource(camera);
            }
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
    }
}