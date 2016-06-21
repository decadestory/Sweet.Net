using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweet.Net.Ext
{
    public static class DataTimeExt
    {
        /// <summary>
        /// 格式化TimeSpan: xx天xx时xx分xx秒xx毫秒
        /// </summary>
        /// <param name="milliSeconds">毫秒数</param>
        /// <returns></returns>
        public static string FormatTimeSpan(this long milliSeconds)
        {
            var span = TimeSpan.FromMilliseconds(milliSeconds);
            var day = span.Days == 0 ? string.Empty : span.Days + "天";
            var hour = span.Hours == 0 ? string.Empty : span.Hours + "时";
            var min = span.Minutes == 0 ? string.Empty : span.Minutes + "分";
            var sec = span.Seconds == 0 ? string.Empty : span.Seconds + "秒";
            var ms = span.Milliseconds == 0 ? string.Empty : span.Milliseconds + "毫秒";
            return day + hour + min + sec + ms;
        }
    }
}
