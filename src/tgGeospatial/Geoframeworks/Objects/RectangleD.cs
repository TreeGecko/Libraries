using System;
using System.Drawing;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TreeGecko.Library.Geospatial.Geoframeworks.Objects
{
	/// <summary>Represents a highly-precise rectangle.</summary>
	/// <remarks>
	/// 	<para>This class functions similar to the <strong>RectangleF</strong> class in the
	///     <strong>System.Drawing</strong> namespace, except that it uses
	///     double-floating-point precision and is also supported on the Compact Framework
	///     edition of the <strong>GeoFramework</strong>.</para>
	/// 	<para>Instances of this class are guaranteed to be thread-safe because it is
	///     immutable (its properties can only be changed during constructors).</para>
	/// </remarks>
    public struct RectangleD : IFormattable, IEquatable<RectangleD>, IXmlSerializable
    {
        private readonly double m_Top;
        private readonly double m_Bottom;
        private readonly double m_Left;
        private readonly double m_Right;
        
		#region Fields

		/// <summary>
		/// Represents a RectangleD having no size.
		/// </summary>
		public static readonly RectangleD Empty = new RectangleD(0.0, 0.0, 0.0, 0.0);
		
        #endregion

		#region Constructors

	    /// <summary>
	    /// Creates a new instance using the specified location, width, and height.
	    /// </summary>
	    /// <param name="_location"></param>
	    /// <param name="_size"></param>
	    public RectangleD(PointD _location, SizeD _size) 
            : this(_location.X, _location.Y, _location.X + _size.Width, _location.Y + _size.Height)
		{}

		/// <summary>
		/// Creates a new instance using the specified location, width, and height.
		/// </summary>
		/// <param name="_location"></param>
		/// <param name="_width"></param>
		/// <param name="_height"></param>
		public RectangleD(PointD _location, double _width, double _height)
			: this(_location.X, _location.Y, _location.X + _width, _location.Y + _height)
		{}

		/// <summary>
		/// Creates a new instance using the specified upper-left and lower-right coordinates.
		/// </summary>
		public RectangleD(PointD _upperLeft, PointD _lowerRight) 
            : this(_upperLeft.X, _upperLeft.Y, _lowerRight.X, _lowerRight.Y)
		{}
        
		/// <summary>
		/// Creates a new instance using the specified latitudes and longitudes.
		/// </summary>
		public RectangleD(double _left, double _top, double _right, double _bottom) 
		{
			m_Top = _top < _bottom ? _top : _bottom;
			m_Left = _left < _right ? _left : _right;
			m_Bottom = _bottom > _top ? _bottom : _top;
			m_Right = _right > _left ? _right : _left;
		}

		public RectangleD(string _value) 
            : this(_value, CultureInfo.CurrentCulture)
		{}

		public RectangleD(string _value, CultureInfo _culture)
		{
            // Split the string into words
            string[] Values = _value.Split(_culture.TextInfo.ListSeparator.ToCharArray());

            // How many words are there?
            if (Values.Length == 4)
            {
                // Extract each item
                m_Top = double.Parse(Values[0], _culture);
                m_Left = double.Parse(Values[1], _culture);
                m_Bottom = double.Parse(Values[2], _culture);
                m_Right = double.Parse(Values[3], _culture);
            }
            else
            {
                throw new FormatException(Properties.Resources.RectangleD_InvalidFormat);
            }
		}

        public RectangleD(XmlReader _reader)
        {
            m_Top = double.Parse(
               _reader.GetAttribute("Top"), CultureInfo.InvariantCulture);
            m_Bottom = double.Parse(
                _reader.GetAttribute("Bottom"), CultureInfo.InvariantCulture);
            m_Left = double.Parse(
                _reader.GetAttribute("Left"), CultureInfo.InvariantCulture);
            m_Right = double.Parse(
                _reader.GetAttribute("Right"), CultureInfo.InvariantCulture);
        }

		#endregion

        #region Public Properties
        
		/// <summary>Returns the top side of the rectangle.</summary>
		/// <value>A <see cref="Latitude"></see> object marking the southern-most latitude.</value>
		public double Top
		{
			get
			{
				return m_Top;
			}
		}

		/// <summary>Returns the bottom side of the rectangle.</summary>
		/// <value>A <see cref="Latitude"></see> object marking the southern-most latitude.</value>
		public double Bottom
		{
			get
			{
				return m_Bottom;
			}
		}
        
		/// <summary>Returns the left side of the rectangle.</summary>
		public double Left
		{
			get
			{
				return m_Left;
			}
		}

		/// <summary>Returns the right side of the rectangle.</summary>
		public double Right
		{
			get
			{
				return m_Right;
			}
		}
        
		/// <summary>Returns the top-to-bottom size of the rectangle.</summary>
		public double Height
		{
			get
			{
				return m_Bottom - m_Top;
			}
		}
        
		/// <summary>Returns the left-to-right size of the rectangle.</summary>
		public double Width
		{
			get
			{
				return m_Right - m_Left;
			}
		}
        
		/// <summary>Returns the width and height of the rectangle.</summary>
		public SizeD Size
		{
			get
			{
				return new SizeD(Width, Height);
			}
		}

        /// <summary>Returns the point at the center of the rectangle.</summary>
        public PointD Center
        {
            get
            {
                return new PointD(m_Left + Width * 0.5, m_Top + Height * 0.5);
            }
        }

		/// <summary>Returns the point at the upper-left corner of the rectangle.</summary>
		public PointD UpperLeft
		{
			get
			{
				return new PointD(m_Left, m_Top);
			}
		}

		/// <summary>Returns the point at the upper-right corner of the rectangle.</summary>
		public PointD UpperRight
		{
			get
			{
				return new PointD(m_Right, m_Top);
			}
		}

		/// <summary>Returns the point at the lower-left corner of the rectangle.</summary>
		public PointD LowerLeft
		{
			get
			{
				return new PointD(m_Left, m_Bottom);
			}
		}

		/// <summary>Returns the point at the lower-right corner of the rectangle.</summary>
		public PointD LowerRight
		{
			get
			{
				return new PointD(m_Right, m_Bottom);
			}
		}

        /// <summary>Returns the ratio of the rectangle's width to its height.</summary>
        /// <remarks>
        /// This property returns the ratio of the RectangleDs width to its height (width / height).  This
        /// property gives an indication of the RectangleD's shape.  An aspect ratio of one indicates
        /// a square, whereas an aspect ratio of two indicates a RectangleD which is twice as wide as
        /// it is high.  
        /// </remarks>
        public double AspectRatio
        {
            get
            {
                return Width / Height;
            }
        }

		/// <summary>Indicates if the rectangle has any value.</summary>
		public bool IsEmpty
		{
			get
			{
				return (m_Top.Equals(0) 
                    && m_Bottom.Equals(0)
                    && m_Left.Equals(0)
                    && m_Right.Equals(0));
			}
		}

        #endregion

        #region Public Methods


        /// <summary>
        /// Changes the size and shape of the RectangleD to match the aspect ratio of the specified RectangleD.
        /// </summary>
        /// <param name="_size"></param>
        /// <returns></returns>
        /// <remarks>This method will expand a RectangleD outward, from its center point, until
        /// the ratio of its width to its height matches the specified value.</remarks>
        public RectangleD ToAspectRatio(SizeD _size)
        {
            // Calculate the aspect ratio
            return ToAspectRatio(_size.Width / _size.Height);
        }


        /// <summary>
        /// Returns whether the specified rectangle is not overlapping the current
        /// instance.
        /// </summary>
        public bool IsDisjointedFrom(RectangleD _rectangle)
        {
            return !IsOverlapping(_rectangle);
        }

        /// <summary>
        /// Indicates if the specified RectangleD is entirely within the current RectangleD.
        /// </summary>
        /// <param name="_rectangle"></param>
        /// <returns></returns>
        public bool IsEnclosing(RectangleD _rectangle)
        {
            return !(_rectangle.Left < m_Left || _rectangle.Right > m_Right || _rectangle.Top < m_Top || _rectangle.Bottom > m_Bottom);
        }

        public bool IsEnclosing(PointD _point)
        {
            return !(_point.X < m_Left || _point.Y < m_Top || _point.X > m_Right || _point.Y > m_Bottom);
        }

        /// <summary>Moves the rectangle so that the specified point is at its center.</summary>
        public RectangleD CenterOn(PointD _point)
        {
            return new RectangleD(new PointD(_point.X - (Width * 0.5), _point.Y - (Height * 0.5)), Size);
        }

        public RectangleD Inflate(SizeD size)
        {
            return Inflate(size.Width, size.Height);
        }

        /// <summary>
        /// Returns a copy of the current instance.
        /// </summary>
        /// <returns></returns>
        public RectangleD Clone()
        {
            return new RectangleD(m_Top, m_Left, m_Bottom, m_Right);
        }

        /// <summary>
        /// Expands the edges of the RectangleD to contain the specified position.
        /// </summary>
        /// <param name="_point">A <strong>PointD</strong> object to surround.</param>
        /// <returns>A <strong>RectangleD</strong> which contains the specified position.</returns>
        /// <remarks>If the specified position is already enclosed, the current instance will be returned.</remarks>
        public RectangleD UnionWith(PointD _point)
        {
            // Does the box already contain the position?  If so, do nothing
            if (IsEnclosing(_point)) return this;
            // Return the expanded box
            return new RectangleD(
                _point.Y < m_Top ? _point.Y : m_Top,
                _point.X < m_Left ? _point.X : m_Left,
                _point.Y > m_Bottom ? _point.Y : m_Bottom,
                _point.X > m_Right ? _point.X : m_Right);
        }

        /// <summary>
        /// Returns a rectangle enclosing the corner points of the current instance, rotated
        /// by the specified amount around a specific point.
        /// </summary>
        /// <returns>A new <strong>RectangleD</strong> containing the rotated rectangle.</returns>
        /// <remarks><para>When a rectangle is rotated, the total width and height it occupies may be larger
        /// than the rectangle's own width and height.  This method calculates the smallest rectangle
        /// which encloses the rotated rectangle.</para>
        /// 	<para>[TODO: Include before and after picture; this is confusing.]</para>
        /// </remarks>
        public RectangleD RotateAt(Angle _angle, PointD _center)
        {
            if (_angle.IsEmpty) return this;
            // Rotate each corner point
            PointD[] points =
            {
                UpperLeft.RotateAt(_angle, _center),
                UpperRight.RotateAt(_angle, _center),
                LowerRight.RotateAt(_angle, _center),
                LowerLeft.RotateAt(_angle, _center)
            };
            
            // Now return the smallest rectangle which encloses these points
            return FromArray(points);
        }

        /// <summary>
        /// Returns a rectangle enclosing the corner points of the current instance, rotated
        /// by the specified amount.
        /// </summary>
        public RectangleD Rotate(Angle _angle)
        {
            if (_angle.IsEmpty) return this;
            // Rotate each corner point
            PointD[] points =
            {
                UpperLeft.RotateAt(_angle, Center),
                UpperRight.RotateAt(_angle, Center),
                LowerRight.RotateAt(_angle, Center),
                LowerLeft.RotateAt(_angle, Center)
            };
            
            // Now return the smallest rectangle which encloses these points
            return FromArray(points);
        }

        /// <summary>Returns the corner points of the rectangle as an array.</summary>
        public PointD[] ToArray()
        {
            return new[] { UpperLeft, UpperRight, LowerRight, LowerLeft, UpperLeft };
        }

        /// <summary>
        /// Changes the size and shape of the RectangleD to match the specified aspect ratio.
        /// </summary>
        /// <param name="_aspectRatio"></param>
        /// <returns></returns>
        /// <remarks>This method will expand a RectangleD outward, from its center point, until
        /// the ratio of its width to its height matches the specified value.</remarks>
        public RectangleD ToAspectRatio(double _aspectRatio)
        {
            double currentAspect = AspectRatio;

            // Do the values already match?
            if (currentAspect .Equals(_aspectRatio)) 
                return this;
            
            // Is the new ratio higher or lower?
            if (_aspectRatio > currentAspect)
            {
                // Inflate the RectangleD to the new height minus the current height
                // TESTS OK
                return Inflate(_aspectRatio * Height - Width, 0);
            }
            else
            {
                // Inflate the RectangleD to the new height minus the current height
                return Inflate(0, Width / _aspectRatio - Height);
            }
        }

        public RectangleD Translate(PointD _offset)
        {
            return new RectangleD(UpperLeft.Add(_offset), Size);
        }

        public RectangleD Translate(double _offsetX, double _offsetY)
        {
            return new RectangleD(UpperLeft.Add(_offsetX, _offsetY), Size);
        }

        /// <summary>
        /// Changes the size and shape of the RectangleD to match the aspect ratio of the specified RectangleD.
        /// </summary>
        /// <param name="_rectangle"></param>
        /// <returns></returns>
        /// <remarks>This method will expand a RectangleD outward, from its center point, until
        /// the ratio of its width to its height matches the specified value.</remarks>
        public RectangleD ToAspectRatio(RectangleD _rectangle)
        {
            // Calculate the aspect ratio
            return ToAspectRatio(_rectangle.Width / _rectangle.Height);
        }

        public RectangleD Inflate(double _widthOffset, double _heightOffset)
        {
            _widthOffset *= .5;
            _heightOffset *= .5;

            double top = m_Top - _heightOffset;
            double bottom = m_Bottom + _heightOffset;
            double left = m_Left - _widthOffset;
            double right = m_Right + _widthOffset;
            
            if (top > bottom || right < left)
                return this;
            
            return new RectangleD(left, top, right, bottom);
        }

        public RectangleD IntersectionOf(RectangleD _rectangle)
        {
            // Return nothing if no intersection is possible
            if (!IsIntersectingWith(_rectangle)) return Empty;

            // Return the rectangle representing the intersection
            double newLeft = (_rectangle.Left > m_Left ? _rectangle.Left : m_Left);
            double newTop = (_rectangle.Top > m_Top ? _rectangle.Top : m_Top);
            double newRight = (_rectangle.Right < m_Right ? _rectangle.Right : m_Right);
            double newBottom = (_rectangle.Bottom < m_Bottom ? _rectangle.Bottom : m_Bottom);

            return new RectangleD(newTop, newLeft, newBottom, newRight);
        }

        /// <summary>Returns whether the current instance overlaps the specified rectangle.</summary>
        public bool IsIntersectingWith(RectangleD rectangle)
        {
            if (rectangle.Left >= m_Left && rectangle.Left <= m_Right)
                if (rectangle.Top >= m_Top && rectangle.Top <= m_Bottom)
                    return true;
                else if (rectangle.Bottom >= m_Top && rectangle.Bottom <= m_Bottom)
                    return true;
                else if (rectangle.Right >= m_Left && rectangle.Right <= m_Right)
                    if (rectangle.Top >= m_Top && rectangle.Top <= m_Bottom)
                        return true;
                    else if (rectangle.Bottom >= m_Top && rectangle.Bottom <= m_Bottom)
                        return true;
            return false;
            //
            //			return ((rectangle.Left >= Left ) && (rectangle.Left <= Right) && (rectangle.Latitude >= Latitude) && (rectangle.Latitude <= Bottom))
            //				|| ((rectangle.Left + rectangle.Width >= Left ) && (rectangle.Left + rectangle.Width <= Left + Width) && (rectangle.Latitude >= Latitude) && (rectangle.Latitude <= Latitude + Height))
            //				|| ((rectangle.Left >= Left ) && (rectangle.Left <= Left + Width) && (rectangle.Latitude + rectangle.Height >= Latitude) && (rectangle.Latitude + rectangle.Height <= Latitude + Height))
            //				|| ((rectangle.Left + rectangle.Width >= Left ) && (rectangle.Left + rectangle.Width <= Left + Width) && (rectangle.Latitude + rectangle.Height >= Latitude) && (rectangle.Latitude + rectangle.Height <= Latitude + Height));
        }

        /// <summary>
        /// Indicates if the specified RectangleD shares any of the same 2D space as the current instance.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public bool IsOverlapping(RectangleD rectangle)
        {
            return !((rectangle.Top > m_Bottom)
            || (rectangle.Bottom < m_Top)
            || (rectangle.Left > m_Right)
            || (rectangle.Right < m_Left));
            //return true;

            //			if(rectangle.Top < Top)
            //				if(rectangle.Bottom < Top) return false;
            //			if(rectangle.Bottom > Bottom)
            //				if(rectangle.Top > Bottom) return false;
            //			if(rectangle.Left < Left)
            //				if(rectangle.Right < Left) return false;
            //			if(rectangle.Right > Right)
            //				if(rectangle.Left > Right) return false;
            //			return true;
        }

        /// <summary>
        /// Returns whether the current instance is surrounded on all sides by the specified
        /// rectangle.
        /// </summary>
        public bool IsInsideOf(RectangleD rectangle)
        {
            return (Left > rectangle.Left
                && Right < rectangle.Right
                && m_Top > rectangle.Top
                && m_Bottom < rectangle.Bottom);
        }

        /// <summary>
        /// Returns whether the current instance surrounds the center point of the specified
        /// rectangle.
        /// </summary>
        public bool IsEnclosingCenter(RectangleD rectangle)
        {
            return (rectangle.Left <= Center.X
                && rectangle.Right >= Center.X
                && rectangle.Top <= Center.Y
                && rectangle.Bottom >= Center.Y);
        }

        /// <summary>
        /// Returns whether the specified rectangle shares a side with the specified
        /// rectangle.
        /// </summary>
        /// <returns>A <strong>Boolean</strong>, true if the specified rectangle shares one
        /// (and only one) side.</returns>
        /// <remarks>The method will return false if the specified rectangle intersects with the
        /// current instance.</remarks>
        public bool IsAdjacentTo(RectangleD rectangle)
        {
            if (rectangle.Top == m_Top)
                if (rectangle.Left >= m_Left && rectangle.Left <= m_Right)
                    return true;
                else if (rectangle.Right >= m_Left && rectangle.Right <= m_Right)
                    return true;
                else if (rectangle.Bottom == m_Bottom)
                    if (rectangle.Left >= m_Left && rectangle.Left <= m_Right)
                        return true;
                    else if (rectangle.Right >= m_Left && rectangle.Right <= m_Right)
                        return true;
                    else if (rectangle.Left == m_Left)
                        if (rectangle.Top >= m_Top && rectangle.Top <= m_Bottom)
                            return true;
                        else if (rectangle.Bottom >= m_Top && rectangle.Bottom <= m_Bottom)
                            return true;
                        else if (rectangle.Right == m_Right)
                            if (rectangle.Top >= m_Top && rectangle.Top <= m_Bottom)
                                return true;
                            else if (rectangle.Bottom >= m_Top && rectangle.Bottom <= m_Bottom)
                                return true;
            return false;
        }

        public bool IsOverlapping(PointD point)
        {
            if (point.X < m_Left) return false;
            if (point.X > m_Right) return false;
            if (point.Y > m_Top) return false;
            if (point.Y < m_Bottom) return false;
            return true;
        }

        public RectangleD UnionWith(RectangleD rectangle)
        {
            // Build a new rectangle from these rectangles
            //Console.WriteLine("rectangle #1: " + ToString());
            //Console.WriteLine("rectangle #2: " + rectangle.ToString());

            return new RectangleD(rectangle.Top < m_Top ? rectangle.Top : m_Top,
                rectangle.Left < m_Left ? rectangle.Left : m_Left,
                rectangle.Bottom > m_Bottom ? rectangle.Bottom : m_Bottom,
                rectangle.Right > m_Right ? rectangle.Right : m_Right);

            //Console.WriteLine("Result: " + Result.ToString());
            //return Result;
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            // Return false if the value is null
            if (obj is RectangleD)
                return this.Equals((RectangleD)obj);
            return false;
        }

        /// <summary>Returns a unique code for this instance used in hash tables.</summary>
        public override int GetHashCode()
        {
            return m_Left.GetHashCode() ^ m_Right.GetHashCode() ^ m_Top.GetHashCode() ^ m_Bottom.GetHashCode();
        }

        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        #endregion

        #region Static Methods

        public static RectangleD FromLTRB(double left, double top, double right, double bottom)
        {
            return new RectangleD(new PointD(left, top), new PointD(right, bottom));
        }

        /// <summary>
        /// Parses a string into a RectangleD object.
        /// </summary>
        /// <param name="value">A <string>String</string> specifying geographic coordinates defining a rectangle.</param>
        /// <returns>A <strong>RectangleD</strong> object using the specified coordinates.</returns>
        /// <remarks>This powerful method will convert points defining a rectangle in the form of a string into
        /// a RectangleD object.  The string can be </remarks>
        public static RectangleD Parse(string value)
        {
            return new RectangleD(value);
        }

        public static RectangleD Parse(string value, CultureInfo culture)
        {
            return new RectangleD(value, culture);
        }

        /// <summary>
        /// Returns the smallest possible RectangleD containing both specified RectangleDs.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static RectangleD UnionWith(RectangleD first, RectangleD second)
        {
            return first.UnionWith(second);
        }

        /// <summary>
        /// Returns a rectangle which encloses the specified points.
        /// </summary>
        /// <param name="points">An array of PointD objects to enclose.</param>
        /// <returns>A <strong>RectangleD</strong> object enclosing the specified points.</returns>
        /// <remarks>This method is typically used to calculate a rectangle surrounding
        /// points which have been rotated.  For example, if a rectangle is rotated by 45°, the
        /// total width it occupies is greater than it's own width.</remarks>
        public static RectangleD FromArray(PointD[] points)
        {
            // Start with the first point
            double top = points[0].Y;
            double left = points[0].X;
            double bottom = points[0].Y;
            double right = points[0].X;

            // Expand the points outward as limits are breached
            for (int index = 1; index < points.Length; index++)
            {
                PointD point = points[index];
                if (point.X < left)
                    left = point.X;
                else if (point.X > right)
                    right = point.X;
                if (point.Y < top)
                    top = point.Y;
                else if (point.Y > bottom)
                    bottom = point.Y;
            }

            // Build a new rectangle
            return new RectangleD(left, top, right, bottom);
        }

        /// <summary>
        /// Returns the RectangleD formed by the intersection of the two specified RectangleDs.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static RectangleD IntersectionOf(RectangleD first, RectangleD second)
        {
            return first.IntersectionOf(second);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Increases the size of the rectangle by the specified amount.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static RectangleD operator +(RectangleD left, SizeD size)
        {
            return new RectangleD(left.UpperLeft, left.Size.Add(size));
        }

        public static RectangleD operator +(RectangleD left, PointD location)
        {
            return new RectangleD(left.UpperLeft.Add(location), left.Size);
        }

        public static RectangleD operator -(RectangleD left, SizeD size)
        {
            return new RectangleD(left.UpperLeft, left.Size.Subtract(size));
        }

        public static RectangleD operator -(RectangleD left, PointD location)
        {
            return new RectangleD(left.UpperLeft.Subtract(location), left.Size);
        }

        public static RectangleD operator *(RectangleD left, SizeD size)
        {
            return new RectangleD(left.UpperLeft, left.Size.Multiply(size));
        }

        public static RectangleD operator *(RectangleD left, PointD location)
        {
            return new RectangleD(left.UpperLeft.Multiply(location), left.Size);
        }

        public static RectangleD operator /(RectangleD left, SizeD size)
        {
            return new RectangleD(left.UpperLeft, left.Size.Divide(size));
        }

        public static RectangleD operator /(RectangleD left, PointD location)
        {
            return new RectangleD(left.UpperLeft.Divide(location), left.Size);
        }

        public static bool operator ==(RectangleD left, RectangleD right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RectangleD left, RectangleD right)
        {
            return !left.Equals(right);
        }

        public RectangleD Add(SizeD size)
        {
            return new RectangleD(UpperLeft, Size.Add(size));
        }

        public RectangleD Subtract(SizeD size)
        {
            return new RectangleD(UpperLeft, Size.Subtract(size));
        }

        public RectangleD Multiply(SizeD size)
        {
            return new RectangleD(UpperLeft, Size.Multiply(size));
        }

        public RectangleD Divide(SizeD size)
        {
            return new RectangleD(UpperLeft, Size.Divide(size));
        }

        public RectangleD Add(PointD position)
        {
            return new RectangleD(UpperLeft.Add(position), Size);
        }

        public RectangleD Subtract(PointD position)
        {
            return new RectangleD(UpperLeft.Subtract(position), Size);
        }

        public RectangleD Multiply(PointD position)
        {
            return new RectangleD(UpperLeft.Multiply(position), Size);
        }

        public RectangleD Divide(PointD position)
        {
            return new RectangleD(UpperLeft.Divide(position), Size);
        }

        #endregion

        #region Conversions

        public static explicit operator RectangleD(Rectangle value) 
		{
			return new RectangleD((double)value.Top, (double)value.Left, (double)value.Bottom, (double)value.Right);
		}

		public static explicit operator RectangleD(RectangleF value) 
		{
			return new RectangleD((double)value.Top, (double)value.Left, (double)value.Bottom, (double)value.Right);
		}

		public static explicit operator Rectangle(RectangleD value) 
		{
			return new Rectangle((int)value.Top, (int)value.Left, (int)(value.Bottom - value.Top), (int)(value.Right - value.Left));
		}

		public static explicit operator RectangleF(RectangleD value) 
		{
			return new RectangleF((float)value.Left, (float)value.Top, (float)(value.Width), (float)(value.Height));
        }

        #endregion

        #region IEquatable<RectangleD> Members

        public bool Equals(RectangleD other)
        {
            // The objects are equivalent if their bounds are equivalent
            return m_Left == other.Left
                && m_Right == other.Right
                && m_Top == other.Top
                && m_Bottom == other.Bottom;
        }

        #endregion

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            CultureInfo culture = (CultureInfo)formatProvider;
            return m_Left.ToString(format, formatProvider)
                + culture.TextInfo.ListSeparator
                + m_Top.ToString(format, formatProvider)
                + culture.TextInfo.ListSeparator
                + Width.ToString(format, formatProvider)
                + culture.TextInfo.ListSeparator
                + Height.ToString(format, formatProvider);
        }

		#endregion

        #region IXmlSerializable Members

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Top",
                        m_Top.ToString("G17", CultureInfo.InvariantCulture));
            writer.WriteAttributeString("Bottom",
                        m_Bottom.ToString("G17", CultureInfo.InvariantCulture));
            writer.WriteAttributeString("Left",
                        m_Left.ToString("G17", CultureInfo.InvariantCulture));
            writer.WriteAttributeString("Right",
                        m_Right.ToString("G17", CultureInfo.InvariantCulture));
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            throw new InvalidOperationException("Use the RectangleD(XmlReader) constructor to create a new instance instead of calling ReadXml.");
        }

        #endregion


        #region Unused Code (Commented Out)

        //		/// <summary>
        //		/// Changes the size and shape of the RectangleD to match the aspect ratio of the specified RectangleD.
        //		/// </summary>
        //		/// <param name="aspectRatio"></param>
        //		/// <returns></returns>
        //		/// <remarks>This method will expand a RectangleD outward, from its center point, until
        //		/// the ratio of its width to its height matches the specified value.</remarks>
        //		public RectangleD ToAspectRatio(System.Drawing.Rectangle rectangle)
        //		{
        //			// Calculate the aspect ratio
        //			return ToAspectRatio((double)RectangleD.Width / (double)RectangleD.Height);
        //		}
        //
        //		/// <summary>
        //		/// Changes the size and shape of the RectangleD to match the aspect ratio of the specified RectangleD.
        //		/// </summary>
        //		/// <param name="aspectRatio"></param>
        //		/// <returns></returns>
        //		/// <remarks>This method will expand a RectangleD outward, from its center point, until
        //		/// the ratio of its width to its height matches the specified value.</remarks>
        //		public RectangleD ToAspectRatio(System.Drawing.RectangleF RectangleD)
        //		{
        //			// Calculate the aspect ratio
        //			return ToAspectRatio((double)RectangleD.Width / (double)RectangleD.Height);
        //		}

        //		/// <summary>
        //		/// Moves the entire RectangleD in the specified direction by the specified distance.
        //		/// </summary>
        //		/// <param name="direction"></param>
        //		/// <param name="distance"></param>
        //		/// <returns></returns>
        //		////[CLSCompliant(false)]
        //		public RectangleD TranslateTo(Angle direction, double distance)
        //		{
        //			// Find the new translation point
        //			return new RectangleD(UpperLeft.TranslateTo(direction, distance), Size);
        //		}

        #endregion
    }
}
