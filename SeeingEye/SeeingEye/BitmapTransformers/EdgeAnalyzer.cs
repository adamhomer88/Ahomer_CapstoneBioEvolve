using Microsoft.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SeeingEye.BitmapTransformers
{
    class EdgeAnalyzer : BitmapTransformer
    {
        int[] pixels;
        Dictionary<Point, byte[]> pixelMap = new Dictionary<Point,byte[]>();
        WriteableBitmap image;
        public int TransformedImageHeight { get; set; }
        public int TransformedImageWidth { get; set; }
        public EdgeAnalyzer(WriteableBitmap image)
        {
            this.image = image;
            this.pixels = image.Pixels;
            this.TransformedImageHeight = image.PixelHeight/3;
            this.TransformedImageWidth = image.PixelWidth/3;
        }

        public BitmapSource Transform()
        {
            int[] grayScale = new int[pixels.Length];
            WriteableBitmap transformedImage = new WriteableBitmap(TransformedImageWidth, TransformedImageHeight);
            int beginXScan = (image.PixelWidth / 2) - TransformedImageWidth/2;
            int beginYScan = (image.PixelHeight / 2) - TransformedImageHeight/2;
            int endXScan = (image.PixelWidth / 2) + TransformedImageWidth / 2;
            int endYScan = (image.PixelHeight / 2) + TransformedImageHeight / 2;
            int xTransformed = 0;
            int yTransformed = 0;
            for (int y = beginYScan; y < endYScan; y++)
            {
                for (int x = beginXScan; x < endXScan; x++)
                {
                    double deltaX = argbPixelDX(x, y);
                    double deltaY = argbPixelDY(x, y);
                    double magnitude = gradientMagnitude(deltaX, deltaY);
                    double theta = gradientDirection(deltaX, deltaY);
                    if (magnitude >= 20)
                    {
                        transformedImage.SetPixel(xTransformed, yTransformed, Color.FromArgb(255, 255, 255, 255));
                    }
                    else
                        transformedImage.SetPixel(xTransformed, yTransformed, Color.FromArgb(255, 0, 0, 0));
                    xTransformed++;
                }
                yTransformed++;
                xTransformed = 0;
            }
            return transformedImage;
            //    for (int i = beginScan; i < image.Pixels.Length - beginScan; i++)
            //    {
            //        double deltaX = argbPixelDX(x, y);
            //        double deltaY = argbPixelDY(x, y);
            //        double magnitude = gradientMagnitude(deltaX, deltaY);
            //        double theta = gradientDirection(deltaX, deltaY);
            //        if (magnitude >= 20)
            //        {
            //            transformedImage.SetPixel(x, y, Color.FromArgb(255, 255, 255, 255));
            //        }
            //        else
            //            transformedImage.SetPixel(x, y, Color.FromArgb(255, 0, 0, 0));
            //        x++;
            //        if (x == (image.PixelWidth / 2 + 70))
            //        {
            //            y++;
            //            x = beginScan;
            //        }
            //    }
            //return transformedImage;
        }

        private double Luminosity(Color pixel)
        {
            double redGray = (byte)(pixel.R *0.21);
            double greenGray = (byte)(pixel.G * 0.72);
            double blueGray = (byte)(pixel.B * 0.07);
            return redGray+greenGray+blueGray;
        }

        private double argbPixelDY(int x, int y)
        {
            double defaultDelta = 1.0;
            double deltaY = 1.0;
            if (notInRangeY(x, y))
                return defaultDelta;
            deltaY = (Luminosity(image.GetPixel(x, y-1)) - Luminosity(image.GetPixel(x, y+1)));
            return deltaY;
        }

        private bool notInRangeY(int x, int y)
        {
            return y == 0 || y == image.PixelHeight-1;
        }

        private double argbPixelDX(int x, int y)
        {
            double defaultDelta = 1.0;
            double deltaX = 1.0;
            if (notInRangeX(x, y))
                return defaultDelta;
            deltaX = Luminosity(image.GetPixel(x-1, y)) - Luminosity(image.GetPixel(x+1, y));
            return deltaX;
        }

        private bool notInRangeX(int x, int y)
        {
            return x == 0 || x == image.PixelWidth-1;
        }

        private double gradientMagnitude(double deltaX, double deltaY)
        {
            return Math.Sqrt(Math.Pow(deltaX,2) + Math.Pow(deltaY,2));
        }

        private double gradientDirection(double deltaX, double deltaY)
        {
            double defaultTheta = -200;
            if (deltaY == deltaX)
                return defaultTheta;
            return Math.Atan(deltaY/deltaX)*180/Math.PI;
        }
    }
}
