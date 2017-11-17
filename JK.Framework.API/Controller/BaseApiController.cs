using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace JK.Framework.API.Controller
{
    public class BaseApiController : ApiController
    {
        private const string ResultTotal = "X-Total-Count";
        private const string SessionKey = "X-SessionKey";
        public static void AppendHeaderTotal(int total)
        {
            System.Web.HttpContext.Current.Response.AppendHeader("Access-Control-Expose-Headers", ResultTotal);
            System.Web.HttpContext.Current.Response.AppendHeader(ResultTotal, Convert.ToString(total));
        }

        public static void AppendHeaderSessionKey(string sessionKey)
        {
            System.Web.HttpContext.Current.Response.AppendHeader("Access-Control-Expose-Headers", SessionKey);
            System.Web.HttpContext.Current.Response.AppendHeader(SessionKey, sessionKey);
        }
    }
}
