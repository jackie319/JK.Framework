using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Logistics.kdniao
{
    public class SearchResult
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string EBusinessID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 快递公司编码
        /// </summary>
        public string ShipperCode { get; set; }
        /// <summary>
        /// 物流运单号
        /// </summary>
        public string LogisticCode { get; set; }
        /// <summary>
        /// 成功与否
        /// </summary>
        public Boolean Success { get; set; }
        /// <summary>
        /// 失败原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 流状态：2-在途中,3-签收,4-问题件
        /// </summary>
        public string State { get; set; }

        public IList<Trace> Traces { get; set; }
    }

    public class Trace
    {
        /// <summary>
        /// 时间
        /// </summary>
        public string AcceptTime { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string AcceptStation { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
