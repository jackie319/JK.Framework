using Aop.Api.Util;
using JK.CommonApi.WebApi.Models.Alipay;
using JK.JKUserAccount;
using JK.PayCenter;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JK.CommonApi.WebApi.Controllers
{
    [RoutePrefix("AlipayNotify")]
    public class AlipayNotifyController : ApiController
    {

        public IPay _Pay;
        private ILog _log;
        // GET: Alipay
        public AlipayNotifyController(IPay pay)
        {

            _Pay = pay;
            _log = LogManager.GetLogger(typeof(PayController));
        }

        /* 实际验证过程建议商户添加以下校验。
1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，
2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），
3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id/seller_email）
4、验证app_id是否为该商户本身。
*/

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public string Notify()
        {
            _log.Info("收到支付宝支付通知");
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context  
            HttpRequestBase request = context.Request;//定义传统request对象  

            Dictionary<string, string> sArray = GetRequestPost(request);
            if (sArray.Count != 0)
            {
                bool flag = AlipaySignature.RSACheckV1(sArray, AppSetting.Instance().AlipayPublicKey, "utf-8", "RSA2", false);
                if (flag)
                {
                    //交易状态
                    //判断该笔订单是否在商户网站中已经做过处理
                    //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    //请务必判断请求时的total_amount与通知时获取的total_fee为一致的
                    //如果有做过处理，不执行商户的业务程序

                    //注意：
                    //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知


                    AlipayNotifyModel model = new AlipayNotifyModel();
                    model.trade_status = request.Form["trade_status"];
                    model.trade_no = request.Form["trade_no"];
                    model.out_trade_no = request.Form["out_trade_no"];
                    model.total_amount = request.Form["total_amount"];
                    model.app_id = request.Form["app_id"];
                    model.seller_id = request.Form["seller_id"];
                  //  _Pay.AlipayNotify(model);
                    _log.Info(model.trade_status);
                    return "success";
                }
                else
                {
                    return "fail";
                }
            }
            return "fail";
        }

        public Dictionary<string, string> GetRequestPost(HttpRequestBase request)
        {
            int i = 0;
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //coll = Request.Form;
            coll = request.Form;
            String[] requestItem = coll.AllKeys;
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], request.Form[requestItem[i]]);
            }
            return sArray;

        }
    }
}
