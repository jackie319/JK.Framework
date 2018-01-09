using JK.CommonApi.WebApi.App_Start;
using JK.Framework.API;
using JK.JKUserAccount;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JK.CommonApi.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterAutofac();
            InitLog4Net();
            GetAppSetting();
        }

        public static void RegisterAutofac()
        {
            string connectionStr = System.Web.Configuration.WebConfigurationManager.
                ConnectionStrings["JKDataEntities"].ConnectionString;

            RegisterApiAutofacForJK.RegisterApi(connectionStr, AutoFacRegister.RegisterAutofacDelegate);
        }

        private static void InitLog4Net()
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
        }

        public void GetAppSetting()
        {
            string pictureUrl = WebConfigurationManager.AppSettings["PictureUrl"];
            string wechatPayNotifyUrl = WebConfigurationManager.AppSettings["WechatPayNotifyUrl"];
            string sessionTimeExpired = WebConfigurationManager.AppSettings["SessionTimeExpired"];
            AppSetting.Instance().PictureUrl = pictureUrl;
            AppSetting.Instance().WechatPayNotifyUrl = wechatPayNotifyUrl;
            AppSetting.Instance().SessionTimeExpired = Convert.ToInt32(sessionTimeExpired);
        }

        /// <summary>
        /// 全局异常额外处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_error(object sender, EventArgs e)
        {
            HttpException error = (HttpException)Server.GetLastError();
            var logger = LogManager.GetLogger(typeof(WebApiConfig));
            logger.Error($"Application_error: {error.GetHttpCode()};{error.Message}");
        }
    }
}
