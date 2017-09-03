using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Extensions
{
    /// <summary>
    /// 搜索算法
    /// </summary>
    public class SearchHelper
    {
        //TODO:待封装成基础方法，实现搜索算法


// 首页搜索 简单实现从右到左逐字匹配
//比如搜索 梳子 （可以是很复杂的一句话）
//1 ：先搜 梳子  找到了返回 找不到则继续搜索 梳
//2： 把包含 梳子  和梳 的所有数据去重然后再按最优解排序（先显示包含梳子的）

//此例为1 的情况。

///// <summary>
///// 首页搜索  简单实现从右到左逐字匹配
///// </summary>
///// <param name="productName"></param>
///// <param name="skip"></param>
///// <param name="take"></param>
///// <param name="total"></param>
///// <returns></returns>
//        public IList<ProductV> HomeSearch(string productName, int skip, int take, out int total)
//        {
//            var status = ProductStatusEnum.OnShelf.ToString();
//            var query = _productVRepository.Table.Where(q => !q.IsDeleted && q.Status.Equals(status));
//            int resultTotal = 0;
//            if (!string.IsNullOrEmpty(productName))
//            {
//                int strLength = productName.Length;
//                for (int i = 0; i < strLength; i++)
//                {
//                    int x = strLength - i;
//                    string searchName = productName.Substring(0, x);
//                    var tryQuery = query;
//                    tryQuery = tryQuery.Where(q => q.SaleTitle.Contains(searchName) || q.CategoryName.Contains(searchName));
//                    resultTotal = tryQuery.Count();
//                    if (resultTotal > 0)
//                    {
//                        query = query.Where(q => q.SaleTitle.Contains(searchName) || q.CategoryName.Contains(searchName));
//                        break;// 此处不返回，则需把所有符合结果的数据去重 再按最优解排序。
//                    };
//                }
//            }
//            else
//            {
//                resultTotal = query.Count();
//            }
//            total = resultTotal;
//            return query.OrderBy(q => q.DisplayOrder)
//                .ThenByDescending(q => q.SoldTotal)
//               .ThenBy(q => q.IsRecommended)
//               .ThenBy(q => q.IsSpecialOffer).
//               ThenByDescending(q => q.TimeCreated).Skip(skip).Take(take).ToList();
//        }




    }
}
