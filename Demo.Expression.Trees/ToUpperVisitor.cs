using System.Linq.Expressions;

namespace Demo.DotNetExpressions
{
    public class ToUpperVisitor : ExpressionVisitor
    {
        public override Expression Visit(Expression node)
        {
            if (node.NodeType == ExpressionType.Parameter)
            {
                return base.Visit(node);
            }

            if (node.Type == typeof(string))
            {
                System.Reflection.MethodInfo? toUpper = typeof(string).GetMethod("ToUpper", Type.EmptyTypes);
                MethodCallExpression methodCallExpression = Expression.Call(node, toUpper);
                return methodCallExpression;
            }

            return base.Visit(node);
        }
    }
}