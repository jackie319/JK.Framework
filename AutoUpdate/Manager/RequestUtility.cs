using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HouseManage.Container.Update
{
    internal class RequestUtility
    {
        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="data">请求对象</param>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string data, string url)
        {
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                var result = await webClient.UploadStringTaskAsync(url, "POST", data);
                return result;
            }
        }

        public static string Get(string url)
        {
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                var result = webClient.DownloadString(url);
                return result;
            }
        }
    }
}
