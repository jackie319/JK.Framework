using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions.Draw
{
    /// <summary>
    /// 集合乱序
    /// </summary>
    public static class OutOfOrder
    {
        /// <summary>
        /// 将集合乱序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IList<T> ToOutOfOrder<T>(this IList<T> list)
        {
            Random r = new Random();
            //随机交换 list.Count  * 2次，平均约2 % 的对象还在原来的位置
            for (int i = 0; i < list.Count * 2; i++)
            {
                int index1 = r.Next(list.Count);
                int index2 = r.Next(list.Count);
                var temp = list[index1];
                list[index1] = list[index2];
                list[index2] = temp;
            }
            return list;
        }
    }
}
