using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sweet.Net.Ext
{
    public static class RandomExt
    {
        static readonly Random Rand = new Random(~unchecked((int)DateTime.Now.Ticks));

        /// <summary>
        /// 生成随机字母与数字组合
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <returns>生成结果</returns>
        public static string GenerateCharAndNumber(int length=6)
        {
            Thread.Sleep(1);
            var p = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var result = string.Empty;
            var n = p.Length;
            for (var i = 0; i < length; i++)
                result += p[Rand.Next(0, n)];
            return result;
        }

        /// <summary>
        /// 生成随机数字组合
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <returns>生成结果</returns>
        public static string GenerateNumber(int length = 6)
        {
            Thread.Sleep(1);
            var p = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var result = string.Empty;
            var n = p.Length;
            for (var i = 0; i < length; i++)
                result += p[Rand.Next(0, n)];
            return result;
        }

        /// <summary>
        /// 生成随机字母组合
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <returns>生成结果</returns>
        public static string GenerateChar(int length = 6)
        {
            Thread.Sleep(1);
            var p = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var result = string.Empty;
            var n = p.Length;
            for (var i = 0; i < length; i++)
                result += p[Rand.Next(0, n)];
            return result;
        }
    }
}
