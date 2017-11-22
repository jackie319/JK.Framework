using JK.PayCenter;
using JK.PayCenter.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace JK.CommonApi.WebApi.Controllers
{
    public class WxNotifyController : Controller
    {
        private IPay _pay;
        private ILog _log;
        public WxNotifyController( IPay pay)
        {
            _pay = pay;
            _log = LogManager.GetLogger(typeof(WxNotifyController));
        }

        /// 注意：同样的通知可能会多次发送给商户系统。商户系统必须能够正确处理重复的通知。
        // 推荐的做法是，当收到通知进行处理时，首先检查对应业务数据的状态，判断该通知是否已经处理过，
        //如果没有处理过再进行处理，如果处理过直接返回结果成功。
        //在对业务数据进行状态检查和处理之前，要采用数据锁进行并发控制，以避免函数重入造成的数据混乱。 

        public string Notify()
        {
            //特别提醒：商户系统对于支付结果通知的内容一定要做签名验证,
            //并校验返回的订单金额是否与商户侧的订单金额一致，
            //防止数据泄漏导致出现“假通知”，造成资金损失。

            _log.Info("收到支付通知");
            PayNotifyResult result = new PayNotifyResult();
            try
            {
                result = _pay.PayNotify(System.Web.HttpContext.Current);
            }
            catch (Exception ex)
            {
                _log.Info("处理支付通知错误:" + ex.Message);
                _log.Info("处理支付通知错误:" + ex.StackTrace);
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
