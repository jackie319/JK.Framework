using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.API.Model
{
    /// <summary>
    /// 已弃用
    /// </summary>
    public class ApiQueryModel
    {
        /// <summary>
        /// 指定返回记录的开始位置
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// 指定返回记录的数量
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// 指定返回结果按照哪个属性排序
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public string Order { set; get; }
    }

}
