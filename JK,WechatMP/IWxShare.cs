using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.WechatMP
{
    public interface IWxShare
    {
        JsSdkUiPackage GetJsSdkUiPackage(string url);
    }
}
