using System;
using System.Drawing;
using TreeGecko.Library.Geospatial.Geoframeworks.Objects;

namespace TreeGecko.Library.Geospatial.Geoframeworks.Helpers
{
    /// <summary>
    /// Provides additional functionality for the RectangleF structure.
    /// </summary>
    public static class RectangleFHelper
    {
        /// <summary>
        /// Returns the point at the center of the specified rectangle.
        /// </summary>
        /// <param name="_rectangle"></param>
        /// <returns></returns>
        public static PointF Center(RectangleF _rectangle)
        {
            return new PointF((_rectangle.Width*.5f) + _rectangle.X,
                (_rectangle.Height*.5f) + _rectangle.Y);
        }

        /// <summary>
        /// Increases the height or broadens the width of a rectangle to match the specified aspect ratio.
        /// </summary>
        /// <param name="_rectangle"></param>
        /// <param name="_aspectRatio"></param>
        /// <returns></returns>
        public static RectangleF ToAspectRatio(RectangleF _rectangle, float _aspectRatio)
        {
            float projectedAspect = (_rectangle.Width/_rectangle.Height);

            if (_aspectRatio > projectedAspect)
            {
                _rectangle.Inflate((_aspectRatio*_rectangle.Height - _rectangle.Width)*.5f, 0);
            }
            else if (_aspectRatio < projectedAspect)
            {
                _rectangle.Inflate(0, (_rectangle.Width/_aspectRatio - _rectangle.Height)*.5f);
            }
            return _rectangle;
        }

        /// <summary>
        /// Shortens the height or narrows the width of a rectangle to match the specified aspect ratio.
        /// </summary>
        /// <param name="_rectangle"></param>
        /// <param name="_aspectRatio"></param>
        /// <returns></returns>
        public static RectangleF ToAspectRatioB(RectangleF _rectangle, float _aspectRatio)
        {
            float projectedAspect = (_rectangle.Width/_rectangle.Height);

            if (_aspectRatio > projectedAspect)
            {
                _rectangle.Inflate(0, (_rectangle.Width/_aspectRatio - _rectangle.Height)*.5f);
            }
            else if (_aspectRatio < projectedAspect)
            {
                _rectangle.Inflate((_aspectRatio*_rectangle.Height - _rectangle.Width)*.5f, 0);
            }
            return _rectangle;
        }

        /// <summary>
        /// Returns the corners of a rectangle
        /// </summary>
        /// <param name="_rectangle"></param>
        /// <returns></returns>
        public static PointF[] Corners(RectangleF _rectangle)
        {
            return new[]
            {
                new PointF(_rectangle.Left, _rectangle.Top),
                new PointF(_rectangle.Right, _rectangle.Top),
                new PointF(_rectangle.Right, _rectangle.Bottom),
                new PointF(_rectangle.Left, _rectangle.Bottom)
            };
        }

        /// <summary>
        /// Calculates the bounding rectangle for the supplied points.
        /// </summary>
        /// <returns></returns>
        public static RectangleF ComputeBoundingBox(PointF[] _projectedPoints)
        {
            // ffs
            if (_projectedPoints.Length == 0) return RectangleF.Empty;

            // Now figure out which points represent the maximum bounds, starting
            // with the first point
            float projectedLeft = _projectedPoints[0].X;
            float projectedRight = _projectedPoints[0].X;
            float projectedTop = _projectedPoints[0].Y;
            float projectedBottom = _projectedPoints[0].Y;

            // Now consider all other points
            int limit = _projectedPoints.Length;
            for (int index = 1; index < limit; index++)
            {
                // Get the current projected point
                PointF projectedPoint = _projectedPoints[index];

                // Now see if it exceeds the current bounds
                if (projectedPoint.X < projectedLeft)
                    projectedLeft = projectedPoint.X;
                if (projectedPoint.X > projectedRight)
                    projectedRight = projectedPoint.X;
                if (projectedPoint.Y < projectedTop)
                    projectedTop = projectedPoint.Y;
                if (projectedPoint.Y > projectedBottom)
                    projectedBottom = projectedPoint.Y;
            }

            // finally, return a rectangle with these bounds
            return RectangleF.FromLTRB(projectedLeft, projectedTop, projectedRight, projectedBottom);
        }

        /// <summary>
        /// Returns the length of the hypotenuse of the specified rectangle.
        /// </summary>
        /// <param name="_rectangle"></param>
        /// <returns></returns>
        public static float Hypotenuse(RectangleF _rectangle)
        {
            return (float) Math.Sqrt(Math.Pow(_rectangle.Width, 2) + Math.Pow(_rectangle.Height, 2));
        }

        /// <summary>
        /// Centers the rectangle on a specific point.
        /// </summary>
        /// <param name="_rectangle">The rectangle to translate</param>
        /// <param name="_point">The point on which to center the reactangle</param>
        /// <returns>The new rectangle centered on the specified point</returns>
        public static RectangleF CenterOn(RectangleF _rectangle, PointF _point)
        {
            PointF center = Center(_rectangle);

            _rectangle.Offset(_point.X - center.X, _point.Y - center.Y);
            return _rectangle;
        }

