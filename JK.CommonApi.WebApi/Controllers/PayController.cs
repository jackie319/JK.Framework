using JK.CommonApi.Domain;
using JK.CommonApi.WebApi.App_Start;
using JK.CommonApi.WebApi.Models.PayModel;
using JK.Framework.API.Filter;
using JK.Framework.API.Model;
using JK.Framework.Extensions;
using JK.JKUserAccount.ServiceModel;
using JK.PayCenter;
using log4net;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JK.CommonApi.WebApi.Controllers
{
    [RoutePrefix("Pay")]
    public class PayController : ApiController
    {
       // private IOrder _order;
        private IRefund _refund;
        private IPay _pay;
        private ILog _log;
        public PayController(IPay pay, IRefund refund)
        {
           // _order = order;
            _pay = pay;
            _refund = refund;
            _log = LogManager.GetLogger(typeof(PayController));
        }
        /// <summary>
        /// 支付 （非公众号支付时不传openId）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [ApiSessionAuthorize]
        [ApiValidationFilter]
        public PrePayViewModel Pay([FromBody] PayViewModel model)
        {
            _log.Info("支付来自页面：" + HttpContext.Current.Request.UrlReferrer.ToString());
            PrePayViewModel pre = new PrePayViewModel();
            var ip = "127.0.0.1";
            try
            {
                ip = HttpContext.Current.Request.ServerVariables.Get("Remote_Addr").ToString();
            }
            catch (Exception e)
            {
            }
            var mmyUser = (UserModel)HttpContext.Current.User;
            //安全起见，订单guid和订单号都需要
            var result = _pay.WechatPay(model.OrderGuid, model.OrderNo, mmyUser.OpenId ?? string.Empty, ip, model.Payment);
            var setting = new UnifiedOrderSetting();

            _log.Info("H5跳转链接=" + result.MWebUrl);
            pre.MWebUrl = result.MWebUrl;
            pre.AppId = setting.AppId;
            pre.NonceStr = Guid.NewGuid().ToString("N");
            pre.SignType = "MD5";
            pre.TimeStamp = DateTimeHelper.GetTimeStamp();
            pre.Package = "prepay_id=" + result.PrepayId;

            RequestHandler payHandler = new RequestHandler(null);
            payHandler.SetParameter("appId", pre.AppId);
            payHandler.SetParameter("timeStamp", pre.TimeStamp);
            payHandler.SetParameter("nonceStr", pre.NonceStr);
            payHandler.SetParameter("package", pre.Package);
            payHandler.SetParameter("signType", pre.SignType);
            string sign = payHandler.CreateMd5Sign("key", setting.Key);
            pre.PaySign = sign;
            return pre;
        }

        /// <summary>
        ///  支付测试（该接口前端不对接）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Test")]
        [ApiSessionAuthorize]
        [HttpPost]
        public PrePayViewModel PayTest([FromBody] PayViewModel model)
        {
            _log.Info("支付来自页面：" + HttpContext.Current.Request.UrlReferrer.ToString());
            PrePayViewModel pre = new PrePayViewModel();
            var ip = "127.0.0.1";
            try
            {
                ip = HttpContext.Current.Request.ServerVariables.Get("Remote_Addr").ToString();
            }
            catch (Exception e)
            {
            }
            var mmyUser = (UserModel)HttpContext.Current.User;
            //安全起见，订单guid和订单号都需要
            var result = _pay.WechatPay(model.OrderGuid, model.OrderNo, mmyUser.OpenId ?? string.Empty, ip, model.Payment);
            var setting = new UnifiedOrderSetting();

            _log.Info("H5跳转链接=" + result.MWebUrl);
            pre.MWebUrl = result.MWebUrl;
            pre.CodeUrl = result.CodeUrl;
            pre.AppId = setting.AppId;
            pre.NonceStr = Guid.NewGuid().ToString("N");
            pre.SignType = "MD5";
            pre.TimeStamp = DateTimeHelper.GetTimeStamp();
            pre.Package = "prepay_id=" + result.PrepayId;

            RequestHandler payHandler = new RequestHandler(null);
            payHandler.SetParameter("appId", pre.AppId);
            payHandler.SetParameter("timeStamp", pre.TimeStamp);
            payHandler.SetParameter("nonceStr", pre.NonceStr);
            payHandler.SetParameter("package", pre.Package);
            payHandler.SetParameter("signType", pre.SignType);
            string sign = payHandler.CreateMd5Sign("key", setting.Key);
            pre.PaySign = sign;
            return pre;
        }


        /// <summary>
        /// 退款（该接口前端不对接）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Refund")]
        [HttpPost]
        public ApiResultModel RefundTest([FromBody] RefundPayViewModel model)
        {
            try
            {
                _refund.WechatRefund(model.OrderRefundGuid);
            }
            catch (Exception ex)
            {
                _log.Info("退款接口错误：" + ex.Message);
                _log.Info("退款接口错误：" + ex.StackTrace);
                return this.ResultApiError(ex.Message);
            }

            return this.ResultApiSuccess();
        }

    }
}
