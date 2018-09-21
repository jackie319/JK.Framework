using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseManage.Container.Update
{

    public class ResultModel
    {
        public VersionModel Result { get; set; }
    }
    public class VersionModel
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string VersionNum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 更新地址
        /// </summary>
        public string UpdateUrl { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
