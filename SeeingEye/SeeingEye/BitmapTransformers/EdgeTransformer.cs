using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SeeingEye.BitmapTransformers
{
    class EdgeTransformer : BitmapTransformer
    {
        public BitmapTransformer Transformer { get; set; }

        public EdgeTransformer(BitmapTransformer transformer)
        {
            Transformer = transformer;
        }

        public BitmapSource Transform()
        {
            throw new NotImplementedException();
        }

        private int Luminosity()
        {
            return -1;
        }
    }
}
