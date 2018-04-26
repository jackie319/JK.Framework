using JK.CommonApi.WebApi.Models.Alipay;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JK.CommonApi.WebApi.Controllers
{
    [RoutePrefix("AlipayNotify")]
    public class AlipayNotifyController : ApiController
    {
        [Route("")]
        [HttpPost]
        public void Notify([FromBody]AlipayNotifyModel model)
        {
            if (model == null) model = new AlipayNotifyModel();
            //HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context  
            //HttpRequestBase request = context.Request;//定义传统request对象  
            var logger = LogManager.GetLogger(typeof(AlipayNotifyController));
            logger.Info(model.out_trade_no);
            logger.Info(model.app_id);

            //            Map<String, String> paramsMap = ... //将异步通知中收到的所有参数都存放到map中
            //boolean signVerified = AlipaySignature.rsaCheckV1(paramsMap, ALIPAY_PUBLIC_KEY, CHARSET, SIGN_TYPE) //调用SDK验证签名
            //if (signVerfied)
            //            {
            //                // TODO 验签成功后，按照支付结果异步通知中的描述，对支付结果中的业务内容进行二次校验，校验成功后在response中返回success并继续商户自身业务处理，校验失败返回failure
            //            }
            //            else
            //            {
            //                // TODO 验签失败则记录异常日志，并在response中返回failure.
            //            }
        }
    }
}
