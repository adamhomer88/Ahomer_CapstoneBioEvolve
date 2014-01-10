using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SeeingEye.BitmapTransformers
{
    class SimpleTransformer : BitmapTransformer
    {
        public WriteableBitmap Image { get; set; }

        public SimpleTransformer(WriteableBitmap Image)
        {
            this.Image = Image;
        }

        public BitmapSource Transform()
        {
            int[] pixelArray = Image.Pixels;
            for (int i = 0; i < Image.Pixels.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(Image.Pixels[i]);
                Image.Pixels[i] = Image.Pixels[0];
            }
            return Image;
        }
    }
}
