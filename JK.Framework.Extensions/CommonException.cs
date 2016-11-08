using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions
{
    public class CommonException : Exception
    {
        public ExcetptionType Type { private set; get; }

        public Dictionary<string, string> Dictionary { set; get; }

        public CommonException(ExcetptionType type)
        {
            Type = type;
        }

        public CommonException(ExcetptionType type, Dictionary<string, string> dictionary)
        {
            Type = type;
            Dictionary = dictionary;
        }
        public CommonException(ExcetptionType type, string messege)
            : base(messege)
        {
            Type = type;
        }

        public CommonException(string message) : base(message)
        {

        }
    }
    /// <summary>
    /// 自定义异常枚举
    /// </summary>
    public enum ExcetptionType
    {
        /// <summary>
        /// 对象为空
        /// </summary>
        NullException,
        /// <summary>
        /// 参数验证异常
        /// </summary>
        ValidationExcption

    }
}
