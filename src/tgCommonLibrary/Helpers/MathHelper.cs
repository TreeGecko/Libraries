using System;
using System.Collections.Generic;

namespace TreeGecko.Library.Common.Helpers
{

    public class MathHelper
    {
        public static double ToRadians(double _degrees)
        {
            //180 degress per pi
            return (_degrees * Math.PI) / 180;
        }

        public static double ToDegrees(double _radians)
        {
            //180 degress per pi
            return (_radians * 180) / Math.PI;
        }
        public static Decimal ToRadians(Decimal _degrees)
        {
            //180 degress per pi
            return (_degrees * Convert.ToDecimal(Math.PI)) / 180;
        }

        public static Decimal ToDegrees(Decimal _radians)
        {
            //180 degress per pi
            return (_radians * 180) / Convert.ToDecimal(Math.PI);
        }

        public static double GetSum(List<double> _values)
        {
            double sum = 0.0;

            foreach (double value in _values)
            {
                sum += value;
            }

            return sum;
        }

        public static double GetMean(List<double> _values)
        {
            double sum = GetSum(_values);

            if (_values.Count > 0)
            {
                return sum / _values.Count;
            }
            return 0;
        }

        public static List<double> GetSquares(List<double> _values)
        {
            List<double> results = new List<double>();

            foreach (double d in _values)
            {
                results.Add(d * d);
            }

            return results;
        }

        public static double GetStandardDeviation(List<double> _values)
        {
            return Math.Sqrt(GetVariance(_values));           
        }

        public static List<double> FilterValues(List<double> _values, 
            double _mean, double _allowedDeviation)
        {
            List<double> result = new List<double>();
            double min = _mean - _allowedDeviation;
            double max = _mean + _allowedDeviation;

            foreach (double d in _values)
            {
                if (d >= min && d <= max)
                {
                    result.Add(d);
                }
            }

            return result;
        }

        public static double GetVariance(List<double> _values)
        {
            double n = _values.Count;

            double v1 = n * GetSum(GetSquares(_values));
            double v2 = Math.Pow(GetSum(_values), 2);
            return (1 / (n * (n - 1))) * (v1 - v2);            
        }

        public static double GetMin(List<double> _values)
        {
            double min = Double.MaxValue;

            foreach (double d in _values)
            {
                if (d < min)
                {
                    min = d;
                }
            }

            return min;
        }

        public static double GetMax(List<double> _values)
        {
            double max = Double.MinValue;

            foreach (double d in _values)
            {
                if (d > max)
                {
                    max = d;
                }
            }

            return max;
        }


    }
}
