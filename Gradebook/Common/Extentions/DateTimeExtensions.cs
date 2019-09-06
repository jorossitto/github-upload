using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    public static class DateTimeExtentions
    {
        public static string ToXmlDateTime(this DateTime dateTime)
        {
            return dateTime.ToXmlDateTime(XmlDateTimeSerializationMode.Utc);
        }
        public static string ToXmlDateTime(this DateTime dateTime, XmlDateTimeSerializationMode mode)
        {
            return XmlConvert.ToString(dateTime, mode);
        }
    }
}
