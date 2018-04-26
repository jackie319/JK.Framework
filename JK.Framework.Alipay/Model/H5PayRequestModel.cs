using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Alipay.Model
{
    public class H5PayRequestModel
    {

        /// <summary>
        /// 	商户网站唯一订单号
        /// </summary>
        public string OutTrabeNo { get; set; }

        /// <summary>
        /// 商品的标题/交易标题/订单标题/订单关键字等。
        /// </summary>
        public string Sbuject { get; set; }
        /// <summary>
        /// 订单总金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000]
        /// </summary>
        public Double TotalAmount { get; set; }
        /// <summary>
        /// 该笔订单允许的最晚付款时间，逾期将关闭交易
        /// </summary>
        public string TimeoutExpress { get; set; }
        /// <summary>
        /// 销售产品码，商家和支付宝签约的产品码。该产品请填写固定值：QUICK_WAP_WAY
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        ///对一笔交易的具体描述信息。如果是多种商品，请将商品描述字符串累加传给body。
        /// </summary>
        public string Boby { get; set; }

        /// <summary>
        /// 收款支付宝用户ID。 如果该值为空，则默认为商户签约账号对应的支付宝用户ID
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// 用户付款中途退出返回商户网站的地址
        /// </summary>
        public string QuitUrl { get; set; }
    }
}
