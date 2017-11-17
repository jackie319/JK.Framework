using Autofac;
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
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterType<TaskImpl>().As<ITask>().InstancePerDependency();

            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
        }
    }
}