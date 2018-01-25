using Autofac;
using JK.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Test
{
    public class Initial
    {
        public static IContainer _container;
        public static void RegisterAutofac()
        {
            string connectionStr = System.Web.Configuration.WebConfigurationManager.
                ConnectionStrings["MMYEntities"].ConnectionString;

            RegisterAutofacForJK.Register(connectionStr, AutoFacRegister.RegisterAutofacDelegate);
            _container = RegisterAutofacForJK._container;

        }

    }
}
