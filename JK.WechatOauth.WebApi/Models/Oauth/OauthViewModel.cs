using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JK.WechatOauth.WebApi.Models.Oauth
{
    public class OauthViewModel
    {
        /// <summary>
        /// 原访问地址
        /// </summary>
        [Required(ErrorMessage ="必填项")]
        public string RedirectUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 商户Appid（视情况可不填）
        /// </summary>
        public string AppId { get; set; }
    }
}