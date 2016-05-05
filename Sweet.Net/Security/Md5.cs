using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sweet.Net.Security
{
    public static class Md5
    {
        /// <summary>
        /// 将字符以MD5方式加密
        /// </summary>
        /// <param name="input">加密字符串</param>
        /// <param name="str">附加字符</param>
        /// <returns>加密结果</returns>
        public static string EnMd5(string input, string str = "")
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            var sb = new StringBuilder();
            foreach (var t in data)
                sb.Append(t.ToString("x2"));
            return sb + str;
        }
    }
}
