using System.Linq.Expressions;
using Expression = System.Linq.Expressions.Expression;

namespace Demo.MyExpression.Trees
{
    /// <summary>
    /// This programs contains simple examples to demostrate the Example class.
    /// </summary>
    internal static class Program
    {
        static void Main(string[] args)
        {
            // Example 1: Example of an expression.
            Expression<Func<int, bool>> isNegativeExp = i => i < 0;
            Console.WriteLine(isNegativeExp.Body);

            /*
             * Type: Func<int, bool>
             * Expression: i < 0
             * Lambda Operator: =>
             * Parameter: Left of =>
             * Body Expression: Right of =>
             * Lambda Expression: i => i < 0
             */

            var isNegativeFunc = isNegativeExp.Compile();
            Console.WriteLine(isNegativeFunc(-1));

            // Let's build the "isNegativeExp" expression

            ConstantExpression constantExpression = Expression.Constant(0, typeof(int));
            //Console.WriteLine(constantExpression.NodeType);
            //Console.WriteLine(constantExpression.Type);
            //Console.WriteLine(constantExpression.Value);

            ParameterExpression parameterExpression = Expression.Parameter(typeof(int), "i");
            //Console.WriteLine(parameterExpression.NodeType);
            //Console.WriteLine(parameterExpression.Type);
            //Console.WriteLine(parameterExpression.Name);

            BinaryExpression binaryExpression = Expression.GreaterThan(constantExpression, parameterExpression);
            //Console.WriteLine(binaryExpression.NodeType);
            //Console.WriteLine(binaryExpression.Type);
            //Console.WriteLine(binaryExpression.Left);
            //Console.WriteLine(binaryExpression.Right);

            // Example 2: Identical to Example 1
            Expression<Func<int, bool>> isNegativeExp2 = System.Linq.Expressions.Expression.Lambda<Func<int, bool>>(binaryExpression, parameterExpression);
            Console.WriteLine(isNegativeExp2.Body);
            Func<int, bool> isNegativeFunc2 = isNegativeExp2.Compile();
            Console.WriteLine(isNegativeFunc2(4));

            // The block expression allows for executing several expressions sequentually
            // When the block expression is executed, it returns the value of the last expression in the sequence
            BlockExpression blockExpression = Expression.Block(
                Expression.Call(null, typeof(Console).GetMethod("Write", new Type[] { typeof(string) }), Expression.Constant("Hello ")),
                Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), Expression.Constant("World!")),
                Expression.Constant(123)
                );

            // The following statement first creates an expression tree, then compiles it, and then executes it.
            var result = Expression.Lambda<Func<int>>(blockExpression).Compile()();
            Console.WriteLine(result);

            // Print out the expressions from the block expression.
            foreach (var expr in blockExpression.Expressions)
            {
                Console.WriteLine(expr.ToString());
            }
        }
    }
}