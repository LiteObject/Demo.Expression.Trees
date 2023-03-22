using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Expression = System.Linq.Expressions.Expression;

namespace Demo.MyExpression.Trees
{
    public class ToUpperVisitor : ExpressionVisitor
    {
        public override Expression Visit(Expression node)
        {
            if (node.NodeType == ExpressionType.Parameter)
            {
                return base.Visit(node);
            }

            if(node.Type == typeof(string))
            {
                var toUpper = typeof(string).GetMethod("ToUpper", Type.EmptyTypes);
                var methodCallExpression = Expression.Call(node, toUpper);
                return methodCallExpression;
            }

            return base.Visit(node);
        }
    }
}