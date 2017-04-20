using System;
using System.Net;
using System.Text;
using JK.Framework.Extensions;
using Newtonsoft.Json;

namespace JK.Framework.Sms.Netease
{
    /// <summary>
    /// 网易云短信
    /// </summary>
    public class SmsCode
    {
        public string Appkey { set; get; }
        public string AppSecret { set; get; }
        public string RegisteCodeTemplateid { set; get; }
        public string RegisteUrl { set; get; }

        public SmsCode(string appkey,string appSecret,string registeCodeTemplateid,string registeUrl)
        {
            Appkey = appkey;
            AppSecret = appSecret;
            RegisteCodeTemplateid = registeCodeTemplateid;
            RegisteUrl = registeUrl;
        }

        public SendRegisteCodeResult SendRegisteCode(string phone)
        {
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                string nonce = Guid.NewGuid().ToString("N");
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
                DateTime now = DateTime.Now;
                string curTime = (now - dt).TotalSeconds.ToString();
                var str = AppSecret + nonce + curTime;
                var sha1 = str.SHA1_Encrypt();
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");
                webClient.Headers.Add("AppKey", Appkey);
                webClient.Headers.Add("Nonce", nonce);
                webClient.Headers.Add("CurTime", curTime);
                webClient.Headers.Add("CheckSum", sha1);
                string url = RegisteUrl;
                string data = "mobile=" + phone + "&templateid =" + RegisteCodeTemplateid + "&codeLen=6";
                var result = webClient.UploadString(url, "POST", data);
                SendRegisteCodeResult model = JsonConvert.DeserializeObject<SendRegisteCodeResult>(result);
                return model;
            }
        }
    }
}
