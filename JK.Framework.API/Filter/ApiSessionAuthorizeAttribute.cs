using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.API.Filter
{
    //public class ApiSessionAuthorizeAttribute : AuthorizeAttribute
    //{
    //    public ICacheManager _cache { get; set; }
    //    public override void OnAuthorization(HttpActionContext filterContext)
    //    {
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
    //            if (!flag) throw new AuthorizeException("无效的sessionkey");
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
    //    耦合了领域模型
    //    private UserModel GetUser(string sessionKey)
    //    {
    //        return _cache.Get<UserModel>(sessionKey);
    //    }
    //}
}
