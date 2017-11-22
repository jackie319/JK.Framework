using JK.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Sms
{
   public  interface ISms
    {
        void SendCode(string phone, SmsTypeEnum type, string remark);
        SmsRecords FindRecord(string phone, SmsTypeEnum smsType);
        void Validate(Guid recordGuid);
        void PayNotify(IList<string> moblieList, IList<string> paramList);
    }
}
