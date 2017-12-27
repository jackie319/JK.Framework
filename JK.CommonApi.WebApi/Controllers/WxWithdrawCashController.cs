using JK.CommonApi.Domain;
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
using System.Web;
using System.Web.Http;

namespace JK.CommonApi.WebApi.Controllers
{
    /// <summary>
    /// 提现
    /// </summary>
    [RoutePrefix("WithdrawCash")]
    [ApiSessionAuthorize]
    public class WxWithdrawCashController : ApiController
    {
        private IPay _pay;
        private ILog _log;
        public WxWithdrawCashController(IPay pay)
        {
            _pay = pay;
            _log = LogManager.GetLogger(typeof(PayController));
        }

        /// <summary>
        /// 提现
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public ApiResultModel WithdrawCash([FromBody]WithdrawCashViewModel model)
        {
            var mmyUser = (UserModel)HttpContext.Current.User;
            var ip = "127.0.0.1";
            try
            {
                ip = HttpContext.Current.Request.ServerVariables.Get("Remote_Addr").ToString();
            }
            catch (Exception e)
            {
            }
            int money = Convert.ToInt32(model.Amount * 100);
            try
            {
                _pay.PayToUser(mmyUser.UserGuid, mmyUser.OpenId, money, ip);
            }
            catch (Exception e)
            {
                return this.ResultApiError(e.Message);
            }
            return this.ResultApiSuccess();
        }
    }
}
