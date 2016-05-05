using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweet.Net.Security
{
    public static class Base64
    {
        /// <summary>
        /// base64加密
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <returns>加密结果</returns>
        public static string EnBase64(string str)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(str)).Replace("+", "%2B");
        }

        /// <summary>
        /// base64解密
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <returns>加密结果</returns>
        public static string DeBase64(string str)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(str.Replace("%2B", "+")));
        }
    }
}
