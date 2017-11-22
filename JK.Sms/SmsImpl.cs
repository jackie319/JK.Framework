using JK.Data.Model;
using JK.Framework.Core;
using JK.Framework.Core.Data;
using JK.Framework.Sms.Netease;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Sms
{
    public class SmsImpl:ISms
    {
        private IRepository<SmsRecords> _SmsRecordsRepository;

        private const string Appkey = "34a5fc42e8d6bf0e9a3817a1af8e901a";
        private const string AppSecret = "7e68c58203ed";
        private const int RegisteCodeTemplateid = 3050311;
        private const int NotifyCodeTemplateid = 3061875;
        private SmsCode smsCode;
        private ILog _log;
        public SmsImpl(IRepository<SmsRecords> smsRecordsRepository)
        {
            _SmsRecordsRepository = smsRecordsRepository;
            smsCode = new SmsCode(Appkey, AppSecret);
            _log = LogManager.GetLogger(typeof(SmsImpl));
        }

        private SmsRecords AddRecord(string phone, SmsTypeEnum type, string remark)
        {
            SmsRecords record = new SmsRecords();
            record.Guid = Guid.NewGuid();
            record.RadomCode = string.Empty;
            record.Remark = remark;
            record.Phone = phone;
            record.IsValidated = false;
            record.SmsType = type.ToString();
            record.ResultStatusCode = string.Empty;
            record.TimeCreated = DateTime.Now;
            record.TimeUpdate = DateTime.MaxValue;
            _SmsRecordsRepository.Insert(record);
            return record;
        }

        private void UpdateRecord(Guid recordGuid, string code, string obj, string msg)
        {
            var entity = _SmsRecordsRepository.Table.FirstOrDefault(q => q.Guid == recordGuid);
            entity.RadomCode = obj;
            entity.ResultStatusCode = code + ":" + msg;
            entity.TimeUpdate = DateTime.Now;
            _SmsRecordsRepository.Update(entity);
        }

        public void Validate(Guid recordGuid)
        {
            var entity = _SmsRecordsRepository.Table.FirstOrDefault(q => q.Guid == recordGuid);
            if (entity != null)
            {
                entity.IsValidated = true;
                entity.TimeUpdate = DateTime.Now;
                _SmsRecordsRepository.Update(entity);
            }

        }

        public SmsRecords FindRecord(string phone, SmsTypeEnum smsType)
        {
            var type = smsType.ToString();
            var list = _SmsRecordsRepository.Table.Where(q => !q.IsValidated && q.SmsType.Equals(type) && q.Phone.Equals(phone)).ToList();
            var entity = list.OrderByDescending(q => q.TimeCreated).FirstOrDefault();
            if (entity != null)
            {
                if ((DateTime.Now - entity.TimeUpdate).TotalMinutes > 10)
                {
                    throw new CommonException("验证码已过期");
                }
            }

            return entity;
        }

        /// <summary>
        /// 发送验证码短信
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="type"></param>
        /// <param name="remark"></param>
        public void SendCode(string phone, SmsTypeEnum type, string remark)
        {
            var record = AddRecord(phone, type, remark);
            var model = smsCode.SendRegisteCode(phone, RegisteCodeTemplateid);
            UpdateRecord(record.Guid, model.code, model.obj ?? string.Empty, model.msg ?? string.Empty);
            if (!model.code.Equals("200"))
            {
                _log.Error("发送验证码短信返回错误：" + model.code + ":" + model.msg ?? string.Empty);
            }
        }

        /// <summary>
        /// 订单通知短信
        /// </summary>
        /// <param name="moblieList"></param>
        /// <param name="paramList"></param>
        public void PayNotify(IList<string> moblieList, IList<string> paramList)
        {
            IList<SmsRecords> smsList = new List<SmsRecords>();
            foreach (var item in moblieList)
            {
                var record = AddRecord(item, SmsTypeEnum.Notify, "订单通知");
                smsList.Add(record);
            }
            var model = smsCode.SendNotifyCode(moblieList, NotifyCodeTemplateid, paramList);
            _log.Info("发送订单通知短信返回：" + model.code + ":" + model.msg ?? string.Empty);
            foreach (var item in smsList)
            {
                UpdateRecord(item.Guid, model.code, model.obj ?? string.Empty, model.msg ?? string.Empty);
            }
            if (!model.code.Equals("200"))
            {
                _log.Error("发送订单通知短信返回错误：" + model.code + ":" + model.msg ?? string.Empty);
            }
        }

    }
}
