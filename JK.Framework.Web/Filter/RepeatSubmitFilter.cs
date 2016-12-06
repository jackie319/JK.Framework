using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JK.Framework.Web.Filter
{
    public class RepeatSubmitFilter : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// 禁止重复提交
        /// 废弃：Token由客户端生成，无安全性。
        /// 应该由服务器生成并加密传递给客户端，
        /// 服务器接收到后解密匹配。
        /// （存入session）
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string method = filterContext.RequestContext.HttpContext.Request.HttpMethod;
            if (method.Equals("POST"))
            {
                string hiddenTokenName = "hiddenToken";
                string hiddenToken = filterContext.HttpContext.Request[hiddenTokenName];
                if (string.IsNullOrWhiteSpace(hiddenToken)) throw new Exception("Token不能为空");

                if (System.Web.HttpContext.Current.Cache[hiddenTokenName] == null)
                {
                    System.Web.HttpContext.Current.Cache.Insert(hiddenTokenName, hiddenToken, null, DateTime.MaxValue, TimeSpan.FromSeconds(10));
                }
                else
                {
                    throw new Exception("不要重复提交");
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}
