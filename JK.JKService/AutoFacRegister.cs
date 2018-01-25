using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.JKService
{
    public class AutoFacRegister
    {
        public static void RegisterAutofacDelegate(ContainerBuilder builder)
        {

            //builder.RegisterType<EarningImpl>().As<IEarnings>().InstancePerDependency();
            //builder.RegisterType<ProjectImpl>().As<IProject>().InstancePerDependency();
            //builder.RegisterType<OrderImpl>().As<IOrder>().InstancePerDependency();
            //builder.RegisterType<UserAccountImpl>().As<IUserAccount>().InstancePerDependency();
            //builder.RegisterType<SettingImpl>().As<IAppSetting>().InstancePerDependency();
        }
    }
}
