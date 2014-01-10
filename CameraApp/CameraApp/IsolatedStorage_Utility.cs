using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CameraApp
{
    public static class IsolatedStorage_Utility
    {
        public static void Save<T>(string fileName, T item)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fstream = new IsolatedStorageFileStream(fileName, System.IO.FileMode.Create, storage))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(fstream, item);
                }
            }
        }

        public static T Load<T>(string filename)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fstream = new IsolatedStorageFileStream(filename, System.IO.FileMode.Open, storage))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    return (T)serializer.ReadObject(fstream);
                }
            }
        }

        public static byte[] ImageToByteArray(BitmapSource bitmapSource)
        {
            using (MemoryStream mstream = new MemoryStream())
            {
                WriteableBitmap bitmap = new WriteableBitmap(bitmapSource);
                Extensions.SaveJpeg(bitmap, mstream, bitmapSource.PixelWidth, bitmapSource.PixelHeight, 0, 100);
                return mstream.ToArray();
            }
        }

        public static BitmapSource ByteArrayToImage(byte[] bytes)
        {
            BitmapImage image = null;
            using (MemoryStream mstream = new MemoryStream(bytes,0,bytes.Length))
            {
                image = new BitmapImage();
                image.SetSource(mstream);
            }

            return image;
        }
    }
}