        /// <summary>
        /// Returns whether any one of a rectangle's sides is a NaN (not a number).
        /// </summary>
        /// <param name="_rectangle"></param>
        /// <returns></returns>
        public static bool IsNaN(RectangleF _rectangle)
        {
            return double.IsNaN(_rectangle.X*_rectangle.Y*_rectangle.Right*_rectangle.Bottom);
        }

        /// <summary>
        /// Rotates a rectangle around its center
        /// </summary>
        /// <param name="_rectangle">The rectangle to apply the rotation</param>
        /// <param name="_angle">The clockwise angle of the rotation</param>
        /// <returns>The minimum bounding rectangle (MBR) of the rotated rectangle</returns>
        public static RectangleF Rotate(RectangleF _rectangle, Angle _angle)
        {
            return RotateAt(_rectangle, (float) _angle.DecimalDegrees, Center(_rectangle));
        }

        /// <summary>
        /// Rotates a rectangle around its center
        /// </summary>
        /// <param name="_rectangle">The rectangle to apply the rotation</param>
        /// <param name="_angle">The clockwise angle of the rotation</param>
        /// <returns>The minimum bounding rectangle (MBR) of the rotated rectangle</returns>
        public static RectangleF Rotate(RectangleF _rectangle, float _angle)
        {
            return RotateAt(_rectangle, _angle, Center(_rectangle));
        }

        /// <summary>
        /// Rotates a rectangle around a coordinate
        /// </summary>
        /// <param name="_rectangle">The rectangle to apply the rotation</param>
        /// <param name="_angle">The clockwise angle of the rotation</param>
        /// <param name="_center"></param>
        /// <returns>The minimum bounding rectangle (MBR) of the rotated rectangle</returns>
        public static RectangleF RotateAt(RectangleF _rectangle, Angle _angle, PointF _center)
        {
            return RotateAt(_rectangle, (float) _angle.DecimalDegrees, _center);
        }

        /// <summary>
        /// Rotates a rectangle around a coordinate
        /// </summary>
        /// <param name="_rectangle">The rectangle to apply the rotation</param>
        /// <param name="_angle">The clockwise angle of the rotation</param>
        /// <param name="_center"></param>
        /// <returns>The minimum bounding rectangle (MBR) of the rotated rectangle</returns>
        public static RectangleF RotateAt(RectangleF _rectangle, float _angle, PointF _center)
        {
            // The graphics transform method only accepts arrays :P
            PointF[] points =
            {
                new PointF(_rectangle.Left, _rectangle.Top),
                new PointF(_rectangle.Right, _rectangle.Top),
                new PointF(_rectangle.Right, _rectangle.Bottom),
                new PointF(_rectangle.Left, _rectangle.Bottom)
            };

            Matrix rotationMatrix = new Matrix();
            rotationMatrix.RotateAt(_angle, _center);
            rotationMatrix.TransformPoints(points);
            rotationMatrix.Dispose();

            // Return the result
            return ComputeBoundingBox(points);
        }

        /// <summary>
        /// Rotates a coordinate around a coordinate
        /// </summary>
        /// <param name="_point">The point to apply the rotation</param>
        /// <param name="_angle">The clockwise angle of the rotation</param>
        /// <param name="_center"></param>
        /// <returns>The rectangle resulting from the rotation of the upperleft and lower rightt corners of the input rectangle</returns>
        public static PointF RotatePointF(PointF _point, Angle _angle, PointF _center)
        {
            return RotatePointF(_point, (float) _angle.DecimalDegrees, _center);
        }

        /// <summary>
        /// Rotates a coordinate around a coordinate
        /// </summary>
        /// <param name="_point">The point to apply the rotation</param>
        /// <param name="_angle">The clockwise angle of the rotation</param>
        /// <param name="_center"></param>
        /// <returns>The rectangle resulting from the rotation of the upperleft and lower rightt corners of the input rectangle</returns>
        public static PointF RotatePointF(PointF _point, float _angle, PointF _center)
        {
            // The graphics transform method only accepts arrays :P
            PointF[] points = new PointF[1]
            {
                _point,
            };

            Matrix rotationMatrix = new Matrix();
            rotationMatrix.RotateAt(_angle, _center);
            rotationMatrix.TransformPoints(points);
            rotationMatrix.Dispose();

            // Return the result
            return (points[0]);
        }

        public static bool IsRectangle(PointF[] _points)
        {
            if (_points.Length != 4) return false;

            // The rectangle could be rotated, we need to check 2 orientations
            return (
                (
                    _points[0].X == _points[3].X &&
                    _points[0].Y == _points[1].Y &&
                    _points[0].X != _points[2].X &&
                    _points[0].Y != _points[2].Y
                    ) ||
                (
                    _points[0].X == _points[1].X &&
                    _points[0].Y == _points[3].Y &&
                    _points[0].X != _points[2].X &&
                    _points[0].Y != _points[2].Y
                    )
                );
        }
    }
}