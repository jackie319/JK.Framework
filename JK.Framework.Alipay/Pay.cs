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
        private IAopClient _AlipayClient;
        private ConmonRequestModel _ConmonRequestModel;

        public Pay(ConmonRequestModel commonRequestModel)
        {
            //公共参数 TODO:建议从数据库获取
            _ConmonRequestModel = commonRequestModel;
            //TODO:待改。alipayClient只需要初始化一次，后续调用不同的API都可以使用同一个alipayClient对象.
            _AlipayClient = new DefaultAopClient(_ConmonRequestModel.Getway, _ConmonRequestModel.AppId, _ConmonRequestModel.MerchantPrivateKey, _ConmonRequestModel.Format, _ConmonRequestModel.Version, _ConmonRequestModel.SignType, _ConmonRequestModel.AlipayPublicKey, _ConmonRequestModel.Charset, false);
        }

        /// <summary>
        /// 电脑网站支付
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AlipayTradePagePayResponse DefaultPay(PagePayRequestModel model)
        {
            AlipayTradePagePayRequest request = new AlipayTradePagePayRequest();
            request.SetReturnUrl(_ConmonRequestModel.ReturnUrl);
            request.SetNotifyUrl(_ConmonRequestModel.NotifyUrl);
            request.BizContent = "{" +
            "    \"body\":\"" + model.Boby + "\"," +
            "    \"subject\":\"" + model.Subject + "\"," +
            "    \"out_trade_no\":\"" + model.OutTradeNo + "\"," +
            "    \"total_amount\":" + model.TotalAmount + ", " +
            "    \"product_code\":\"" + model.ProductCode + "\"" +
            "  }";
            AlipayTradePagePayResponse response = _AlipayClient.pageExecute(request);
            return response;
        }

        /// <summary>
        /// 手机网站支付
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AlipayTradeWapPayResponse H5DefulatPay(H5PayRequestModel model)
        {
            AlipayTradeWapPayRequest request = new AlipayTradeWapPayRequest();
            request.SetReturnUrl(_ConmonRequestModel.ReturnUrl);
            request.SetNotifyUrl(_ConmonRequestModel.NotifyUrl);
            request.BizContent = "{" +
            "    \"body\":\"" + model.Boby + "\"," +
            "    \"subject\":\"" + model.Sbuject + "\"," +
            "    \"out_trade_no\":\"" + model.OutTrabeNo + "\"," +
            "    \"timeout_express\":\"" + model.TimeoutExpress + "\"," +
            "    \"total_amount\":" + model.TotalAmount + "," +
            "    \"quit_url\":\"" + model.QuitUrl + "\"," +
            "    \"product_code\":\"" + model.ProductCode + "\"" +
            "  }";
            AlipayTradeWapPayResponse response = _AlipayClient.pageExecute(request);
            return response;
        }
    }
}
