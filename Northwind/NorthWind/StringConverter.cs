using System;
using System.Globalization;

namespace NorthWindNS
{
    public static class StringConverter
    {
        public static DateTime? ToNullableDateTime(string inputString)
        {
            DateTime dateTime;
            if (DateTime.TryParseExact(inputString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out dateTime))
            {
                return dateTime;
            }
            return null;
        }

        public static int? ToNullableInt32(string inputString)
        {
            int result;
            if (Int32.TryParse(inputString, out result))
            {
                return result;
            }
            return null;
        }

        public static short? ToNullableInt16(string inputString)
        {
            short result;
            if (Int16.TryParse(inputString, out result))
            {
                return result;
            }
            return null;
        }

        public static decimal? ToNullableDecimal(string inputString)
        {
            decimal result;
            if (Decimal.TryParse(inputString, out result))
            {
                return result;
            }
            return null;
        }

        public static float? ToNullableFloat(string inputString)
        {
            float result;
            if (float.TryParse(inputString, out result))
            {
                return result;
            }
            return null;
        }
    }
}