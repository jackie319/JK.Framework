using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.API.Filter
{
    //public class ApiSessionAuthorizeAttribute : AuthorizeAttribute
    //{
    //public ICacheManager _cache { get; set; }  //属性注入
    //public IUserAccount _userAccount { get; set; } //属性注入
    //private ILog _log = LogManager.GetLogger(typeof(ApiSessionAuthorizeAttribute));

    //前端项目
    //需要登录的Action才加 ApiSessionAuthorizeAttribute 标记

    //public override void OnAuthorization(HttpActionContext filterContext)
    //{
    //    string sessionkey = string.Empty;
    //    if (filterContext.Request.Headers.Contains("sessionkey"))
    //    {
    //        try
    //        {
    //            sessionkey = HttpUtility.UrlDecode(filterContext.Request.Headers.GetValues("sessionkey").FirstOrDefault());
    //        }
    //        catch (ArgumentException)
    //        {
    //        }
    //        if (string.IsNullOrEmpty(sessionkey))
    //        {
    //            throw new AuthorizeException("缺少参数sessionkey");
    //        }

    //        var flag = SessionKeyIsExist(sessionkey);
    //        if (!flag)
    //        {
    //            lock (this)
    //            {
    //                string userAgent = filterContext.Request.Headers.UserAgent.ToString();
    //                if (userAgent.ToLower().Contains("micromessenger"))
    //                {
    //                    var userAccountWechat = _userAccount.FindUserAccount(sessionkey);
    //                    if (userAccountWechat == null)
    //                    {
    //                        throw new AuthorizeException("无效的sessionkey");
    //                    }

    //                    Guid userGuid = userAccountWechat.UserAccountGuid;
    //                    var account = _userAccount.FindUserAccount(userGuid);
    //                    if (account == null)
    //                    {
    //                        throw new AuthorizeException("无效的sessionkey");
    //                    }
    //                    string openId = userAccountWechat.WechatOpenId;

    //                    UserModel userModel = new UserModel(account, account.NickName, true, openId) { };
    //                    _log.Info("session 来了=================");
    //                    if (!SessionKeyIsExist(sessionkey))
    //                    {
    //                        _cache.SetSliding(sessionkey, userModel, AppSetting.Instance().SessionTimeExpired);
    //                        var total = _cache.Total();
    //                        //TODO:考虑做成异步
    //                        try
    //                        {
    //                            var ip = "127.0.0.1";
    //                            try
    //                            {
    //                                ip = HttpContext.Current.Request.ServerVariables.Get("Remote_Addr").ToString();
    //                            }
    //                            catch (Exception e)
    //                            {
    //                            }
    //                            var record = new UserLoginRecords();
    //                            record.Channel = UserLoginSourceEnum.Wechat.ToString();
    //                            record.UserGuid = userModel.UserGuid;
    //                            record.IP = ip;
    //                            record.UserAgent = userAgent;
    //                            record.SessionTotal = total;
    //                            _userAccount.UserLoginRecord(record);
    //                            _log.Info("已登录数(ApiSession)：" + total.ToString());
    //                        }
    //                        catch (Exception ex)
    //                        {

    //                        }

    //                    }
    //                }
    //                else
    //                {
    //                    throw new AuthorizeException("无效的sessionkey");
    //                }

    //            }

    //        }

    //        HttpContext.Current.User = GetUser(sessionkey);

    //        base.OnAuthorization(filterContext);
    //    }
    //    else
    //    {
    //        throw new AuthorizeException("缺少参数sessionkey");
    //    }

    //}

    //private bool SessionKeyIsExist(string sessionKey)
    //{
    //    return _cache.IsSet(sessionKey);
    //}
    //private UserModel GetUser(string sessionKey)
    //{
    //    return _cache.Get<UserModel>(sessionKey);
    //}

    //}

    //后台项目 
    //webApiConfig 统一加入ApiSessionAuthorizeAttribute 标记，所以要排除登录的action
    //或排除加了AllowAnonymousAttribute 标记的action

    //public class ApiSessionAuthorizeAttribute : AuthorizeAttribute
    //{
    //    public ICacheManager _cache { get; set; }
    //    public IUserAccount _userAccount { get; set; }
    //    private ILog _log = LogManager.GetLogger(typeof(ApiSessionAuthorizeAttribute));

    //    public override void OnAuthorization(HttpActionContext filterContext)
    //    {
    //        var attributes = filterContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
    //        bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
    //        if (isAnonymous)
    //        {
    //            base.OnAuthorization(filterContext);
    //            return;
    //        }
    //        string sessionkey = string.Empty;
    //        if (filterContext.Request.Headers.Contains("sessionkey"))
    //        {
    //            try
    //            {
    //                sessionkey = HttpUtility.UrlDecode(filterContext.Request.Headers.GetValues("sessionkey").FirstOrDefault());
    //            }
    //            catch (ArgumentException)
    //            {
    //            }
    //            if (string.IsNullOrEmpty(sessionkey))
    //            {
    //                throw new AuthorizeException("缺少参数sessionkey");
    //            }

    //            var flag = SessionKeyIsExist(sessionkey);
    //            if (!flag)
    //            {
    //                throw new AuthorizeException("无效的sessionkey");
    //            }

    //            HttpContext.Current.User = GetUser(sessionkey);

    //            base.OnAuthorization(filterContext);
    //        }
    //        else
    //        {
    //            throw new AuthorizeException("缺少参数sessionkey");
    //        }

    //    }

    //    private bool SessionKeyIsExist(string sessionKey)
    //    {
    //        return _cache.IsSet(sessionKey);
    //    }
    //    private UserModel GetUser(string sessionKey)
    //    {
    //        return _cache.Get<UserModel>(sessionKey);
    //    }
    //}


}
