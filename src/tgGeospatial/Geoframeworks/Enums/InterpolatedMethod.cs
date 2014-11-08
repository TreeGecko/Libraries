namespace TreeGecko.Library.Geospatial.Geoframeworks.Enums
{
    public enum InterpolationMethod
    {
        /// <summary>
        /// The transition occurs at a steady rate.
        /// </summary>
        Linear = 0,
        /// <summary>
        /// The transition is immediate; no interpolation takes place.
        /// </summary>
        Snap,
        /// <summary>
        /// The transition starts at zero and accelerates to the end using a quadratic formula.
        /// </summary>
        QuadraticEaseIn,
        /// <summary>
        /// The transition starts at high speed and decelerates to zero.
        /// </summary>
        QuadraticEaseOut,
        /// <summary>
        /// The transition accelerates to the halfway point, then decelerates to zero.
        /// </summary>
        QuadraticEaseInAndOut,
        CubicEaseIn,
        CubicEaseOut,
        CubicEaseInOut,
        QuarticEaseIn,
        ExponentialEaseIn,
        ExponentialEaseOut
    }
}
