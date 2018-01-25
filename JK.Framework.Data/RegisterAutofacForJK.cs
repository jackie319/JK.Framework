using Autofac;
using JK.Framework.Core.Caching;
using JK.Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Data
{
    /// <summary>
    /// 单元测试或windows 服务等注册autofac
    /// </summary>
    public class RegisterAutofacForJK
    {
        public static IContainer _container;

        public delegate void RegisterAutofacDelegate(ContainerBuilder builder);

        public static void Register(string connectionStr, RegisterAutofacDelegate registerAutofacDelegate)
        {
            ContainerBuilder builder = new ContainerBuilder();
            // builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

            //  HttpConfiguration config =GlobalConfiguration.Configuration;

            builder.Register<IDbContext>(c => new JKObjectContext(connectionStr)).InstancePerDependency();
            //InstancePerDependency对每一个依赖或每一次调用创建一个新的唯一的实例
            //InstancePerLifetimeScope在一个生命周期域中，每一个依赖或调用创建一个单一的共享的实例，且每一个不同的生命周期域，实例是唯一的，不共享的。
            //泛型注入
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
            registerAutofacDelegate(builder);
            //TODO:也可通过反射找到IDependencyRegistrar的实现类并调用方法

            // then
            _container = builder.Build();

            //注册api容器需要使用HttpConfiguration对象
            //   config.DependencyResolver = new AutofacWebApiDependencyResolver(_container);

            //DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));//支持MVC.通知的Controller是MVC

        }
    }
}
