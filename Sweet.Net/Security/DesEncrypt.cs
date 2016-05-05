using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Sweet.Net.Security
{
    public static class DesEncrypt
    {
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="key">加密key</param>
        /// <returns></returns>
        public static string Encrypt(string text, string key = "TEMPKEY")
        {
            var cryptoServiceProvider = new DESCryptoServiceProvider();
            var bytes = Encoding.Default.GetBytes(text);
            cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, cryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            var stringBuilder = new StringBuilder();
            foreach (var num in memoryStream.ToArray())
                stringBuilder.AppendFormat("{0:X2}", num);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="text">解密字符</param>
        /// <param name="key">解密key</param>
        /// <returns></returns>
        public static string Decrypt(string text, string key = "TEMPKEY")
        {
            var cryptoServiceProvider = new DESCryptoServiceProvider();
            var length = text.Length / 2;
            var buffer = new byte[length];
            for (var index = 0; index < length; ++index)
            {
                var num = Convert.ToInt32(text.Substring(index * 2, 2), 16);
                buffer[index] = (byte)num;
            }
            cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, cryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(buffer, 0, buffer.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.Default.GetString(memoryStream.ToArray());
        }
    }
}