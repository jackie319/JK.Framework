using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace JK.Framework.API.Filter
{
    /// <summary>
    /// 身份认证filter
    /// </summary>
    public class JKApiAuthorizeAttribute : AuthorizeAttribute
    {
        #region 判断 IIdentity.IsAuthenticated 是否通过

        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            //bool flag = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            //if (flag)
            //{
            //    return;
            //}
            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    throw new AuthorizeException("用户未通过认证");
            //}
            base.OnAuthorization(filterContext);
        }

        #endregion
    }


 

}
