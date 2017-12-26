using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Oauth.Wechat
{
    public class WechatJsCode
    {
        private string SessionKeyUrl = "https://api.weixin.qq.com/sns/jscode2session";
        private string AppId { get; set; }
        private string Secret { get; set; }

        public WechatJsCode(string appId, string secret)
        {
            AppId = appId;
            Secret = secret;
        }

        /// <summary>
        /// 微信小程序获取用户session_key 和openid
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SessionKeyResult GetSessionKey(string code)
        {
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                string url = $"{SessionKeyUrl}?appid={AppId}&secret={Secret}&js_code={code}&grant_type=authorization_code";
                var result = webClient.UploadString(url, "GET");
                SessionKeyResult model = JsonConvert.DeserializeObject<SessionKeyResult>(result);
                return model;
            }
        }
    }
}
