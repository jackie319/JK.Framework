using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JK.Framework.Web.Filter
{
    /// <summary>
    /// 权限filter
    /// </summary>
    public class AuthorityWebFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = string.Empty;
            try
            {
                controllerName = filterContext.RouteData.Values["Controller"].ToString();
            }
            catch (Exception e)
            {
            }
            string notFilterController = "Account|Error|Home|WebClientConfig|";

            //配合权限模块
            //if (!controllerName.IsNullOrWhiteSpace() && !notFilterController.Contains(controllerName))
            //{
            //    bool flag = false;

            //    var currentUse = UserHelper.GetCurrentUser<WebUser>();
            //    foreach (var item in currentUse.ActionUrl)
            //    {
            //        if (item.Contains(controllerName))
            //        {
            //            flag = true;
            //        }
            //    }
            //    if (!flag) throw new AuthorityException("当前用户没有操作权限");

            //}

            base.OnActionExecuting(filterContext);
        }


    }


    #region 自定义User实现IPrincipal
    //public class WebUser : WebUserBase
    //{
    //}

    //public abstract class WebUserBase : UserBase, IWebUser { }

    //public abstract class UserBase : IPrincipal, IUser { }
    #endregion

    #region 登录成功后将自定义User 赋给 HttpContext.User（IPrincipal）
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
