using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions
{
    public static class OrderNo
    {
        /// <summary>
        /// 种子精确到百纳秒级别
        /// </summary>
        /// <returns></returns>
        public static string CreateOrderNo()
        {
            string date = DateTime.Now.ToString("yyMMddHHmmss");
            //种子精确到百纳秒级别
            int ram = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0)).Next(100000, 999999);
            return string.Format(date + ram);
        }
    }
}
