using Senparc.Weixin.MP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.JKUserAccount.ServiceModel
{
    public class UnifiedOrderSetting
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId = "wx153d31e744b266d0";

        //抽取到数据库
        public string AppSecret = "1f3af6756d168f35704555fd77a7ae58";

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId = "1480012562";

        /// <summary>
        /// Key TODO:提取到数据库
        /// </summary>
        public string Key = "9EFE9DE8918E47E0B1718D2F0ABFjklt";


        /// <summary>
        /// AppKey
        /// </summary>
        public string AppKey = "";

        public string Cert = @"D:\cert\apiclient_cert.p12";

        #region 服务商

        /// <summary>
        /// 子商户公众账号ID sub_appid
        /// </summary>
        public string SubAppId { get; set; }

        /// <summary>
        /// 子商户号 sub_mch_id  是 String(32)  1900000109	微信支付分配的子商户号
        /// </summary>
        public string SubMchId { get; set; }

        #endregion

        /// <summary>
        /// 自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"，String(32)如：013467007045764
        /// </summary>
        public string DeviceInfo { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }
        /// <summary>
        /// 签名类型，默认为MD5，支持HMAC-SHA256和MD5。（使用默认）
        /// </summary>
        //public string SignType { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 商品详细列表，使用Json格式，传输签名前请务必使用CDATA标签将JSON文本串保护起来。
        ///cost_price Int 可选 32 订单原价，商户侧一张小票订单可能被分多次支付，订单原价用于记录整张小票的支付金额。当订单原价与支付金额不相等则被判定为拆单，无法享/受/优/惠。
        /// receipt_id String 可选 32 商家小票ID
        ///goods_detail 服务商必填[]：
        ///└ goods_id String 必填 32 商品的编号
        ///└ wxpay_goods_id String 可选 32 微信支付定义的统一商品编号
        ///└ goods_name String 可选 256 商品名称 
        ///└ quantity Int 必填  32 商品数量
        ///└ price Int 必填 32 商品单价，如果商户有优惠，需传输商户优惠后的单价
        ///注意：单品总金额应&lt;=订单总金额total_fee，否则会无法享受优惠。
        /// String(6000)
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。String(127)，如：深圳分店
        /// </summary>
        public string Attach { get; set; }
        /// <summary>
        /// 符合ISO 4217标准的三位字母代码，默认人民币：CNY，详细列表请参见货币类型。String(16)，如：CNY
        /// </summary>
        public string FeeType { get; set; }
        /// <summary>
        /// 商家订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 商品金额,以分为单位(money * 100).ToString()
        /// </summary>
        public int TotalFee { get; set; }
        /// <summary>
        /// 用户的公网ip，不是商户服务器IP
        /// </summary>
        public string SpbillCreateIP { get; set; }
        /// <summary>
        /// 订单生成时间，最终生成格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。其他详见时间规则。
        /// 如果为空，则默认为当前服务器时间
        /// </summary>
        public string TimeStart { get; set; }
        /// <summary>
        /// 订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。其他详见时间规则
        /// 注意：最短失效时间间隔必须大于5分钟。
        /// 留空则不设置失效时间
        /// </summary>
        public string TimeExpire { get; set; }
        /// <summary>
        /// 商品标记，使用代金券或立减优惠功能时需要的参数，说明详见代金券或立减优惠。String(32)，如：WXG
        /// </summary>
        public string GoodsTag { get; set; }
        /// <summary>
        /// 接收财付通通知的URL
        /// </summary>
        public string NotifyUrl { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public TenPayV3Type TradeType { get; set; }
        /// <summary>
        /// trade_type=NATIVE时（即扫码支付），此参数必传。此参数为二维码中包含的商品ID，商户自行定义。
        /// String(32)，如：12235413214070356458058
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 上传此参数no_credit--可限制用户不能使用信用卡支付
        /// </summary>
        public string LimitPay { get; set; }
        /// <summary>
        /// 用户的openId
        /// </summary>
        public string OpenId { get; set; }

        #region 服务商

        /// <summary>
        /// 用户子标识
        /// </summary>
        public string SubOpenid { get; set; }

        #endregion





        public readonly string Sign;
    }
}
