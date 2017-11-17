using JK.CommonApi.Domain;
using JK.Framework.Core;
using JK.Framework.Core.Caching;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace JK.CommonApi.WebApi.App_Start
{
    public class ApiSessionAuthorizeAttribute: AuthorizeAttribute
    {
        public ICacheManager _cache { get; set; }
        private ILog _log = LogManager.GetLogger(typeof(ApiSessionAuthorizeAttribute));

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            var attributes = filterContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
            bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
            if (isAnonymous)
            {
                base.OnAuthorization(filterContext);
                return;
            }
            string sessionkey = string.Empty;
            if (filterContext.Request.Headers.Contains("sessionkey"))
            {
                try
                {
                    sessionkey = HttpUtility.UrlDecode(filterContext.Request.Headers.GetValues("sessionkey").FirstOrDefault());
                }
                catch (ArgumentException)
                {
                }
                if (string.IsNullOrEmpty(sessionkey))
                {
                    throw new AuthorizeException("缺少参数sessionkey");
                }

                var flag = SessionKeyIsExist(sessionkey);
                if (!flag)
                {
                    throw new AuthorizeException("无效的sessionkey");
                }

                HttpContext.Current.User = GetUser(sessionkey);

                base.OnAuthorization(filterContext);
            }
            else
            {
                throw new AuthorizeException("缺少参数sessionkey");
            }

        }

        private bool SessionKeyIsExist(string sessionKey)
        {
            return _cache.IsSet(sessionKey);
        }
        private UserModel GetUser(string sessionKey)
        {
            return _cache.Get<UserModel>(sessionKey);
        }
    }
}