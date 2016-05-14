using System;
using System.Globalization;

namespace TreeGecko.Library.Geospatial.Geoframeworks.Objects
{
    // TODO: Make this a structure once Position becomes a structure.

    /// <summary>
    /// Represents a line connected by two points on Earth's surface.
    /// </summary>
    public struct Segment : IFormattable
    {
        private readonly Position m_Start;
        private readonly Position m_End;

        #region Fields

        public static readonly Segment Empty = new Segment(Position.Empty, Position.Empty);

        #endregion

        #region Constructors

        /// <summary>Creates a new instance using the specified end points.</summary>
        public Segment(Position _start, Position _end)
        {
            m_Start = _start;
            m_End = _end;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the distance from the starting point to the end point.
        /// </summary>
        public Distance Distance
        {
            get { return m_Start.DistanceTo(m_End); }
        }

        /// <summary>
        /// Returns the bearing from the start to the end of the line.
        /// </summary>
        public Azimuth Bearing
        {
            get { return m_Start.BearingTo(m_End); }
        }

        /// <summary>
        /// Returns the starting point of the segment.
        /// </summary>
        public Position Start
        {
            get { return m_Start; }
        }

        /// <summary>
        /// Returns the end point of the segment.
        /// </summary>
        public Position End
        {
            get { return m_End; }
        }

        /// <summary>Returns the location halfway from the start to the end point.</summary>
        public Position Midpoint
        {
            get
            {
                return new Position(m_Start.Latitude.Add(m_End.Latitude.DecimalDegrees).Multiply(0.5),
                    m_Start.Longitude.Add(m_End.Longitude.DecimalDegrees).Multiply(0.5));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the distance from the segment to the specified position.
        /// </summary>
        /// <param name="_position"></param>
        /// <returns></returns>
        /// <remarks>This method analyzes the relative position of the segment to the line to determine the
        /// best mathematical approach.</remarks>
        public Distance DistanceTo(Position _position)
        {
            if (m_Start.Equals(m_End))
                return _position.DistanceTo(m_Start);
            Position delta = m_End.Subtract(m_Start);
            double ratio = ((_position.Longitude.DecimalDegrees - m_Start.Longitude.DecimalDegrees)
                            *delta.Longitude.DecimalDegrees +
                            (_position.Latitude.DecimalDegrees - m_Start.Latitude.DecimalDegrees)
                            *delta.Latitude.DecimalDegrees)/
                           (delta.Longitude.DecimalDegrees*delta.Longitude.DecimalDegrees +
                            delta.Latitude.DecimalDegrees
                            *delta.Latitude.DecimalDegrees);
            if (ratio < 0)
                return _position.DistanceTo(m_Start);

            if (ratio > 1)
                return _position.DistanceTo(m_End);

            Position destination = new Position(
                new Latitude((1 - ratio)*m_Start.Latitude.DecimalDegrees + ratio*m_End.Latitude.DecimalDegrees),
                new Longitude((1 - ratio)*m_Start.Longitude.DecimalDegrees + ratio*m_End.Longitude.DecimalDegrees));
            return _position.DistanceTo(destination);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        #endregion

        #region IFormattable Members

        public string ToString(string _format, IFormatProvider _formatProvider)
        {
            CultureInfo culture = (CultureInfo) _formatProvider;

            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            if (string.IsNullOrEmpty(_format))
                _format = "G";

            return m_Start.ToString(_format, _formatProvider)
                   + culture.TextInfo.ListSeparator + " "
                   + m_End.ToString(_format, _formatProvider);
        }

        #endregion
    }
}