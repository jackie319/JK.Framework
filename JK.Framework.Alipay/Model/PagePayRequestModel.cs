using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Alipay.Model
{
    public class PagePayRequestModel
    {
        /// <summary>
        /// 商户订单号，64个字符以内、可包含字母、数字、下划线；需保证在商户端不重复
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 销售产品码，与支付宝签约的产品码名称。 注：目前仅支持FAST_INSTANT_TRADE_PAY
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 订单总金额，单位为元，精确到小数点后两位，取值范围[0.01, 100000000]
        /// </summary>

        public double TotalAmount { get; set; }

        /// <summary>
        /// 订单标题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 订单描述
        /// </summary>
        public string Boby { get; set; }


    }
}
