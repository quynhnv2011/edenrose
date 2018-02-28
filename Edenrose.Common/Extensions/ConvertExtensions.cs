using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Edenrose.Common
{
    public static class ConvertExtensions
    {
        // transform object into Identity data type (integer).

        public static int AsId(this object item, int defaultId = -1)
        {
            if (item == null)
                return defaultId;

            int result;
            if (!int.TryParse(item.ToString(), out result))
                return defaultId;

            return result;
        }

        // transform object into integer data type.

        public static int AsInt(this object item, int defaultInt = default(int))
        {
            if (item == null)
                return defaultInt;

            int result;
            if (!int.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        // transform object into double data type

        public static double AsDouble(this object item, double defaultDouble = default(double))
        {
            if (item == null)
                return defaultDouble;

            double result;
            if (!double.TryParse(item.ToString(), out result))
                return defaultDouble;

            return result;
        }
        public static decimal AsDecimail(this object item, decimal defaultDecimal = default(decimal))
        {
            if (item == null)
                return defaultDecimal;

            decimal result;
            if (!decimal.TryParse(item.ToString(), out result))
                return defaultDecimal;

            return result;
        }
        public static byte AsByte(this object item, byte defaultByte = default(byte))
        {
            if (item == null)
                return defaultByte;

            byte result;
            if (!byte.TryParse(item.ToString(), out result))
                return defaultByte;

            return result;
        }
        // transform object into string data type

        public static string AsString(this object item, string defaultString = default(string))
        {
            if (item == null || item.Equals(System.DBNull.Value))
                return defaultString;

            return item.ToString().Trim();
        }

        public static string AsTrim(this string item)
        {
            if (item == null || item.Equals(System.DBNull.Value) || item == "0")
                return null;

            return item.Trim();
        }

        // transform object into DateTime data type.

        public static DateTime AsDateTime(this object item, DateTime defaultDateTime = default(DateTime))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultDateTime;

            DateTime result;
            if (!DateTime.TryParse(item.ToString(), out result))
                return defaultDateTime;

            return result;
        }
        public static DateTime AsDateTime(this object item, string format,string culture, DateTime defaultDateTime = default(DateTime))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultDateTime;

            DateTime result;
            if (!DateTime.TryParseExact(item.ToString(), format, new CultureInfo(culture),
                           DateTimeStyles.None, out result))
                return defaultDateTime;

            return result;
        }
        // transform object into bool data type

        public static bool AsBool(this object item, bool defaultBool = default(bool))
        {
            if (item == null)
                return defaultBool;

            return new List<string>() { "yes", "y", "true","1"}.Contains(item.ToString().ToLower());
        }

        // transform string into byte array

        public static byte[] AsByteArray(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            return Convert.FromBase64String(s);
        }

        // transform object into base64 string.

        public static string AsBase64String(this object item)
        {
            if (item == null)
                return null;
            ;

            return Convert.ToBase64String((byte[])item);
        }

        // transform object into Guid data type

        public static Guid AsGuid(this object item)
        {
            try { return new Guid(item.ToString()); }
            catch { return Guid.Empty; }
        }

        // concatenates SQL and ORDER BY clauses into a single string

        public static string OrderBy(this string sql, string sortExpression)
        {
            if (string.IsNullOrEmpty(sortExpression))
                return sql;

            return sql + " ORDER BY " + sortExpression;
        }


        // takes an enumerable source and returns a comma separate string.
        // handy for building SQL Statements (for example with IN () statements) from object collections

        public static string CommaSeparate<T, U>(this IEnumerable<T> source, Func<T, U> func)
        {
            return string.Join(",", source.Select(s => func(s).ToString()).ToArray());
        }

        public static string ConvertTenTrangThaiTb(this int trangThai)
        {
            string tenTrangThai = "";
            switch (trangThai)
            {
                case 1:
                    tenTrangThai = "Trong kho";
                    break;
                case 2:
                    tenTrangThai= "Đang sử dụng";
                     break;
                case 3:
                    tenTrangThai = "Bảo hành";
                    break;
                case 4:
                    tenTrangThai = "Sửa ngoài";
                    break;
                case 5:
                    tenTrangThai = "Thanh lý - thanh hủy";
                    break;
                case 6:
                    tenTrangThai = "Không xác định";
                    break;                
                default:
                    tenTrangThai = "Không xác định";
                    break;
            }
            return tenTrangThai;
        }

        public static double ConvertToPhanTram(this int so, int total)
        {
            try
            {
                if (total == 0 || so == 0) return 0f;
                var s = (double)so/total;
                double result = Math.Round((double)so / total * 100, 2);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public static T CopyObject<T>(this object objSource)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, objSource);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

    }
}
