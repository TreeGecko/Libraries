using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TreeGecko.Library.Geospatial.Geoframeworks.Objects
{
    /// <summary>
    /// Represents an Earth-centered, Earth-fixed (ECEF) Cartesian coordinate.
    /// </summary>
    public struct CartesianPoint : IFormattable, IEquatable<CartesianPoint>, IXmlSerializable
    {
        private Distance m_X;
        private Distance m_Y;
        private Distance m_Z;

        #region Constructors

        /// <summary>
        /// Creates a new instance using the specified X, Y and Z values.
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_z"></param>
        public CartesianPoint(Distance _x, Distance _y, Distance _z)
        {
            m_X = _x.ToMeters();
            m_Y = _y.ToMeters();
            m_Z = _z.ToMeters();
        }

        /// <summary>
        /// Creates a new instance from the specified block of GML.
        /// </summary>
        /// <param name="_reader"></param>
        public CartesianPoint(XmlReader _reader)
        {
            // Initialize all fields
            m_X = Distance.Invalid;
            m_Y = Distance.Invalid;
            m_Z = Distance.Invalid;

            // Deserialize the object from XML
            ReadXml(_reader);
        }

        #endregion

        #region Fields

        /// <summary>
        /// Returns a cartesian coordinate with empty values.
        /// </summary>
        public static readonly CartesianPoint Empty = new CartesianPoint(Distance.Empty, Distance.Empty, Distance.Empty);
        /// <summary>
        /// Returns a cartesian point with infinite values.
        /// </summary>
        public static readonly CartesianPoint Infinity = new CartesianPoint(Distance.Infinity, Distance.Infinity, Distance.Infinity);
        /// <summary>
        /// Represents an invalid or unspecified value.
        /// </summary>
        public static readonly CartesianPoint Invalid = new CartesianPoint(Distance.Invalid, Distance.Invalid, Distance.Invalid);

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the horizontal (longitude) portion of a Cartesian coordinate.
        /// </summary>
        public Distance X
        {
            get
            {
                return m_X;
            }
        }

        /// <summary>
        /// Returns the vertical (latitude) portion of a Cartesian coordinate.
        /// </summary>
        public Distance Y
        {
            get
            {
                return m_Y;
            }
        }

        /// <summary>
        /// Returns the altitude portion of a Cartesian coordinate.
        /// </summary>
        public Distance Z
        {
            get
            {
                return m_Z;
            }
        }

        /// <summary>
        /// Indicates whether the current instance has no value.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return m_X.IsEmpty && m_Y.IsEmpty && m_Z.IsEmpty;
            }
        }

        /// <summary>
        /// Indicates whether the current instance is invalid or unspecified.
        /// </summary>
        public bool IsInvalid
        {
            get
            {
                return m_X.IsInvalid && m_Y.IsInvalid && m_Z.IsInvalid;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the current instance to a geodetic (latitude/longitude) coordinate.
        /// </summary>
        /// <returns>A <strong>Position</strong> object containing the converted result.</returns>
        /// <remarks>The conversion formula will convert the Cartesian coordinate to
        /// latitude and longitude using the WGS1984 ellipsoid (the default ellipsoid for
        /// GPS coordinates).</remarks>
        public Position3D ToPosition3D()
        {
            return ToPosition3D(Ellipsoid.Wgs1984);
        }

        /// <summary>
        /// Converts the current instance to a geodetic (latitude/longitude) coordinate using the specified ellipsoid.
        /// </summary>
        /// <returns>A <strong>Position</strong> object containing the converted result.</returns>
        /// <remarks>The conversion formula will convert the Cartesian coordinate to
        /// latitude and longitude using the WGS1984 ellipsoid (the default ellipsoid for
        /// GPS coordinates).  The resulting three-dimensional coordinate is accurate to within two millimeters
        /// (2 mm).</remarks>
        public Position3D ToPosition3D(Ellipsoid _ellipsoid)
        {
            if (_ellipsoid == null)
                throw new ArgumentNullException("_ellipsoid");

            #region New code

            /*
             * % ECEF2LLA - convert earth-centered earth-fixed (ECEF)
%            cartesian coordinates to latitude, longitude,
%            and altitude
%
% USAGE:
% [lat,lon,alt] = ecef2lla(x,y,z)
%
% lat = geodetic latitude (radians)
% lon = longitude (radians)
% alt = height above WGS84 ellipsoid (m)
% x = ECEF X-coordinate (m)
% y = ECEF Y-coordinate (m)
% z = ECEF Z-coordinate (m)
%
% Notes: (1) This function assumes the WGS84 model.
%        (2) Latitude is customary geodetic (not geocentric).
%        (3) Inputs may be scalars, vectors, or matrices of the same
%            size and shape. Outputs will have that same size and shape.
%        (4) Tested but no warranty; use at your own risk.
%        (5) Michael Kleder, April 2006

function [lat,lon,alt] = ecef2lla(x,y,z)

% WGS84 ellipsoid constants:
a = 6378137;
e = 8.1819190842622e-2;

% calculations:
b   = sqrt(a^2*(1-e^2));
ep  = sqrt((a^2-b^2)/b^2);
p   = sqrt(x.^2+y.^2);
th  = atan2(a*z,b*p);
lon = atan2(y,x);
lat = atan2((z+ep^2.*b.*sin(th).^3),(p-e^2.*a.*cos(th).^3));
N   = a./sqrt(1-e^2.*sin(lat).^2);
alt = p./cos(lat)-N;

% return lon in range [0,2*pi)
lon = mod(lon,2*pi);

% correct for numerical instability in altitude near exact poles:
% (after this correction, error is about 2 millimeters, which is about
% the same as the numerical precision of the overall function)

k=abs(x)<1 & abs(y)<1;
alt(k) = abs(z(k))-b;

return
             */

            double x = m_X.ToMeters().Value;
            double y = m_Y.ToMeters().Value;
            double z = m_Z.ToMeters().Value;

            //% WGS84 ellipsoid constants:
            //a = 6378137;

            double a = _ellipsoid.EquatorialRadius.ToMeters().Value;

            //e = 8.1819190842622e-2;

            double e = _ellipsoid.Eccentricity;

            //% calculations:
            //b   = sqrt(a^2*(1-e^2));

            double b = Math.Sqrt(Math.Pow(a, 2) * (1 - Math.Pow(e, 2)));

            //ep  = sqrt((a^2-b^2)/b^2);

            double ep = Math.Sqrt((Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(b, 2));

            //p   = sqrt(x.^2+y.^2);

            double p = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

            //th  = atan2(a*z,b*p);

            double th = Math.Atan2(a * z, b * p);

            //lon = atan2(y,x);

            double lon = Math.Atan2(y, x);

            //lat = atan2((z+ep^2.*b.*sin(th).^3),(p-e^2.*a.*cos(th).^3));

            double lat = Math.Atan2((z + Math.Pow(ep, 2) * b * Math.Pow(Math.Sin(th), 3)), (p - Math.Pow(e, 2) * a * Math.Pow(Math.Cos(th), 3)));

            //N   = a./sqrt(1-e^2.*sin(lat).^2);

            double N = a / Math.Sqrt(1 - Math.Pow(e, 2) * Math.Pow(Math.Sin(lat), 2));

            //alt = p./cos(lat)-N;

            double alt = p / Math.Cos(lat) - N;

            //% return lon in range [0,2*pi)
            //lon = mod(lon,2*pi);

            lon = lon % (2 * Math.PI);

            //% correct for numerical instability in altitude near exact poles:
            //% (after this correction, error is about 2 millimeters, which is about
            //% the same as the numerical precision of the overall function)

            //k=abs(x)<1 & abs(y)<1;

            bool k = Math.Abs(x) < 1.0 && Math.Abs(y) < 1.0;

            //alt(k) = abs(z(k))-b;

            if (k)
                alt = Math.Abs(z) - b;

            //return

            return new Position3D(
                    Distance.FromMeters(alt),
                    Latitude.FromRadians(lat), 
                    Longitude.FromRadians(lon));

            #endregion
        }

        /// <summary>
        /// Returns the distance from the current instance to the specified cartesian point.
        /// </summary>
        /// <param name="_point">A <strong>CartesianPoint</strong> object representing the end of a segment.</param>
        /// <returns></returns>
        public Distance DistanceTo(CartesianPoint _point)
        {
            return new Distance(
                Math.Sqrt(Math.Pow(_point.X.Value - m_X.Value, 2) 
                        + Math.Pow(_point.Y.Value - m_Y.Value, 2))
                , DistanceUnit.Meters).ToLocalUnitType();
        }

        public string ToString(string _format)
        {
            return ToString(_format, CultureInfo.CurrentCulture);
        }

        #endregion

        #region Overrides

        public override bool Equals(object _obj)
        {
            if (_obj is CartesianPoint)
                return Equals((CartesianPoint)_obj);
            return false;
        }

        public override int GetHashCode()
        {
            return m_X.GetHashCode() ^ m_Y.GetHashCode() ^ m_Z.GetHashCode();
        }

        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        #endregion

        #region IFormattable Members

        public string ToString(string _format, IFormatProvider _formatProvider)
        {
            CultureInfo culture = (CultureInfo)_formatProvider;

            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            if (string.IsNullOrEmpty(_format))
                _format = "G";

            return m_X.ToString(_format, culture) + culture.TextInfo.ListSeparator
                + m_Y.ToString(_format, culture) + culture.TextInfo.ListSeparator
                + m_Z.ToString(_format, culture);
        }

        #endregion

        #region IEquatable<CartesianPoint> Members

        public bool Equals(CartesianPoint _other)
        {
            return m_X.Equals(_other.X)
                && m_Y.Equals(_other.Y)
                && m_Z.Equals(_other.Z);
        }

        #endregion

        #region IXmlSerializable Members

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        public void WriteXml(XmlWriter _writer)
        {
            /* The position class uses the GML 3.0 specification for XML.
             * 
             * <gml:pos>X Y Z</gml:pos>
             *
             */
            _writer.WriteStartElement(Xml.GmlXmlPrefix, "pos", Xml.GmlXmlNamespace);
            _writer.WriteString(m_X.ToMeters().Value.ToString("G17", CultureInfo.InvariantCulture));
            _writer.WriteString(" ");
            _writer.WriteString(m_Y.ToMeters().Value.ToString("G17", CultureInfo.InvariantCulture));
            _writer.WriteString(" ");
            _writer.WriteString(m_Z.ToMeters().Value.ToString("G17", CultureInfo.InvariantCulture));
            _writer.WriteEndElement();
        }

        public void ReadXml(XmlReader _reader)
        {
            /* The position class uses the GML 3.0 specification for XML.
             * 
             * <gml:pos>X Y Z</gml:pos>
             *
             * ... but it is also helpful to be able to READ older versions
             * of GML, such as this one for GML 2.0:
             * 
             * <gml:coord>
             *      <gml:X>double</gml:X>
             *      <gml:Y>double</gml:Y>  // optional
             *      <gml:Z>double</gml:Z>  // optional
             * </gml:coord>
             * 
             */

            // .NET whines if we don't fully assign all values
            m_X = Distance.Empty;
            m_Y = Distance.Empty;
            m_Z = Distance.Empty;
            
            // Move to the <gml:pos> or <gml:coord> element
            if (!_reader.IsStartElement("pos", Xml.GmlXmlNamespace) 
                && !_reader.IsStartElement("coord", Xml.GmlXmlNamespace))
                _reader.ReadStartElement();

            switch (_reader.LocalName.ToLower(CultureInfo.InvariantCulture))
            {
                case "pos":
                    // Read the "X Y" string, then split by the space between them
                    string[] values = _reader.ReadElementContentAsString().Split(' ');
                    // Deserialize the X
                    m_X = Distance.FromMeters(double.Parse(values[0], CultureInfo.InvariantCulture));

                    // Deserialize the Y
                    if (values.Length >= 2)
                        m_Y = Distance.FromMeters(double.Parse(values[1], CultureInfo.InvariantCulture));

                    // Deserialize the Z
                    if (values.Length == 3)
                        m_Z = Distance.FromMeters(double.Parse(values[2], CultureInfo.InvariantCulture));

                    break;
                case "coord":
                    // Read the <gml:coord> start tag
                    _reader.ReadStartElement();
                    // Now read up to 3 elements: X, and optionally Y or Z
                    for (int index = 0; index < 3; index++)
                    {
                        switch (_reader.LocalName.ToLower(CultureInfo.InvariantCulture))
                        {
                            case "x":
                                // Read X as meters (there's no unit type in the spec :P morons)
                                m_X = Distance.FromMeters(_reader.ReadElementContentAsDouble());
                                break;
                            case "y":
                                // Read Y as meters (there's no unit type in the spec :P morons)
                                m_Y = Distance.FromMeters(_reader.ReadElementContentAsDouble());
                                break;
                            case "z":
                                // Read Z as meters (there's no unit type in the spec :P morons)
                                m_Z = Distance.FromMeters(_reader.ReadElementContentAsDouble());
                                break;
                        }

                        // If we're at an end element, stop
                        if (_reader.NodeType == XmlNodeType.EndElement)
                            break;
                    }
                    // Read the </gml:coord> end tag
                    _reader.ReadEndElement();
                    break;
            }
        }

        #endregion

        #region Operators

        public static CartesianPoint operator +(CartesianPoint _a, CartesianPoint _b)
        {
            return new CartesianPoint(_a.X.Add(_b.X), _a.Y.Add(_b.Y), _a.Z.Add(_b.Z));
        }

        public static CartesianPoint operator -(CartesianPoint _a, CartesianPoint _b)
        {
            return new CartesianPoint(_a.X.Subtract(_b.X), _a.Y.Subtract(_b.Y), _a.Z.Subtract(_b.Z));
        }

        public static CartesianPoint operator *(CartesianPoint _a, CartesianPoint _b)
        {
            return new CartesianPoint(_a.X.Multiply(_b.X), _a.Y.Multiply(_b.Y), _a.Z.Multiply(_b.Z));
        }

        public static CartesianPoint operator /(CartesianPoint _a, CartesianPoint _b)
        {
            return new CartesianPoint(_a.X.Divide(_b.X), _a.Y.Divide(_b.Y), _a.Z.Divide(_b.Z));
        }

        public static explicit operator CartesianPoint(Position _value)
        {
            return _value.ToCartesianPoint();
        }

        public static explicit operator CartesianPoint(Position3D _value)
        {
            return _value.ToCartesianPoint();
        }

        #endregion
    }
}
