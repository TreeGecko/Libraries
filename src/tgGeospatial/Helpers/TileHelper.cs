using System;
using System.Text;

namespace TreeGecko.Library.Common.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class TileHelper
    {
        private const double EARTH_RADIUS = 6378137;
        private const double MIN_LATITUDE = -85.05112878;
        private const double MAX_LATITUDE = 85.05112878;
        private const double MIN_LONGITUDE = -180;
        private const double MAX_LONGITUDE = 180;


        /// <summary>
        /// Clips a number to the specified minimum and maximum values.
        /// </summary>
        /// <param name="_n">The number to clip.</param>
        /// <param name="_minValue">Minimum allowable value.</param>
        /// <param name="_maxValue">Maximum allowable value.</param>
        /// <returns>The clipped value.</returns>
        private static double Clip(double _n, double _minValue, double _maxValue)
        {
            return Math.Min(Math.Max(_n, _minValue), _maxValue);
        }

        /// <summary>
        /// Determines the map width and height (in pixels) at a specified level
        /// of detail.
        /// </summary>
        /// <param name="_levelOfDetail">Level of detail, from 1 (lowest detail)
        /// to 23 (highest detail).</param>
        /// <returns>The map width and height in pixels.</returns>
        public static uint MapSize(int _levelOfDetail)
        {
            return (uint)256 << _levelOfDetail;
        }


        /// <summary>
        /// Determines the ground resolution (in meters per pixel) at a specified
        /// latitude and level of detail.
        /// </summary>
        /// <param name="_latitude">Latitude (in degrees) at which to measure the
        /// ground resolution.</param>
        /// <param name="_levelOfDetail">Level of detail, from 1 (lowest detail)
        /// to 23 (highest detail).</param>
        /// <returns>The ground resolution, in meters per pixel.</returns>
        public static double GroundResolution(double _latitude, int _levelOfDetail)
        {
            _latitude = Clip(_latitude, MIN_LATITUDE, MAX_LATITUDE);
            return Math.Cos(_latitude * Math.PI / 180) * 2 * Math.PI * EARTH_RADIUS / MapSize(_levelOfDetail);
        }

        /// <summary>
        /// Determines the map scale at a specified latitude, level of detail,
        /// and screen resolution.
        /// </summary>
        /// <param name="_latitude">Latitude (in degrees) at which to measure the
        /// map scale.</param>
        /// <param name="_levelOfDetail">Level of detail, from 1 (lowest detail)
        /// to 23 (highest detail).</param>
        /// <param name="_screenDpi">Resolution of the screen, in dots per inch.</param>
        /// <returns>The map scale, expressed as the denominator N of the ratio 1 : N.</returns>
        public static double MapScale(double _latitude, int _levelOfDetail, int _screenDpi)
        {
            return GroundResolution(_latitude, _levelOfDetail) * _screenDpi / 0.0254;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_latitude"></param>
        /// <param name="_longitude"></param>
        /// <param name="_levelOfDetail"></param>
        /// <returns></returns>
        public static string LatLongToQuadKey(double _latitude, double _longitude, 
            int _levelOfDetail)
        {
            int pixelX;
            int pixelY;

            LatLongToPixelXY(_latitude, _longitude, _levelOfDetail, out pixelX, out pixelY);

            int tileX;
            int tileY;

            PixelXYToTileXY(pixelX, pixelY, out tileX, out tileY);

            string tileName = TileXYToQuadKey(tileX, tileY, _levelOfDetail);

            return tileName;
        }

        /// <summary>
        /// Converts a point from latitude/longitude WGS-84 coordinates (in degrees)
        /// into pixel XY coordinates at a specified level of detail.
        /// </summary>
        /// <param name="_latitude">Latitude of the point, in degrees.</param>
        /// <param name="_longitude">Longitude of the point, in degrees.</param>
        /// <param name="_levelOfDetail">Level of detail, from 1 (lowest detail)
        /// to 23 (highest detail).</param>
        /// <param name="_pixelX">Output parameter receiving the X coordinate in pixels.</param>
        /// <param name="_pixelY">Output parameter receiving the Y coordinate in pixels.</param>
        public static void LatLongToPixelXY(double _latitude, double _longitude, 
            int _levelOfDetail, out int _pixelX, out int _pixelY)
        {
            _latitude = Clip(_latitude, MIN_LATITUDE, MAX_LATITUDE);
            _longitude = Clip(_longitude, MIN_LONGITUDE, MAX_LONGITUDE);

            double x = (_longitude + 180) / 360;
            double sinLatitude = Math.Sin(_latitude * Math.PI / 180);
            double y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

            uint mapSize = MapSize(_levelOfDetail);
            _pixelX = (int)Clip(x * mapSize + 0.5, 0, mapSize - 1);
            _pixelY = (int)Clip(y * mapSize + 0.5, 0, mapSize - 1);
        }

        /// <summary>
        /// Converts a pixel from pixel XY coordinates at a specified level of detail
        /// into latitude/longitude WGS-84 coordinates (in degrees).
        /// </summary>
        /// <param name="_pixelX">X coordinate of the point, in pixels.</param>
        /// <param name="_pixelY">Y coordinates of the point, in pixels.</param>
        /// <param name="_levelOfDetail">Level of detail, from 1 (lowest detail)
        /// to 23 (highest detail).</param>
        /// <param name="_latitude">Output parameter receiving the latitude in degrees.</param>
        /// <param name="_longitude">Output parameter receiving the longitude in degrees.</param>
        public static void PixelXYToLatLong(int _pixelX, int _pixelY, 
            int _levelOfDetail, out double _latitude, out double _longitude)
        {
            double mapSize = MapSize(_levelOfDetail);
            double x = (Clip(_pixelX, 0, mapSize - 1) / mapSize) - 0.5;
            double y = 0.5 - (Clip(_pixelY, 0, mapSize - 1) / mapSize);

            _latitude = 90 - 360 * Math.Atan(Math.Exp(-y * 2 * Math.PI)) / Math.PI;
            _longitude = 360 * x;
        }

        /// <summary>
        /// Converts pixel XY coordinates into tile XY coordinates of the tile containing
        /// the specified pixel.
        /// </summary>
        /// <param name="_pixelX">Pixel X coordinate.</param>
        /// <param name="_pixelY">Pixel Y coordinate.</param>
        /// <param name="_tileX">Output parameter receiving the tile X coordinate.</param>
        /// <param name="_tileY">Output parameter receiving the tile Y coordinate.</param>
        public static void PixelXYToTileXY(int _pixelX, int _pixelY, 
            out int _tileX, out int _tileY)
        {
            _tileX = _pixelX / 256;
            _tileY = _pixelY / 256;
        }

        /// <summary>
        /// Converts tile XY coordinates into pixel XY coordinates of the upper-left pixel
        /// of the specified tile.
        /// </summary>
        /// <param name="_tileX">Tile X coordinate.</param>
        /// <param name="_tileY">Tile Y coordinate.</param>
        /// <param name="_pixelX">Output parameter receiving the pixel X coordinate.</param>
        /// <param name="_pixelY">Output parameter receiving the pixel Y coordinate.</param>
        public static void TileXYToPixelXY(int _tileX, int _tileY, 
            out int _pixelX, out int _pixelY)
        {
            _pixelX = _tileX * 256;
            _pixelY = _tileY * 256;
        }

        /// <summary>
        /// Converts tile XY coordinates into a QuadKey at a specified level of detail.
        /// </summary>
        /// <param name="_tileX">Tile X coordinate.</param>
        /// <param name="_tileY">Tile Y coordinate.</param>
        /// <param name="_levelOfDetail">Level of detail, from 1 (lowest detail)
        /// to 23 (highest detail).</param>
        /// <returns>A string containing the QuadKey.</returns>
        public static string TileXYToQuadKey(int _tileX, int _tileY, int _levelOfDetail)
        {
            StringBuilder quadKey = new StringBuilder();
            for (int i = _levelOfDetail; i > 0; i--)
            {
                char digit = '0';
                int mask = 1 << (i - 1);
                if ((_tileX & mask) != 0)
                {
                    digit++;
                }
                if ((_tileY & mask) != 0)
                {
                    digit++;
                    digit++;
                }
                quadKey.Append(digit);
            }
            return quadKey.ToString();
        }

        /// <summary>
        /// Converts a QuadKey into tile XY coordinates.
        /// </summary>
        /// <param name="_quadKey">QuadKey of the tile.</param>
        /// <param name="_tileX">Output parameter receiving the tile X coordinate.</param>
        /// <param name="_tileY">Output parameter receiving the tile Y coordinate.</param>
        /// <param name="_levelOfDetail">Output parameter receiving the level of detail.</param>
        public static void QuadKeyToTileXY(string _quadKey, out int _tileX, 
            out int _tileY, out int _levelOfDetail)
        {
            _tileX = _tileY = 0;
            _levelOfDetail = _quadKey.Length;
            for (int i = _levelOfDetail; i > 0; i--)
            {
                int mask = 1 << (i - 1);
                switch (_quadKey[_levelOfDetail - i])
                {
                    case '0':
                        break;

                    case '1':
                        _tileX |= mask;
                        break;

                    case '2':
                        _tileY |= mask;
                        break;

                    case '3':
                        _tileX |= mask;
                        _tileY |= mask;
                        break;

                    default:
                        throw new ArgumentException("Invalid QuadKey digit sequence.");
                }
            }
        }
    }
}