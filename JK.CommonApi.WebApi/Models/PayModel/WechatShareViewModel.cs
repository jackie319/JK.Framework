using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.PayModel
{
    /// <summary>
    /// 微信分享需要参数
    /// </summary>
    public class WechatShareViewModel
    {
        /// <summary>
        /// 微信AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// 随机码
        /// </summary>
        public string NonceStr { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }

        public static WechatShareViewModel CopyFrom(JsSdkUiPackage package)
        {
            WechatShareViewModel model = new WechatShareViewModel();
            model.AppId = package.AppId;
            model.Timestamp = package.Timestamp;
            model.NonceStr = package.NonceStr;
            model.Signature = package.Signature;
            return model;
        }
    }
}