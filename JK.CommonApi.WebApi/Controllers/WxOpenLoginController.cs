using JK.CommonApi.Domain;
using JK.CommonApi.WebApi.Models.UserAccount;
using JK.Data.Model;
using JK.Framework.API.Controller;
using JK.Framework.API.Model;
using JK.Framework.Core;
using JK.Framework.Core.Caching;
using JK.JKUserAccount;
using JK.JKUserAccount.IServices;
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
    /// /// <summary>
    /// 微信登录（开放平台，网站）
    /// Appid 和 Appsecret 和公众平台不一样
    /// 用户openId 和公众号不一样
    /// </summary>
    [RoutePrefix("WxOpenLogin")]
    public class WxOpenLoginController : ApiController
    {
        private IUserAccount _userAccount;
        private ICacheManager _cache;
        private ILog _log;
        public WxOpenLoginController(IUserAccount userAccount, ICacheManager cache)
        {
            _userAccount = userAccount;
            _cache = cache;
            _log = LogManager.GetLogger(typeof(WxLoginController));
        }
        [HttpPost]
        [Route("")]
        public ApiResultModel Login([FromBody]WxLoginViewModel model)
        {
            try
            {
                var account = _userAccount.WechatOpenLogin(model.Code, model.UserGuid);
                string openId = string.Empty;
                var usreAccountWechat = _userAccount.FindUserAccountWechat(account.Guid);
                if (usreAccountWechat != null)
                {
                    openId = usreAccountWechat.WechatOpenId;
                }
                UserModel userModel = new UserModel(account, account.NickName, true, openId) { };
                string sessionKey = SessionManager.GetSessionKey(userModel.UserGuid, "CommonApi");
                _cache.Remove(sessionKey);
                _cache.SetSliding(sessionKey, userModel, AppSetting.Instance().SessionTimeExpired);
                _userAccount.UpdateSessionKey(userModel.UserGuid, sessionKey);
                var total = _cache.Total();

                //TODO:考虑做成异步
                try
                {
                    var ip = "127.0.0.1";
                    try
                    {
                        ip = HttpContext.Current.Request.ServerVariables.Get("Remote_Addr").ToString();
                    }
                    catch (Exception e)
                    {
                    }
                    string userAgent = HttpContext.Current.Request.UserAgent;
                    var record = new UserLoginRecords();
                    record.Channel = UserLoginSourceEnum.Wechat.ToString();
                    record.UserGuid = userModel.UserGuid;
                    record.IP = ip;
                    record.UserAgent = userAgent;
                    record.SessionTotal = total;
                    _userAccount.UserLoginRecord(record);
                    _log.Info("已登录数：" + total.ToString());
                }
                catch (Exception ex)
                {

                }


                BaseApiController.AppendHeaderSessionKey(sessionKey);
            }
            catch (ArgumentException ar)
            {
                return this.ResultApiError("获取微信用户信息失败");
            }
            catch (CommonException ex)
            {
                return this.ResultApiError(ex.Message);
            }
            return this.ResultApiSuccess();
        }

    }
}
