using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DotNetExpressions
{
    public static class Extensions
    {
        public static Expression ReplaceParameter(this Expression expression, ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            return new ParameterReplacerVisitor(oldParameter, newParameter).Visit(expression);
        }
    }
}
