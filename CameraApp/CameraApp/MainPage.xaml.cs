using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CameraApp.Resources;
using Windows.Phone.Media.Capture;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Devices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace CameraApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhotoCaptureDevice captureDevice;
        private int photoCounter = 0;
        PhotoCamera camera;
        MediaLibrary library = new MediaLibrary();

        public MainPage()
        {
            InitializeComponent();
            //UseCamera();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (PhotoCamera.IsCameraTypeSupported(CameraType.Primary))
            {
                camera = new PhotoCamera(CameraType.Primary);
                camera.CaptureImageAvailable += new EventHandler<ContentReadyEventArgs>(cam_CaptureImageAvailable);
                camera.AutoFocusCompleted += camera_AutoFocusCompleted;
                viewFinderBrush.SetSource(camera);
            }
            else
                TxtMessage.Text = "A camera is not available on this device.";
        }

        void camera_AutoFocusCompleted(object sender, CameraOperationCompletedEventArgs e)
        {
            PhotoCamera camera = (PhotoCamera)sender;
            camera.CaptureImage();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (camera != null)
                camera.Dispose();
        }


        private void viewfinder_Hold(object sender, GestureEventArgs e)
        {
            if(camera!=null)
            {
                try
                {
                    camera.Focus();
                }
                catch(Exception ex)
                {
                    TxtMessage.Text = ex.Message;
                }
            }
        }

        private void cam_CaptureImageAvailable(object sender, ContentReadyEventArgs e)
        {
            photoCounter++;
            string filename = photoCounter + ".bmp";
            Deployment.Current.Dispatcher.BeginInvoke(delegate(){
                TxtMessage.Text = "Captured image availabe, saving image.";
            });
            library.SavePictureToCameraRoll(filename, e.ImageStream);
            Deployment.Current.Dispatcher.BeginInvoke(delegate(){
                TxtMessage.Text="Image has been saved in the camera roll.";
            });
        }


        private async void UseCamera()
        {
            captureDevice = await InitializeCamera();
            CameraCaptureSequence seq = captureDevice.CreateCaptureSequence(1);
            viewFinderBrush.SetSource(seq.Frames[0]);
        }

        private async Task<PhotoCaptureDevice> InitializeCamera()
        {
            PhotoCaptureDevice PhotoCaptureDevice = null;

            if (PhotoCaptureDevice.AvailableSensorLocations.Contains(CameraSensorLocation.Back))
            {
                System.Collections.Generic.IReadOnlyList<Windows.Foundation.Size> SupportedResolutions =
                    PhotoCaptureDevice.GetAvailableCaptureResolutions(CameraSensorLocation.Back);
                Windows.Foundation.Size res = SupportedResolutions[0];
                PhotoCaptureDevice = await PhotoCaptureDevice.OpenAsync(CameraSensorLocation.Back, res);
            }
            return PhotoCaptureDevice;
        }
    }
}