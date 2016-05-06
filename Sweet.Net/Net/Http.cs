using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Sweet.Net.API;

namespace Sweet.Net.Net
{
    public class Http
    {
        /// <summary>
        /// HttpGet请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <returns>返回字符</returns>
        public static string HttpGet(string url)
        {
            var hc = new HttpClient();
            var response = hc.GetStringAsync(url);
            return response.Result;
        }

        /// <summary>
        /// HttpGet请求,带头信息
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="headers">头信息</param>
        /// <returns>返回字符</returns>
        public static string HttpGet(string url, List<KeyValue> headers)
        {
            var hc = new HttpClient();
            headers.ForEach(t => hc.DefaultRequestHeaders.Add(t.Key, t.Value));
            var response = hc.GetStringAsync(url);
            return response.Result;
        }

        /// <summary>
        /// HttpPost请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="kayValues">参数</param>
        /// <returns>返回字符</returns>
        public static string HttpPost(string url, List<KeyValue> kayValues)
        {
            var hc = new HttpClient();
            var param = kayValues.Select(item => new KeyValuePair<string, string>(item.Key, item.Value)).ToList();
            var content = new FormUrlEncodedContent(param);
            var response = hc.PostAsync(url, content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// HttpPost请求,带头信息
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="kayValues">参数</param>
        /// <param name="headers">请求头信息</param>
        /// <returns>返回字符</returns>
        public static string HttpPost(string url, List<KeyValue> kayValues,List<KeyValue> headers)
        {
            var hc = new HttpClient();
            headers.ForEach(t=>hc.DefaultRequestHeaders.Add(t.Key,t.Value));
            var param = kayValues.Select(item => new KeyValuePair<string, string>(item.Key, item.Value)).ToList();
            var content = new FormUrlEncodedContent(param);
            var response = hc.PostAsync(url, content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

    }
}
