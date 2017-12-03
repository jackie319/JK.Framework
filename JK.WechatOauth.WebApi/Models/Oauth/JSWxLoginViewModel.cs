using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JK.WechatOauth.WebApi.Models.Oauth
{
    public class JSWxLoginViewModel
    {
        /// <summary>
        /// 第三方页面显示二维码的容器id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 应用唯一标识，在微信开放平台提交应用审核通过后获得
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 应用授权作用域，拥有多个作用域用逗号（,）分隔，网页应用目前仅填写snsapi_login即可
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// 重定向地址，需要进行UrlEncode
        /// </summary>
        public string RedirectUri { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string State { get; set; }

        public string Style { get; set; }

        public string Href { get; set; }
    }
}