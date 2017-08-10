﻿using System;
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
        private string Appkey { set; get; }
        private string AppSecret { set; get; }
        private readonly string RegisteUrl = "https://api.netease.im/sms/sendcode.action";
        private readonly string NotifyUrl = "https://api.netease.im/sms/sendtemplate.action";
        public SmsCode(string appkey,string appSecret)
        {
            Appkey = appkey;
            AppSecret = appSecret;
        }
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public SendRegisteCodeResult SendRegisteCode(string phone,int templateid)
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
                string data = "mobile=" + phone + "&templateid =" + templateid + "&codeLen=6";
                var result = webClient.UploadString(url, "POST", data);
                SendRegisteCodeResult model = JsonConvert.DeserializeObject<SendRegisteCodeResult>(result);
                return model;
            }
        }

        /// <summary>
        ///  发送通知类和运营类短信
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public SendRegisteCodeResult SendNotifyCode(string mobiles, int templateid,string param)
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
                string url = NotifyUrl;
                string data = "mobiles=" + mobiles + "&templateid =" + templateid + "&params="+param;
                var result = webClient.UploadString(url, "POST", data);
                SendRegisteCodeResult model = JsonConvert.DeserializeObject<SendRegisteCodeResult>(result);
                return model;
            }
        }
    }
}
