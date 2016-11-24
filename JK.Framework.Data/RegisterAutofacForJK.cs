using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using JK.Framework.Core.Caching;
using JK.Framework.Core.Data;

namespace JK.Framework.Data
{
    public class RegisterAutofacForJK
    {
        //每个项目分别注册autofac（也可以统一管理，避免每个项目都引用autofac）
        public static void RegisterAutofacForJKFramework(ContainerBuilder builder, string connectionStr)
        {
            builder.Register<IDbContext>(c => new JKObjectContext(connectionStr)).InstancePerLifetimeScope();
            //InstancePerDependency对每一个依赖或每一次调用创建一个新的唯一的实例
            //InstancePerLifetimeScope在一个生命周期域中，每一个依赖或调用创建一个单一的共享的实例，且每一个不同的生命周期域，实例是唯一的，不共享的。
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
        }
    }


    //mvc web项目中配置autofac
    //public class MvcApplication : System.Web.HttpApplication
    //{
    //    protected void Application_Start()
    //    {

    //        RegisterAutofac();
    //    }

    //    public static void RegisterAutofac()
    //    {
    //        string connectionStr = System.Web.Configuration.WebConfigurationManager.
    //            ConnectionStrings["AccountEntities"].ConnectionString; ;

    //        ContainerBuilder builder = new ContainerBuilder();
    //        builder.RegisterControllers(Assembly.GetExecutingAssembly());

    //        #region IOC注册区域
    //        //倘若需要默认注册所有的，请这样写（主要参数需要修改）
    //        //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
    //        //   .AsImplementedInterfaces();

    //        //JKFramework
    //        RegisterAutofacForJK.RegisterAutofacForJKFramework(builder, connectionStr);
    //        builder.RegisterType<AccountServiceImpl>().As<IAccountService>().InstancePerHttpRequest(); //mvc


    //        #endregion
    //        // then
    //        var container = builder.Build();
    //        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

    //    }


    //}


}
