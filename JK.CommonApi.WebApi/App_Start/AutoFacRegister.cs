using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace JK.CommonApi.WebApi.App_Start
{
    public class AutoFacRegister
    {
        public static void RegisterAutofacDelegate(ContainerBuilder builder)
        {
            //webapi
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //mvc
             builder.RegisterControllers(Assembly.GetExecutingAssembly());//通知类Controller是MVC
            //业务类
            //builder.RegisterType<TaskImpl>().As<ITask>().InstancePerDependency();

            //webapi Filter
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
        }
    }
}