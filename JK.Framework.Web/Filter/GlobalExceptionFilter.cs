using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using JK.Framework.Core;
using JK.Framework.Extensions;
using JK.Framework.Web.Model;

namespace JK.Framework.Web.Filter
{
    /// <summary>
    /// 全局异常处理filter
    /// </summary>
    public class GlobalExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public string ErrorHandlerController { get; }

        public string ErrorHandlerAction { get; }

        public delegate ResultModel ExceptionHandlerDelegate(ExceptionContext exceptionFilterContext);
        public ExceptionHandlerDelegate ExceptionHandler { get; }

        public GlobalExceptionFilter(ExceptionHandlerDelegate exceptionHandler,string errorHandlerController="Error", string errorHandlerAction="Index")
        {
            ExceptionHandler = exceptionHandler;
            ErrorHandlerController = errorHandlerController;
            ErrorHandlerAction = errorHandlerAction;

        }
        public void OnException(ExceptionContext filterContext)
        {
            var url = filterContext.RequestContext.HttpContext.Request.RawUrl;
            var exception = filterContext.Exception;
            LogTool.ErrorRecord("捕获全局异常：", exception.Message, url, "", exception.StackTrace);

            var errorHandledResult = ExceptionHandler(filterContext);
            var accepts = filterContext.RequestContext.HttpContext.Request.AcceptTypes;
            if (accepts != null && accepts.Any(a => a.Equals("application/json", StringComparison.OrdinalIgnoreCase)))
            {
                filterContext.Result = errorHandledResult.ToJsonResultModel();
            }
            else
            {
                filterContext.Controller.ViewData.Model = errorHandledResult;
                if (errorHandledResult.ExceptionType == JKExceptionType.NoAuthorized)
                {
                    filterContext.Result = new RedirectResult(errorHandledResult.RedirectUrl);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(GetRouteValueDictionary());
                }
             
            }
            filterContext.ExceptionHandled = true;
        }



        public RouteValueDictionary GetRouteValueDictionary()
        {
            var routerValues = new RouteValueDictionary();

          //  routerValues.Add("Area", ErrorHandelerArea);

            if (!string.IsNullOrWhiteSpace(ErrorHandlerController))
            {
                routerValues.Add("Controller", ErrorHandlerController);
            }

            if (!string.IsNullOrWhiteSpace(ErrorHandlerAction))
            {
                routerValues.Add("Action", ErrorHandlerAction);
            }
            return routerValues;
        }

        //项目自处理异常委托
        //FilterConfig partial 类中
        //private static ResultModel GlobalExceptionHandler(ExceptionContext exceptionfiltercontext)
        //{
        //    var url = exceptionfiltercontext.RequestContext.HttpContext.Request.RawUrl;
        //    var exception = exceptionfiltercontext.Exception;
        //    while (exception.InnerException != null)
        //    {
        //        exception = exception.InnerException;
        //    }

        //    var logger = LogManager.GetLogger(typeof(FilterConfig));
        //    logger.Error("出错了！错误信息：" + exception.Message + "访问路径：" + url + "堆栈：" + exception.StackTrace);
        //    var result = new ResultModel(false, exception.Message, 0, url, null) { };
        //    return result;
        //}
         //以及
        //filters.Add(new GlobalExceptionFilter(GlobalExceptionHandler, "Error", "Index"));
    }
}
