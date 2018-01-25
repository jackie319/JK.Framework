using Autofac;
using JK.JKUserAccount.IServices;
using JK.JKUserAccount.ServicesImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Test
{
    public class AutoFacRegister
    {
        public static void RegisterAutofacDelegate(ContainerBuilder builder)
        {

     
            builder.RegisterType<UserAccountImpl>().As<IUserAccount>().InstancePerDependency();
    
        }
    }
}
