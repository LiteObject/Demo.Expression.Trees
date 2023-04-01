using System.Linq.Expressions;

namespace Demo.DotNetExpressions
{
    public class ParameterReplacerVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression oldParameter;
        private readonly ParameterExpression newParameter;

        public ParameterReplacerVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            this.oldParameter = oldParameter;
            this.newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == oldParameter ? newParameter : base.VisitParameter(node);
        }
    }
}
