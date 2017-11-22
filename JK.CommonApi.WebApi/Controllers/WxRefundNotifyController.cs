using JK.PayCenter;
using JK.PayCenter.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JK.CommonApi.WebApi.Controllers
{
    public class WxRefundNotifyController : Controller
    {
        private IRefund _refund;
        private IPay _pay;
        private ILog _log;
        public WxRefundNotifyController( IPay pay, IRefund refund)
        {
            _pay = pay;
            _refund = refund;
            _log = LogManager.GetLogger(typeof(WxRefundNotifyController));
        }

        public string Notify()
        {

            _log.Info("收到退款通知");
            PayNotifyResult result = new PayNotifyResult();
            try
            {
                result = _refund.RefundNotify(System.Web.HttpContext.Current);
            }
            catch (Exception ex)
            {
                _log.Info("处理退款通知错误:" + ex.Message);
                _log.Info("处理退款通知错误:" + ex.StackTrace);
                result.ReturnCode = "FAIL";
                result.ReturnMsg = ex.Message;
            }


            string xml = string.Format(@"<xml>
   <return_code><![CDATA[{0}]]></return_code>
   <return_msg><![CDATA[{1}]]></return_msg>
</xml>", result.ReturnCode, result.ReturnMsg);
            return xml;
        }
    }
}