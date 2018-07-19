using JK.CommonApi.WebApi.App_Start;
using JK.CommonApi.WebApi.Models.Alipay;
using JK.CommonApi.WebApi.Models.PayModel;
using JK.Framework.API.Filter;
using JK.PayCenter;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace JK.CommonApi.WebApi.Controllers
{
    public class AlipayApiController : ApiController
    {
        public IPay _Pay;
        private ILog _log;
        // GET: Alipay
        public AlipayApiController(IPay pay)
        {

            _Pay = pay;
            _log = LogManager.GetLogger(typeof(PayController));
        }
        /// <summary>
        /// 网站支付
        /// </summary>
        [Route("")]
        [HttpPost]
        [ApiSessionAuthorize]
        [ApiValidationFilter]
        public HttpResponseMessage AliPay([FromBody]PayViewModel model)
        {
            //  var userModel = (UserModel)HttpContext.Current.User;
            var result = _Pay.Alipay(model.OrderGuid, model.OrderNo, Guid.NewGuid());
            var response = new HttpResponseMessage();
            response.Content = new StringContent(result);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        /// <summary>
        /// 同步返回
        /// </summary>
        /// <param name="model"></param>
        [Route("Return")]
        [HttpGet]
        public void Return([FromUri]AlipayReturnModel model)
        {
           // _Pay.AlipayReturn(model);
        }
    }
}
