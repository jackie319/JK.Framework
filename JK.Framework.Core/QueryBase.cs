using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace JK.Framework.Core
{
    public class QueryBase
    {
      
        private int skip;
        private int take = 20;

        /// <summary>
        /// 指定跳过条目数
        /// </summary>
        public int Skip
        {
            get {
                if (skip > 0)
                {
                    return skip;
                }
                else
                {
                    return 0;
                }
            }
            set { skip = value; }
        }

        /// <summary>
        /// 指定返回记录的数量
        /// </summary>
        public int Take
        {
            get
            {
                if (take < 0 || take > 1000)
                {
                    return 20;
                }
                else
                {
                    return take;
                }
            }
            set { take = value; }
        }


    }
}
