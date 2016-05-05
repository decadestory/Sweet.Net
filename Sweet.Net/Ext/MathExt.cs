using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweet.Net.Ext
{
    public static class MathExt
    {
        /// <summary>
        /// 扩展方法：将整形转成有逗号分隔的货币类型(100,000,000)
        /// </summary>
        /// <param name="value">输入整型</param>
        /// <returns>返回字符</returns>
        public static string ToUsaMoney(this int value)
        {
            var ts = value.ToString(CultureInfo.InvariantCulture);
            ts = ts.Reverse();
            var sb = new StringBuilder();
            for (var i = 0; i < ts.Length; i++)
            {
                sb.Append((i % 3 == 0 && i != 0) ? "," + ts.Substring(i, 1) : ts.Substring(i, 1));
            }

            return sb.ToString().Reverse();
        }


    }
}
