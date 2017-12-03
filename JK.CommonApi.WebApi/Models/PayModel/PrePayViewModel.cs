using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.PayModel
{
    /// <summary>
    /// WeixinJSBridge 支付所需参数
    /// </summary>
    public class PrePayViewModel
    {
        /// <summary>
        /// 公众号id
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }
        /// <summary>
        /// 签名方式
        /// </summary>
        public string SignType { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string PaySign { get; set; }
        /// <summary>
        /// prepay_id
        /// </summary>
        public string Package { get; set; }


        /// <summary>
        /// 支付跳转链接(H5支付时才返回)
        /// </summary>
        public string MWebUrl { get; set; }
        /// <summary>
        /// 扫码支付二维码地址（扫码支付时才返回）
        /// </summary>
        public string CodeUrl { get; set; }
    }
}