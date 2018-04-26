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
    /// <summary>
    /// 同步通知
    /// </summary>
    [RoutePrefix("AlipayReturn")]
    public class AlipayReturnController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Receive([FromUri]AlipayReturnModel model)
        {
            if (model == null) model = new AlipayReturnModel();
            var logger = LogManager.GetLogger(typeof(AlipayReturnController));
            logger.Info(model.out_trade_no);
            logger.Info(model.app_id);
            //TODO:
            return Redirect("http://www.baidu.com");

        }
    }
}
