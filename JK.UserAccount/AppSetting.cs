using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.JKUserAccount
{
    public class AppSetting
    {
        private static AppSetting _AppSetting;

        private AppSetting()
        {
        }

        public static AppSetting Instance()
        {
            if (_AppSetting == null) _AppSetting = new AppSetting();
            return _AppSetting;
        }

        public string PictureUrl { get; set; }

        public string ZipPath { get; set; }

        public string QrLogoPath { get; set; }

        public string WechatPayNotifyUrl { get; set; }

        public int SessionTimeExpired { get; set; }
        public string AlipayPublicKey { get; set; }
    }
}
