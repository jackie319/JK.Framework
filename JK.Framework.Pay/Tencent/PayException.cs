using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Pay.Tencent
{
    public class PayException:Exception
    {
        public PayException(string message)
            : base(message)
        {
        }
    }

}
