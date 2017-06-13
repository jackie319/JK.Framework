using JK.Framework.API.Model;
using JK.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;

namespace JK.Framework.API.Filter
{
    public class ApiGlobalExceptioinFilter : ExceptionFilterAttribute
    {

        public delegate ApiResultModel ExceptionHandlerDelegate(HttpActionExecutedContext actionExecutedContext);
        public ExceptionHandlerDelegate ExceptionHandler { get; }

        public bool AllowMultiple => throw new NotImplementedException();

        public ApiGlobalExceptioinFilter(ExceptionHandlerDelegate exceptionHandler)
        {
            ExceptionHandler = exceptionHandler;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var url = actionExecutedContext.Request.RequestUri.ToString();
            var exception = actionExecutedContext.Exception;
            LogTool.ErrorRecord("捕获全局异常：", exception.Message, url, "", exception.StackTrace);

            var errorHandledResult = ExceptionHandler(actionExecutedContext);
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            response.Content = new StringContent(errorHandledResult.ErrorMsg);
            actionExecutedContext.Response = response;
            base.OnException(actionExecutedContext);
        }

    }
}
