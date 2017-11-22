using JK.JKUserAccount.ServiceModel;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.WechatMP
{
    public class WxShareImpl : IWxShare
    {
        private UnifiedOrderSetting _setting;
        public WxShareImpl()
        {
            _setting = new UnifiedOrderSetting();
        }
        public JsSdkUiPackage GetJsSdkUiPackage(string url)
        {
            return JSSDKHelper.GetJsSdkUiPackage(_setting.AppId, _setting.AppSecret, url);
        }
    }
}
