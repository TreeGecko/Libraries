using System.Drawing;

namespace TreeGecko.Library.Common.Helpers
{
    public static class ImageSizeHelper
    {
        public static SizeF SizeThatFits(Image _image, float _maxDimension)
        {
            if (_image != null)
            {
                if (_image.Size.Width > _image.Size.Height)
                {
                    //Landscape
                    float height = (_maxDimension/_image.Size.Width)*_image.Size.Height;
                    return new SizeF(_maxDimension, height);
                }

                if (_image.Size.Width < _image.Size.Height)
                {
                    //Portrait
                    float width = (_maxDimension/_image.Size.Height)*_image.Size.Width;
                    return new SizeF(width, _maxDimension);
                }

                return new SizeF(_maxDimension, _maxDimension);
            }

            return new SizeF(0, 0);
        }
    }
}