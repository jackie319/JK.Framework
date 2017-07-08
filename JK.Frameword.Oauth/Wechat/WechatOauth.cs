using JK.Frameword.Oauth.Wechat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace JK.Framework.Oauth.Wechat
{
    public class WechatOauth
    {
        private string AccessTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token";
        private string RefreshAccessTokenUrl = "https://api.weixin.qq.com/sns/oauth2/refresh_token";
        private string UserInfoUrl = "https://api.weixin.qq.com/sns/userinfo";
        private string CheckAccessTokenUrl = "https://api.weixin.qq.com/sns/auth";
        private string AppId { get; set; }
        private string Secret { get; set; }

        public WechatOauth(string appId, string secret)
        {
            AppId = appId;
            Secret = secret;
        }
        /// <summary>
        /// 获取AccessToken
        /// 尤其注意：由于公众号的secret和获取到的access_token安全级别都非常高，必须只保存在服务器，
        /// 不允许传给客户端。后续刷新access_token、通过access_token获取用户信息等步骤，也必须从服务器发起。
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public AccessTokenResult GetAccessToken(string code)
        {
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                string url = $"{AccessTokenUrl}?appid={AppId}&secret={Secret}&code={code}&grant_type=authorization_code";
                var result = webClient.UploadString(url, "GET");
                AccessTokenResult model = JsonConvert.DeserializeObject<AccessTokenResult>(result);
                return model;
            }
        }
        /// <summary>
        /// 刷新AccessToken
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public AccessTokenResult RefreshAccessToken(string refreshToken)
        {
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                string url = $"{RefreshAccessTokenUrl}?appid={AppId}&grant_type=refresh_token&refresh_token={refreshToken}";
                var result = webClient.UploadString(url, "GET");
                AccessTokenResult model = JsonConvert.DeserializeObject<AccessTokenResult>(result);
                return model;
            }
        }

        /// <summary>
        /// 拉取用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public UserInfoResult GetUserInfo(string accessToken, string openId)
        {
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                string url = $"{UserInfoUrl}?access_token={accessToken}&openid={openId}&lang=zh_CN";
                var result = webClient.UploadString(url, "GET");
                UserInfoResult model = JsonConvert.DeserializeObject<UserInfoResult>(result);
                return model;
            }
        }
        /// <summary>
        /// 检验AccessToken是否有效
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public CheckAccessTokenResult CheckAccessToken(string accessToken, string openId)
        {
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                string url = $"{CheckAccessTokenUrl}?access_token={accessToken}&openid={openId}";
                var result = webClient.UploadString(url, "GET");
                CheckAccessTokenResult model = JsonConvert.DeserializeObject<CheckAccessTokenResult>(result);
                return model;
            }
        }

    }
}
