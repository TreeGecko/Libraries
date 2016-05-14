using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TreeGecko.Library.Geospatial.Geoframeworks.Objects
{
    /// <summary>Represents a highly-precise two-dimensional size.</summary>
    /// <remarks>
    /// 	<para>This structure is a <em>GeoFrameworks</em> "parseable type" whose value can
    ///     be freely converted to and from <strong>String</strong> objects via the
    ///     <strong>ToString</strong> and <strong>Parse</strong> methods.</para>
    /// 	<para>Instances of this structure are guaranteed to be thread-safe because it is
    ///     immutable (its properties can only be modified via constructors).</para>
    /// </remarks>
    public struct SizeD : IFormattable, IEquatable<SizeD>, IXmlSerializable
    {
        private double m_Width;
        private double m_Height;

        #region Fields

        /// <summary>Represents a size with no value.</summary>
        public static readonly SizeD Empty = new SizeD(0.0, 0.0);

        /// <summary>Represents an infinite size.</summary>
        public static readonly SizeD Infinity = new SizeD(Double.PositiveInfinity, Double.PositiveInfinity);

        /// <summary>Represents the smallest possible size.</summary>
        public static readonly SizeD Minimum = new SizeD(Double.MinValue, Double.MinValue);

        /// <summary>Represents the largest possible size.</summary>
        public static readonly SizeD Maximum = new SizeD(Double.MaxValue, Double.MaxValue);

        #endregion

        #region Constructors

        public SizeD(PointD _pt)
        {
            m_Width = _pt.X;
            m_Height = _pt.Y;
        }

        public SizeD(SizeD _size)
        {
            m_Width = _size.Width;
            m_Height = _size.Height;
        }

        /// <summary>Creates a new instance.</summary>
        public SizeD(double _width, double _height)
        {
            m_Width = _width;
            m_Height = _height;
        }

        public SizeD(string _value)
            : this(_value, CultureInfo.CurrentCulture)
        {
        }

        public SizeD(string _value, CultureInfo _culture)
        {
            // Split out the values
            string[] values = _value.Trim().Split(_culture.TextInfo.ListSeparator.ToCharArray());

            // There should be two values
            if (values.Length != 2)
                throw new FormatException(Properties.Resources.SizeD_InvalidFormat);

            // PArse it out
            m_Width = double.Parse(values[0].Trim(), _culture);
            m_Height = double.Parse(values[1].Trim(), _culture);
        }

        public SizeD(XmlReader _reader)
        {
            // Initialize all fields
            m_Width = Double.NaN;
            m_Height = Double.NaN;

            // Deserialize the object from XML
            ReadXml(_reader);
        }

        #endregion

        #region Public Properties

        /// <summary>Returns the horizontal size.</summary>
        public double Width
        {
            get { return m_Width; }
        }

        /// <summary>Returns the vertical size.</summary>
        public double Height
        {
            get { return m_Height; }
        }

        /// <summary>Returns the ratio width to height.</summary>
        public double AspectRatio
        {
            get { return m_Width/m_Height; }
        }

        /// <summary>Indicates if the instance has any value.</summary>
        public bool IsEmpty
        {
            get { return (m_Width.Equals(0) && m_Height.Equals(0)); }
        }

        #endregion

        #region Public Methods

        public SizeD ToAspectRatio(SizeD _size)
        {
            // Calculate the aspect ratio
            return ToAspectRatio(_size.Width/_size.Height);
        }

        public SizeD ToAspectRatio(double _aspectRatio)
        {
            double currentAspect = AspectRatio;

            // Do the values already match?
            if (currentAspect.Equals(_aspectRatio))
                return this;

            // Is the new ratio higher or lower?
            if (_aspectRatio > currentAspect)
            {
                // Inflate the GeographicRectangle to the new height minus the current height
                // TESTS OK
                return new SizeD(m_Width +
                                 (_aspectRatio*Height - Width), m_Height);
            }

            // Inflate the GeographicRectangle to the new height minus the current height
            return new SizeD(m_Width,
                m_Height + (Width/_aspectRatio - Height));
        }

        /// <summary>Returns a copy of the current instance.</summary>
        public SizeD Clone()
        {
            return new SizeD(m_Width, m_Height);
        }

        public string ToString(string _format)
        {
            return ToString(_format, CultureInfo.CurrentCulture);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Compares the current instance to the specified object.
        /// </summary>
        /// <param name="_obj">An <strong>Object</strong> to compare with.</param>
        /// <returns>A <strong>Boolean</strong>, True if the values are equivalent.</returns>
        public override bool Equals(object _obj)
        {
            // Only compare similar objects
            if (_obj is SizeD)
                return Equals((SizeD) _obj);
            return false;
        }

        public override int GetHashCode()
        {
            return Convert.ToInt32(m_Width) ^ Convert.ToInt32(m_Height);
        }

        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        #endregion

        #region Static Methods

        public static SizeD Parse(string _value)
        {
            return new SizeD(_value, CultureInfo.CurrentCulture);
        }

        public static SizeD Parse(string _value, CultureInfo _culture)
        {
            return new SizeD(_value, _culture);
        }

        #endregion

        #region Operators

        public static bool operator ==(SizeD _left, SizeD _right)
        {
            return _left.Equals(_right);
        }

        public static bool operator !=(SizeD _left, SizeD _right)
        {
            return !(_left.Equals(_right));
        }

        public static SizeD operator +(SizeD _left, SizeD _right)
        {
            return _left.Add(_right);
        }

        public static SizeD operator -(SizeD _left, SizeD _right)
        {
            return _left.Subtract(_right);
        }

        public static SizeD operator *(SizeD _left, SizeD _right)
        {
            return _left.Multiply(_right);
        }

        public static SizeD operator /(SizeD _left, SizeD _right)
        {
            return _left.Divide(_right);
        }

        /// <summary>Returns the sum of the current instance with the specified size.</summary>
        public SizeD Add(SizeD _size)
        {
            return new SizeD(m_Width + _size.Width, m_Height + _size.Height);
        }

        /// <summary>Returns the current instance decreased by the specified value.</summary>
        public SizeD Subtract(SizeD _size)
        {
            return new SizeD(m_Width - _size.Width, m_Height - _size.Height);
        }

        /// <summary>Returns the product of the current instance with the specified value.</summary>
        public SizeD Multiply(SizeD _size)
        {
            return new SizeD(m_Width*_size.Width, m_Height*_size.Height);
        }

        /// <summary>Returns the current instance divided by the specified value.</summary>
        public SizeD Divide(SizeD _size)
        {
            return new SizeD(m_Width/_size.Width, m_Height/_size.Height);
        }

        #endregion

        #region IEquatable<SizeD> Members

        /// <summary>
        /// Compares the current instance to the specified object.
        /// </summary>
        /// <param name="_other"></param>
        /// <returns>A <strong>Boolean</strong>, True if the values are equivalent.</returns>
        public bool Equals(SizeD _other)
        {
            return m_Width.Equals(_other.Width) && m_Height.Equals(_other.Height);
        }

        #endregion

        #region IFormattable Members

        public string ToString(string _format, IFormatProvider _formatProvider)
        {
            CultureInfo culture = (CultureInfo) _formatProvider;

            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            if (_format == null || _format.Length == 0)
                _format = "G";

            return Width.ToString(_format, culture)
                   + culture.TextInfo.ListSeparator + " "
                   + Height.ToString(_format, culture);
        }

        #endregion

        #region IXmlSerializable Members

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        public void WriteXml(XmlWriter _writer)
        {
            _writer.WriteAttributeString("Width",
                m_Width.ToString("G17", CultureInfo.InvariantCulture));
            _writer.WriteAttributeString("Height",
                m_Height.ToString("G17", CultureInfo.InvariantCulture));
        }

        public void ReadXml(XmlReader _reader)
        {
            string sWidth = _reader.GetAttribute("Width");
            string sHeight = _reader.GetAttribute("Height");

            if (sWidth != null)
                m_Width = double.Parse(sWidth, CultureInfo.InvariantCulture);

            if (sHeight != null)
                m_Height = double.Parse(sHeight, CultureInfo.InvariantCulture);
        }

        #endregion
    }
}