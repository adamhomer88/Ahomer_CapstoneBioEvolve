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

namespace CameraApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhotoCaptureDevice captureDevice;

        public MainPage()
        {
            InitializeComponent();
            captureDevice = await InitializeCamera();
            
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