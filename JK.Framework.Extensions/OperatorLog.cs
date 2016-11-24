using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions
{
   public class OperatorLog
    {

    }

//    表对应的model：
//    public partial class OperateRecord
//    {
//        public System.Guid Guid { get; set; }
//        public System.Guid OperatorGuid { get; set; }
//        public string OperatorNickName { get; set; }
//        public string Action { get; set; }
//        public string ObjectInfo { get; set; }
//        public string Detail { get; set; }
//        public System.DateTime OperateTime { get; set; }
//    }

//=======================================================================


///// <summary>
//       /// 记录操作日志
//       /// </summary>
//       /// <param name="operGuid">操作者guid</param>
//       /// <param name="operatorNickName">冗余操作者当时的昵称</param>
//       /// <param name="objectInfo">操作的动作(修改学校)</param>
//       /// <param name="operateEnum">操作对象的信息（板桥小学）</param>
//       /// <param name="detail">具体操作详情。（把板桥小学的扣费金额从100元改为120元）</param>
//       /// <exception cref="PACSValidationException">PACSValidationException</exception>
//       public void RecordOperateHistory(Guid operGuid, string operatorNickName, OperateEnum operateEnum, string objectInfo, string detail)
//    {
//        Dictionary<string, string> dictionary = new Dictionary<string, string>();
//        if (operGuid == Guid.Empty) dictionary.Add("perGuid", "操作者guid必填");
//        if (string.IsNullOrEmpty(operatorNickName)) dictionary.Add("operatorNickName", "操作者昵称必填");
//        if (string.IsNullOrEmpty(objectInfo)) dictionary.Add("objectInfo", "操作对象信息必填");
//        if (string.IsNullOrEmpty(detail)) dictionary.Add("detail", "具体操作详情必填");
//        if (dictionary.Count > 0) throw new PACSValidationException(dictionary);
//        IMP.PACS.OperateRecord record = new IMP.PACS.OperateRecord();
//        record.OperatorGuid = operGuid;
//        record.OperatorNickName = operatorNickName;
//        record.Action = operateEnum.ToString();
//        record.Detail = detail;
//        OperateManager.CreateOperateRecord(record);
//    }
//    /// <summary>
//    /// 记录操作日志   
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="operGuid">操作者guid</param>
//    /// <param name="operatorNickName">冗余操作者当时的昵称</param>
//    /// <param name="operateEnum">操作的动作(修改学校)</param>
//    /// <param name="objectInfo">操作对象的信息（板桥小学）</param>
//    /// <param name="operModelOld">修改前对象</param>
//    /// <param name="operModelNew">修改后对象</param>
//    /// <exception cref="PACSValidationException">PACSValidationException</exception>
//    public void RecordOperateHistory<T>(Guid operGuid, string operatorNickName, OperateEnum operateEnum, string objectInfo, T operModelOld, T operModelNew)
//    {
//        Type tOld = operModelOld.GetType();
//        Type tNew = operModelNew.GetType();
//        StringBuilder sb = new StringBuilder("");
//        foreach (var item in tOld.GetProperties())
//        {
//            var nameOld = item.Name;
//            var valueOld = item.GetValue(operModelOld).ToString();
//            if (nameOld.Equals("TimeCreated")) continue;
//            foreach (var itemNew in tNew.GetProperties())
//            {
//                var nameNew = itemNew.Name;
//                var valueNew = itemNew.GetValue(operModelNew).ToString();
//                if (nameNew.Equals("TimeCreated")) continue;
//                if (nameOld.Equals(nameNew))
//                {
//                    if (!valueOld.Equals(valueNew))
//                    {
//                        sb.Append(string.Format("[{0}]的值从[{1}]改为[{2}];", nameOld, valueOld, valueNew));
//                    }
//                }
//            }
//        }
//        RecordOperateHistory(operGuid, operatorNickName, operateEnum, objectInfo, sb.ToString());
//    }
//} 


//=============================进入框架==============

// /// <summary>
//       /// 记录操作日志   
//       /// </summary>
//       /// <typeparam name="T"></typeparam>
//       /// <param name="operateEnum">操作的动作(修改学校)</param>
//       /// <param name="objectInfo">操作对象的信息（板桥小学）</param>
//       /// <param name="operModelOld">修改前对象</param>
//       /// <param name="operModelNew">修改后对象</param>
//       /// <exception cref="PACSValidationException">PACSValidationException</exception>
//       internal void RecordOperateHistory<T>(OperateEnum operateEnum, string objectInfo, T operModelOld, T operModelNew)
//{
//    Type tOld = operModelOld.GetType();
//    Type tNew = operModelNew.GetType();
//    StringBuilder sb = new StringBuilder("");
//    foreach (var item in tOld.GetProperties())
//    {
//        var nameOld = item.Name;
//        var valueOld = item.GetValue(operModelOld).ToString();
//        if (nameOld.Equals("TimeCreated")) continue;
//        foreach (var itemNew in tNew.GetProperties())
//        {
//            var nameNew = itemNew.Name;
//            var valueNew = itemNew.GetValue(operModelNew).ToString();
//            if (nameNew.Equals("TimeCreated")) continue;
//            if (nameOld.Equals(nameNew))
//            {
//                if (!valueOld.Equals(valueNew))
//                {
//                    sb.Append(string.Format("[{0}]的值从[{1}]改为[{2}];", nameOld, valueOld, valueNew));
//                }
//            }
//        }
//    }
//    RecordOperateHistory(operateEnum, objectInfo, sb.ToString());
//}
///// <summary>
///// 记录操作日志
///// </summary>
///// <param name="objectInfo">操作的动作(修改学校)</param>
///// <param name="operateEnum">操作对象的信息（板桥小学）</param>
///// <param name="detail">具体操作详情。（把板桥小学的扣费金额从100元改为120元）</param>
///// <exception cref="PACSValidationException">PACSValidationException</exception>
//internal void RecordOperateHistory(OperateEnum operateEnum, string objectInfo, string detail)
//{
//    Dictionary<string, string> dictionary = new Dictionary<string, string>();
//    if (string.IsNullOrEmpty(objectInfo)) dictionary.Add("objectInfo", "操作对象信息必填");
//    if (string.IsNullOrEmpty(detail)) detail = string.Empty;
//    if (dictionary.Count > 0) throw new PACSValidationException(dictionary);
//    IMP.PACS.OperateRecord record = new IMP.PACS.OperateRecord();
//    record.OperatorGuid = AdminGuid;
//    record.OperatorNickName = AdminNickName;
//    record.Action = operateEnum.ToString();
//    record.Detail = detail;
//    record.ObjectInfo = objectInfo;
//    _Server.OperateManager.CreateOperateRecord(record);
//}
}
