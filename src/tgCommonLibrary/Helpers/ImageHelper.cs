using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TreeGecko.Library.Common.Enums;

namespace TreeGecko.Library.Common.Helpers
{
    /// <summary>
    /// 
    /// Copyright 2006 by Concretize, Inc.
    /// 
    /// </summary>
    public static class ImageHelper
    {
        public static byte[] GetBytes(Image _image, ImageFormat _imageFormat)
        {
            byte[] imageData = new byte[0];

            if (_image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    _image.Save(ms, _imageFormat);

                    int imageSize = Convert.ToInt32(ms.Length);
                    imageData = new byte[imageSize];

                    ms.Position = 0;
                    ms.Read(imageData, 0, imageSize);
                }
            }

            return imageData;
        }

        public static Image GetImage(byte[] _data)
        {
            if (_data != null)
            {
                using (MemoryStream ms = new MemoryStream(_data))
                {
                    Image image = Image.FromStream(ms);

                    foreach (var prop in image.PropertyItems)
                    {
                        if (prop.Id == 0x112 || prop.Id == 0x5029)
                        {
                            byte[] propValue = prop.Value;

                            if (propValue.Length > 0)
                            {
                                ExifOrientations orientation = (ExifOrientations) propValue[0];

                                switch (orientation)
                                {
                                    case ExifOrientations.TopLeft:
                                        //Do nothing;
                                        break;
                                    case ExifOrientations.BottomRight:
                                        //Rotate 180

                                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                        break;
                                    case ExifOrientations.RightTop:
                                        //Rotate clock wise 90

                                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                        break;
                                    case ExifOrientations.LeftBottom:
                                        //Rotate ccw 90

                                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                        break;
                                }
                            }
                        }
                    }

                    return image;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_image"></param>
        /// <param name="_height"></param>
        /// <param name="_width"></param>
        /// <returns></returns>
        public static Image GetThumbnail(Image _image, int _height, int _width)
        {
            Image.GetThumbnailImageAbort myCallback = ThumbnailCallback;

            Bitmap bitmap = new Bitmap(_image);

            return bitmap.GetThumbnailImage(_width, _height, myCallback, IntPtr.Zero);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool ThumbnailCallback()
        {
            return false;
        }

        public static Image GetThumbnailOfMaxSize(Image _image, int _maxDimension)
        {
            SizeF sizeF = ImageSizeHelper.SizeThatFits(_image, _maxDimension);
            Size size = sizeF.ToSize();

            return GetThumbnail(_image, size.Height, size.Width);
        }

        public static byte[] GetThumbnailBytesOfMaxSize(Image _image, int _maxDimension)
        {
            SizeF sizeF = ImageSizeHelper.SizeThatFits(_image, _maxDimension);
            Size size = sizeF.ToSize();

            Image image = GetThumbnail(_image, size.Height, size.Width);

            return GetBytes(image, ImageFormat.Jpeg);
        }
    }
}