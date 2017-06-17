using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web;

namespace JK.Framework.API.Filter
{
    /// <summary>
    /// 身份认证filter
    /// </summary>
    public class JKApiAuthorizeAttribute : AuthorizeAttribute
    {
        #region 判断 IIdentity.IsAuthenticated 是否通过

        /// <summary>
        /// 暂时简单处理授权
        /// </summary>
        private string[] _PrivateToken = new string[] { "1k23k1231asdfa8923" , "23k2340jsdf0923" };
        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            string token = string.Empty;
            //bool flag = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            //if (flag)
            //{
            //    return;
            //}
            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    throw new AuthorizeException("用户未通过认证");
            //}

            if (filterContext.Request.Headers.Contains("token"))
            {
                token = HttpUtility.UrlDecode(filterContext.Request.Headers.GetValues("token").FirstOrDefault());
            }
            else
            {
                throw new CommonException("无效的token");
            }
            base.OnAuthorization(filterContext);
        }

        #endregion
    }


 

}
