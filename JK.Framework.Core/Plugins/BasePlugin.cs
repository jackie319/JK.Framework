using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Core.Plugins
{
    public abstract class BasePlugin:IPlugin
    {
        public PluginDescriptor PluginDescriptor { get; set; }
        public void Install()
        {
            throw new NotImplementedException();
        }

        public void Uninstall()
        {
            throw new NotImplementedException();
        }
    }
}
