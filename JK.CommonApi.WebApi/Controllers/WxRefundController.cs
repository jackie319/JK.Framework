using JK.CommonApi.WebApi.App_Start;
using JK.CommonApi.WebApi.Models.PayModel;
using JK.Framework.API.Model;
using JK.PayCenter;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JK.CommonApi.WebApi.Controllers
{
    [RoutePrefix("Refund")]
    public class WxRefundController : ApiController
    {
        private IRefund _refund;
        private IPay _pay;
        private ILog _log;
        public WxRefundController(IPay pay, IRefund refund)
        {
            // _order = order;
            _pay = pay;
            _refund = refund;
            _log = LogManager.GetLogger(typeof(PayController));
        }

        /// <summary>
        /// 退款（该接口前端不对接）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [ApiSessionAuthorize]
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
