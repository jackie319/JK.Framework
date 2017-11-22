using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using JK.Framework.Alipay.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Alipay
{
    public class Pay
    {
        private string AppId { get; set; }
        private string MerchantPrivateKey { get; set; }
        private string AlipayPublicKey { get; set; }

        public Pay(string appid,string merchantPrivateKey,string alipayPublicKey)
        {
            AppId = appid;
            MerchantPrivateKey = merchantPrivateKey;
            AlipayPublicKey = alipayPublicKey;
        }

        /// <summary>
        /// (统一收单交易支付接口)
        /// </summary>
        public TradePayResponseModel AlipayTradePay(TradePayRequestModel model)
        {
            IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", AppId, MerchantPrivateKey, "json", "1.0", "RSA2", AlipayPublicKey, "GBK", false);
            AlipayTradePayRequest request = new AlipayTradePayRequest();
            request.BizContent = "{" +
            "\"out_trade_no\":\""+model.out_trade_no+"\"," +
            "\"scene\":\"bar_code\"," +
            "\"auth_code\":\"28763443825664394\"," +
            "\"product_code\":\"FACE_TO_FACE_PAYMENT\"," +
            "\"subject\":\"Iphone616G\"," +
            "\"buyer_id\":\"2088202954065786\"," +
            "\"seller_id\":\"2088102146225135\"," +
            "\"total_amount\":88.88," +
            "\"discountable_amount\":8.88," +
            "\"body\":\"Iphone616G\"," +
            "\"goods_detail\":[{" +
            "\"goods_id\":\"apple-01\"," +
            "\"goods_name\":\"ipad\"," +
            "\"quantity\":1," +
            "\"price\":2000," +
            "\"goods_category\":\"34543238\"," +
            "\"body\":\"特价手机\"," +
            "\"show_url\":\"http://www.alipay.com/xxx.jpg\"" +
            "}]," +
            "\"operator_id\":\"yx_001\"," +
            "\"store_id\":\"NJ_001\"," +
            "\"terminal_id\":\"NJ_T_001\"," +
            "\"extend_params\":{" +
            "\"sys_service_provider_id\":\"2088511833207846\"" +
            "}," +
            "\"timeout_express\":\"90m\"" +
            "}";

            AlipayTradePayResponse response = client.Execute(request);
            TradePayResponseModel result = JsonConvert.DeserializeObject<TradePayResponseModel>(response.Body);
            return result;
        }
    }
}
