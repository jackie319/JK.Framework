using Autofac;
using Autofac.Integration.WebApi;
using JK.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace JK.PictureCenter.WebApi
{
    public class AutoFacRegister
    {
        public static void RegisterAutofacDelegate(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<PictureImpl>().As<IPicture>().InstancePerDependency();

            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
        }
    }
}