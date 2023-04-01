using System.Linq.Expressions;

namespace Demo.DotNetExpressions
{
    internal class Practice1
    {
        static void Main()
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

            Func<int, bool> isNegativeFunc = isNegativeExp.Compile();
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
            Expression<Func<int, bool>> isNegativeExp2 = Expression.Lambda<Func<int, bool>>(binaryExpression, parameterExpression);
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
            int result = Expression.Lambda<Func<int>>(blockExpression).Compile()();
            Console.WriteLine(result);

            // Print out the expressions from the block expression.
            foreach (Expression expr in blockExpression.Expressions)
            {
                Console.WriteLine(expr.ToString());
            }

            Expression<Func<string, string>> magicSring = s => s + " belongs to me";
            ToUpperVisitor toUpperVisitor = new();
            Expression<Func<string, string>> expressed = toUpperVisitor.VisitAndConvert(magicSring, null);

            object? what = expressed.Compile().DynamicInvoke("the cheese");
            Console.WriteLine(what);
        }
    }
}
