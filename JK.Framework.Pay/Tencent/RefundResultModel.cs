using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Pay.Tencent
{
   public  class RefundResultModel
    {
        public string ReturnCode { set; get; }

        public string ReturnMsg { get; set; }

        public string ResultCode { get; set; }

        public string ErrCode { get; set; }

        public string ErrCodeDes { get; set; }

    }
}
