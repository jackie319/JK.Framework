using JK.Framework.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace JK.JKService
{
    public partial class P2bService : ServiceBase
    {
        public P2bService()
        {
            InitializeComponent();
        }
        public static Autofac.IContainer _container;
        private Statistics _St { get; set; }
       // public IEarnings Earning;
        protected override void OnStart(string[] args)
        {
            //启用调试
            Debugger.Launch();
            RegisterAutofac();
           // Earning = _container.Resolve<IEarnings>();   //根据需要加载具体的业务
            _St = new Statistics(this);
            _St.BeginStatistics();
        }

        public static void RegisterAutofac()
        {
            string connectionStr = System.Web.Configuration.WebConfigurationManager.
                ConnectionStrings["P2BPlatFormEntities"].ConnectionString;

            RegisterAutofacForJK.Register(connectionStr, AutoFacRegister.RegisterAutofacDelegate);
            _container = RegisterAutofacForJK._container;
        }

        protected override void OnStop()
        {
            //服务结束执行代码
        }

        protected override void OnPause()
        {

            //服务暂停执行代码

            base.OnPause();
        }

        protected override void OnContinue()
        {

            //服务恢复执行代码

            base.OnContinue();

        }

        protected override void OnShutdown()
        {
            //系统即将关闭执行代码

            base.OnShutdown();

        }
    }
}
