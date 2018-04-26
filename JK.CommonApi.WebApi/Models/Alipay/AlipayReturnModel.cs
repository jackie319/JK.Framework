using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JK.CommonApi.WebApi.Models.Alipay
{
    public class AlipayReturnModel
    {
        /// <summary>
        ///支付宝分配给开发者的应用ID
        /// </summary>
        public string app_id { get; set; }
        /// <summary>
        ///接口名称
        /// </summary>
        public string method { get; set; }
        /// <summary>
        /// 签名算法类型，目前支持RSA2和RSA，推荐使用RSA2
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 支付宝对本次支付结果的签名，开发者必须使用支付宝公钥验证签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 编码格式，如utf-8,gbk,gb2312等
        /// </summary>
        public string charset { get; set; }
        /// <summary>
        /// 前台回跳的时间，格式"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 调用的接口版本，固定为：1.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 授权方的appid注：由于本接口暂不开放第三方应用授权，因此auth_app_id=app_id
        /// </summary>
        public string auth_app_id { get; set; }
        /// <summary>
        /// 商户网站唯一订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        ///该交易在支付宝系统中的交易流水号。最长64位。
        /// </summary>
        public string trade_no { get; set; }
        /// <summary>
        /// 该笔订单的资金总额，单位为RMB-Yuan。取值范围为[0.01，100000000.00]，精确到小数点后两位。
        /// </summary>
        public string total_amount { get; set; }
        /// <summary>
        /// 收款支付宝账号对应的支付宝唯一用户号。 以2088开头的纯16位数字
        /// </summary>
        public string seller_id { get; set; }
    }
}