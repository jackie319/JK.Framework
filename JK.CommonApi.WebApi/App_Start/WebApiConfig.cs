using JK.Framework.API.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JK.CommonApi.WebApi
{
    public static  class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //跨域 ，支持Delete在web.config
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // Web API 配置和服务

            //全局异常
            config.Filters.Add(new ApiGlobalExceptioinFilter(GlobalException.GlobalExceptionHandler));
            // config.Filters.Add(new ApiSessionAuthorizeAttribute()); //TODO： 放在此处里面的属性 autofac不能注入

            string privateToken = WebConfigurationManager.AppSettings["PrivateToken"];
            config.Filters.Add(new JKApiTokenAuthorizeAttribute(privateToken));
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
