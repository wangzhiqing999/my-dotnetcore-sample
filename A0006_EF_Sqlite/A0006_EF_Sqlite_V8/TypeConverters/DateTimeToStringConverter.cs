using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace A0006_EF_Sqlite_V8.TypeConverters
{

    /// <summary>
    /// DateTime 转换器.
    /// </summary>
    internal class DateTimeToStringConverter : ValueConverter<DateTime, string>
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static readonly string _timeFormat = "yyyy-MM-dd HH:mm:ss.SSS";


        public DateTimeToStringConverter(ConverterMappingHints? mappingHints = null)
            : base(
                v => v.ToString(_timeFormat),
                v => DateTime.ParseExact(v, _timeFormat, null),
                mappingHints)
        { }

    }
}
