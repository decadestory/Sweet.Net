using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Sweet.Net.Security;

namespace Sweet.Net.Ext
{
    public static class StringExt
    {
        /// <summary>   
        /// 截取文本，区分中英文字符，中文算两个长度，英文算一个长度
        /// </summary>
        /// <param name="str">待截取的字符串</param>
        /// <param name="length">需计算长度的字符串</param>
        /// <param name="omitStr">省略字符默认(...)</param>
        /// <returns>string</returns>
        public static string Cut(this string str, int length = 10, string omitStr = "...")
        {
            var temp = str;
            var j = 0;
            var k = 0;
            for (var i = 0; i < temp.Length; i++)
            {
                j = Regex.IsMatch(temp.Substring(i, 1), @"[\u4e00-\u9fa5]+") ? j + 2 : j + 1;
                k = j <= length ? k + 1 : k;
                if (j > length) return temp.Substring(0, k) + omitStr;
            }
            return temp;
        }

        /// <summary>
        /// 扩展方法:手机号转化成189****6547形式
        /// </summary>
        /// <param name="phoneNumber">11位手机号</param>
        /// <returns>失败返回原手机号</returns>
        public static string GetHidePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 11)
            {
                return phoneNumber.Substring(0, 3) + "****" + phoneNumber.Substring(7, 4);
            }
            return phoneNumber;
        }

        /// <summary>
        /// 扩展方法:判断字符串是否只由数字组成
        /// </summary>
        /// <param name="oldStr">字符</param>
        /// <returns>true or false</returns>
        public static bool IsNumber(this string oldStr)
        {
            return Regex.IsMatch(oldStr, @"^[0-9]+$");
        }

        /// <summary>
        /// 扩展方法:判断字符串是否只由数字或字母组成
        /// </summary>
        /// <param name="oldStr">字符</param>
        /// <returns>true or false</returns>
        public static bool IsNumberOrLetter(this string oldStr)
        {
            return Regex.IsMatch(oldStr, @"^[A-Za-z0-9]+$");
        }

        /// <summary>
        /// 扩展方法:判断字符串是否只由数字或字母或汉字组成
        /// </summary>
        /// <param name="oldStr">字符</param>
        /// <returns>true or false</returns>
        public static bool IsNumberOrLetterOrChinese(this string oldStr)
        {
            return Regex.IsMatch(oldStr, @"[a-zA-Z0-9\u4e00-\u9fa5]{1,50}");
        }

        /// <summary>
        /// 扩展方法:判断字符串是否只由汉字组成
        /// </summary>
        /// <param name="oldStr">字符</param>
        /// <returns>true or false</returns>
        public static bool IsChinese(this string oldStr)
        {
            return Regex.IsMatch(oldStr, @"^[\u4e00-\u9fa5]+$");
        }

        /// <summary>
        /// 扩展方法:判断字符串是否是Email
        /// </summary>
        /// <param name="oldStr">字符</param>
        /// <returns>true or false</returns>
        public static bool IsEmail(this string oldStr)
        {
            return Regex.IsMatch(oldStr, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 功能：检查中国公民身份证是否正确
        /// </summary>
        /// <param name="cid">需检查的身份证号码</param>
        /// <returns>返回由省市，生日，性别组成的字符串</returns>
        public static bool IsAbsoluteIdCard(this string cid)
        {
            var aCity = new[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };
            double iSum = 0;
            var rg = new Regex(@"^\d{17}(\d|x)$");
            var mc = rg.Match(cid);
            if (!mc.Success) return false; //身份证格式
            cid = cid.ToLower();
            cid = cid.Replace("x", "a");
            if (aCity[int.Parse(cid.Substring(0, 2))] == null) return false; //非法地区
            try
            {
                DateTime.Parse(cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2));
            }
            catch
            {
                return false; //非法生日
            }
            for (var i = 17; i >= 0; i--)
            {
                iSum += (System.Math.Pow(2, i) % 11) * int.Parse(cid[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);
            }
            if (iSum % 11 != 1) return false; //非法证号

            var data = aCity[int.Parse(cid.Substring(0, 2))] + "," + cid.Substring(6, 4) + "-" + cid.Substring(10, 2) +
                       "-" + cid.Substring(12, 2) + "," + (int.Parse(cid.Substring(16, 1)) % 2 == 1 ? "男" : "女");
            return true;
        }

        /// <summary>
        /// 扩展方法:将字符以MD5方式加密
        /// </summary>
        /// <param name="input">加密字符串</param>
        /// <param name="str"></param>
        /// <returns>加密结果</returns>
        public static string GetMd5(this string input, string str = "")
        {
            return Md5.EnMd5(input, str);
        }

        /// <summary>
        /// 扩展方法:base64加密
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <returns>加密结果</returns>
        public static string ToBase64Str(this string str)
        {
            return Base64.EnBase64(str);
        }

        /// <summary>
        /// 扩展方法:base64解密
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <returns>加密结果</returns>
        public static string DeBase64Str(this string str)
        {
            return Base64.DeBase64(str);
        }

        /// <summary>
        /// 扩展方法:DES加密
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <param name="key">加密key</param>
        /// <returns>加密结果</returns>
        public static string Encrypt(this string str,string key="TEMPKEY")
        {
            return DesEncrypt.Encrypt(str,key);
        }

        /// <summary>
        /// 扩展方法:DES解密
        /// </summary>
        /// <param name="str">解密字符串</param>
        /// <param name="key">解密key</param>
        /// <returns>解密结果</returns>
        public static string Decrypt(this string str, string key = "TEMPKEY")
        {
            return DesEncrypt.Decrypt(str, key);
        }

        /// <summary>
        /// 扩展方法:替换字符串中的html代码(去尖括号)
        /// </summary>
        /// <param name="str">html字符</param>
        /// <returns>处理后字符</returns>
        public static string ReplaceHtmlString(this string str)
        {
            return Regex.Replace(str, @"<[^>]+>", "");
        }

        /// <summary>
        /// 扩展方法：将字符串逆转
        /// </summary>
        /// <param name="oldStr">字符</param>
        /// <returns>逆转字符串</returns>
        public static string Reverse(this string oldStr)
        {
            var temp = oldStr.ToCharArray();
            Array.Reverse(temp);
            return new string(temp);
        }

    }
}
