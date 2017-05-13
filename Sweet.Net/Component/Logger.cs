using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweet.Net.Component
{
    public static class Logger
    {
        static string LogPath { get; set; }

        static Logger()
        {
            LogPath = AppDomain.CurrentDomain.BaseDirectory + "/Logs/";
        }

        /// <summary>
        /// 一般日志
        /// </summary>
        /// <param name="content">日志内容</param>
        public static void Info(string content)
        {
            SaveToFile(content, 1);
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="content">日志内容</param>
        public static void Warn(string content)
        {
            SaveToFile(content, 2);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="content">日志内容</param>
        public static void Error(string content)
        {
            SaveToFile(content, 3);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="content">日志内容</param>
        public static void Fatal(string content)
        {
            SaveToFile(content, 4);
        }

        private static void SaveToFile(string content, int level)
        {
            try
            {
                var path = GetFilePath(level);
                var method = new StackTrace(true).GetFrame(2).GetMethod();
                var declaringType = method.DeclaringType;
                var nspace = declaringType != null ? declaringType.Namespace : string.Empty;
                var md = "<" + nspace + "." + method.Name + "> ";

                using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 1024, false))
                {
                    var time = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss" + "]");
                    var bty = Encoding.UTF8.GetBytes(time + md + content + "\r\n");
                    fs.Write(bty, 0, bty.Length);
                    fs.Close();
                }
            }
            catch
            {

            }
        }

        private static string GetFilePath(int level)
        {
            var logPath = LogPath;
            switch (level)
            {
                case 1:
                    logPath += "Info/"; break;
                case 2:
                    logPath += "Warn/"; break;
                case 3:
                    logPath += "Error/"; break;
                case 4:
                    logPath += "Fatal/"; break;
            }
            if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
            logPath += DateTime.Now.ToString("yyyy-MM-dd") + ".log";

            return logPath;

        }
    }
}
