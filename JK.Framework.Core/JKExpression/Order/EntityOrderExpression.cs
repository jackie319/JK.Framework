using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Core
{
    public class EntityOrderExpression<T> : IDisposable
       where T : class
    {
        private readonly List<OrderExpressionStruct> _OrderExpressionList;

        public EntityOrderExpression(Expression<Func<T, object>> expression, ListSortDirection direction)
        {
            _OrderExpressionList = new List<OrderExpressionStruct>();
            var propertyName = GetPropertyName(expression);
            _OrderExpressionList.Add(
                new OrderExpressionStruct
                {
                    PropertyName = propertyName,
                    Direction = direction
                });
        }
        ~EntityOrderExpression()
        {
            _OrderExpressionList.Clear();
        }

        /// <summary>执行与释放或重置非托管资源关联的应用程序定义的任务。</summary>
        public void Dispose()
        {
            _OrderExpressionList.Clear();
        }

        public EntityOrderExpression<T> ThenOrderByAscending(Expression<Func<T, object>> expression)
        {
            var propertyName = GetPropertyName(expression);
            _OrderExpressionList.Add(
                new OrderExpressionStruct
                {
                    PropertyName = propertyName,
                    Direction = ListSortDirection.Ascending
                });
            return this;
        }

        public EntityOrderExpression<T> ThenOrderByDescending(Expression<Func<T, object>> expression)
        {
            var propertyName = GetPropertyName(expression);
            _OrderExpressionList.Add(
                new OrderExpressionStruct
                {
                    PropertyName = propertyName,
                    Direction = ListSortDirection.Descending
                });
            return this;
        }

        public IEnumerable<OrderExpressionStruct> ToList()
        {
            //TODO: Clone
            return new List<OrderExpressionStruct>(_OrderExpressionList);
        }

        private string GetPropertyName(Expression<Func<T, object>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                // 再次查一元表达式操作数
                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                }
            }

            if (memberExpression != null)
            {
                return memberExpression.Member.Name;
            }

            throw new NotSupportedException(nameof(GetPropertyName));
        }
    }
}
