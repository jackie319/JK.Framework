using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin;
using Senparc.Weixin.MP.TenPayLibV3;

namespace JK.Framework.Pay.Tencent
{
    public static class UnifiedOrder
    {
        public static UnifiedorderResult Pay(TenPayV3UnifiedorderRequestData dataInfo, int timeOut = Config.TIME_OUT)
        {
            return TenPayV3.Unifiedorder(dataInfo, timeOut);
        }
    }
}
