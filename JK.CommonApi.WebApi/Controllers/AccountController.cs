using JK.Authority.IServices;
using JK.CommonApi.Domain;
using JK.CommonApi.WebApi.App_Start;
using JK.CommonApi.WebApi.Models.UserAccount;
using JK.Data.Model;
using JK.Framework.API.Controller;
using JK.Framework.API.Filter;
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
    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {

        private IUserAccount _userAccount;
        private IAuthority _authority;
        private ISms _sms;
        private ICacheManager _cache;
        private ILog _log;
        public AccountController(IUserAccount userAccount, IAuthority authority, ISms sms, ICacheManager cache)
        {
            _userAccount = userAccount;
            _authority = authority;
            _sms = sms;
            _cache = cache;
            _log = LogManager.GetLogger(typeof(AccountController));
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ApiValidationFilter]
        public ApiResultModel SubmitLogin([FromBody]LogInViewModel model)
        {
            try
            {
                var account = _userAccount.Login(model.UserName, model.PasswordMd5);
                string openId = string.Empty;
                var usreAccountWechat = _userAccount.FindUserAccountWechat(account.Guid);
                if (usreAccountWechat != null)
                {
                    openId = usreAccountWechat.WechatOpenId;
                }
                UserModel userModel = new UserModel(account, account.NickName, true, openId) { };
                string sessionKey = SessionManager.GetSessionKey(userModel.UserGuid,"CommonApi");
                _cache.Remove(sessionKey);
                _cache.SetSliding(sessionKey, userModel, AppSetting.Instance().SessionTimeExpired);
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
                    record.Channel = UserLoginSourceEnum.MH5.ToString();
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
            catch (CommonException)
            {
                return this.ResultApiError("用户名或密码错误");
            }
            return this.ResultApiSuccess();
        }

        /// <summary>
        /// 发送注册验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Route("sendcode")]
        [HttpGet]
        public ApiResultModel SendCode(string phone)
        {
            _sms.SendCode(phone, SmsTypeEnum.Registe, "注册验证码");
            return this.ResultApiSuccess();
        }


        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        [ApiValidationFilter]
        public ApiResultModel Register([FromBody]RegisterViewModel model)
        {
            try
            {
                var entity = _sms.FindRecord(model.MobilePhone, SmsTypeEnum.Registe);
                if (entity == null) return this.ResultApiError("验证码错误");
                if (!model.SmsCode.Equals(entity.RadomCode))
                {
                    return this.ResultApiError("验证码错误");
                }
                _userAccount.Register(model.MobilePhone, model.PasswordMd5, model.SmsCode);
                _sms.Validate(entity.Guid);
            }
            catch (CommonException ex)
            {
                return this.ResultApiError(ex.Message);
            }
            return this.ResultApiSuccess();
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("ChangePwd")]
        [ApiSessionAuthorize]
        [HttpPost]
        [ApiValidationFilter]
        public ApiResultModel ChangePassword(ChangePasswordViewModel model)
        {
            if (!model.NewPasswordMd5Confirm.Equals(model.NewPasswordMd5))
                return this.ResultApiError("俩次输入的密码不匹配");
            try
            {
                var mmyUser = (UserModel)HttpContext.Current.User;
                _userAccount.ChangePwd(mmyUser.UserName, model.OldPasswordMd5, model.NewPasswordMd5);
            }
            catch (CommonException ex)
            {
                return this.ResultApiError(ex.Message);
            }
            return this.ResultApiSuccess();
        }

        /// <summary>
        /// 发送找回密码验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Route("sendcodeBack")]
        [HttpGet]
        public ApiResultModel SendCodeBack(string phone)
        {
            _sms.SendCode(phone, SmsTypeEnum.GetBackPwd, "找回密码验证码");
            return this.ResultApiSuccess();
        }

        /// <summary>
        /// 找回密码(通过手机验证码重设密码)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("GetBackPassword")]
        [HttpPost]
        [ApiValidationFilter]
        public ApiResultModel GetBackPassword([FromBody]GetBackPasswrodViewModel model)
        {
            try
            {
                var entity = _sms.FindRecord(model.MobilePhone, SmsTypeEnum.GetBackPwd);
                if (entity == null) return this.ResultApiError("验证码错误");
                if (!model.SmsCode.Equals(entity.RadomCode))
                {
                    return this.ResultApiError("验证码错误");
                }
                _userAccount.GetBackPassword(model.MobilePhone, model.PasswordMd5, model.SmsCode);
                _sms.Validate(entity.Guid);
            }
            catch (CommonException ex)
            {
                return this.ResultApiError(ex.Message);
            }
            return this.ResultApiSuccess();
        }


        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [Route("logout")]
        [ApiSessionAuthorize]
        [HttpPost]
        public ApiResultModel Logout()
        {
            //已经经过滤器进来，sessionkey一定有值
            var sessionkey = Request.Headers.GetValues("sessionkey").FirstOrDefault();
            _cache.Remove(sessionkey);
            return this.ResultApiSuccess();
        }
    }
}
