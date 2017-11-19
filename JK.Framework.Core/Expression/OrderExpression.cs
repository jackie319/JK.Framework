using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Core.Expression
{
   public  class OrderExpression
    {
        public static EntityOrderExpression<T> OrderByDescending<T>(Expression<Func<T, object>> expression) where T : class
        {
            return new EntityOrderExpression<T>(expression, ListSortDirection.Descending);
        }

        public static EntityOrderExpression<T> OrderByAscending<T>(Expression<Func<T, object>> expression) where T : class
        {
            return new EntityOrderExpression<T>(expression, ListSortDirection.Ascending);
        }
    }
}
