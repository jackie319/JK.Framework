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
    /// 暂时简单处理Token授权,待改
    /// </summary>
    public class JKApiTokenAuthorizeAttribute : AuthorizeAttribute
    {
        private string _PrivateToken { get; }
        public JKApiTokenAuthorizeAttribute(string privateToken)
        {
            _PrivateToken = privateToken;
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            string token = string.Empty;

            if (filterContext.Request.Headers.Contains("token"))
            {
                try {
                    token = HttpUtility.UrlDecode(filterContext.Request.Headers.GetValues("token").FirstOrDefault());
                } catch (ArgumentException)
                {
                }
                
                if (string.IsNullOrEmpty(token) || !token.Equals(_PrivateToken))
                {
                    throw new CommonException("无效的token");
                }
            }
            else
            {
                throw new CommonException("缺少参数token");
            }
            base.OnAuthorization(filterContext);
        }

    }




}
