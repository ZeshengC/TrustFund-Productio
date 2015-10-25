using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Utils
{
    public static class PropertySupport
    {
        public static string ExtractPropertyName<T>(Expression<Func<T>> expression)
        {
            var body = expression.Body as MemberExpression;
            if(body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }
            return body.Member.Name;
        }
    }
}
