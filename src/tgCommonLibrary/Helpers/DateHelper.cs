using System;
using System.Globalization;

namespace TreeGecko.Library.Common.Helpers
{
    public class DateHelper
    {
        public static string ToString(DateTime _dateTime)
        {
            return _dateTime.ToString("o");
        }

        /// <summary>
        /// Requires format of RoundTrip format
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static DateTime ParseDateTimeString(string _value)
        {
            if (_value == null)
            {
                throw new ArgumentNullException("_value", "Supplied date time string cannot be null");
            }

            DateTime outputValue = DateTime.Parse(_value, null, DateTimeStyles.RoundtripKind);
            return outputValue;
        }

        public static DateTime? ParseDate(string _value)
        {
            DateTime? output;
            if (_value != null)
            {
                DateTime tempDate = new DateTime();

                if (!DateTime.TryParse(_value, out tempDate))
                {
                    string temp = _value.ToLower();
                    string prefix = "";
                    int actionValue = 0;
                    string action = "none";

                    if (temp.Contains("+"))
                    {
                        action = "add";
                        prefix = temp.Substring(0, temp.IndexOf("+"));
                        string suffix = temp.Substring(temp.IndexOf("+") + 1);

                        if (!Int32.TryParse(suffix, out actionValue))
                        {
                            action = "none";
                            actionValue = 0;
                        }
                    }
                    else if (temp.Contains("-"))
                    {
                        action = "subtract";
                        prefix = temp.Substring(0, temp.IndexOf("-"));
                        string suffix = temp.Substring(temp.IndexOf("-") + 1);

                        if (!Int32.TryParse(suffix, out actionValue))
                        {
                            action = "none";
                            actionValue = 0;
                        }
                    }
                    else
                    {
                        prefix = temp;
                    }

                    switch (prefix)
                    {
                        case "now":
                            tempDate = DateTime.Now;
                            break;
                        case "today":
                            tempDate = DateTime.Now.Date;
                            break;
                        case "yesterday":
                            tempDate = DateTime.Now.Date.AddDays(-1);
                            break;
                        case "tomorrow":
                            tempDate = DateTime.Now.Date.AddDays(1);
                            break;
                    }

                    if (action != "none")
                    {
                        if (action == "add")
                        {
                            tempDate = tempDate.AddDays(actionValue);
                        }
                        else if (action == "subtract")
                        {
                            tempDate = tempDate.AddDays(-1*actionValue);
                        }
                    }
                    output = tempDate;
                }
                else
                {
                    output = tempDate;
                }

                return output;
            }

            return null;
        }

        public static string GetCurrentTimeStamp()
        {
            return DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fff");
        }
    }
}