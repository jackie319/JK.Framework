using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JK.Framework.Web.Filter
{
    /// <summary>
    /// 身份认证filter
    /// </summary>
    public class JKAuthorizeAttribute : AuthorizeAttribute
    {
        #region 判断 IIdentity.IsAuthenticated 是否通过

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //do some sthing
            }
            base.OnAuthorization(filterContext);
        }

        #endregion
    }


    #region 自定义User实现IPrincipal
    //public class WebUser : WebUserBase
    //{
    //}

    //public abstract class WebUserBase : UserBase, IWebUser { }

    //public abstract class UserBase : IPrincipal, IUser { }
    #endregion

    #region 登录成功后将自定义User 赋给 HttpContext.User（IPrincipal）,并设置IIdentity.IsAuthenticated 设置为true（认证通过）
    //public ActionResult SubmitLogin()
    //{

    //    HttpContext.User = webUser;

    //    return Json(new { success = true });
    //}

    #endregion


    #region 获取自定义User
    //public class UserHelper
    //{
    //    public static T GetCurrentUser<T>() where T : WebUser
    //    {
    //        if (HttpContext.Current.User != null && HttpContext.Current.User is WebUser)
    //            return (T)HttpContext.Current.User;

    //        throw new InvalidOperationException();
    //    }
    //}
    #endregion
}
