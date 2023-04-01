using System;
using System.Linq.Expressions;

namespace Demo.DotNetExpressions
{
    /// <summary>
    /// This programs contains simple examples to demostrate the Example class.
    /// </summary>
    internal static class Program
    {
        static void Main()
        {
            List<Person> people = new()
            {
                new Person { Name = "One", Age = 1 },
                new Person { Name = "One", Age = 11 },
                new Person { Name = "Two", Age = 2 },
                new Person { Name = "Two", Age = 22 },
            };

            Func<Person, bool> predicate = BuildPredicate<Person>(
                p => p.Age > 1,
                p => p.Name == "One"
            );

            IEnumerable<Person> filtered = people.Where(predicate);

            foreach (Person p in filtered)
            {
                Console.WriteLine($"{p.Name} is {p.Age} year(s) old.");
            }
        }

        /// <summary>
        /// This method takes a variable number of Expression<Func<T, bool>> conditions and 
        /// returns a compiled delegate that can be used to filter a collection of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <returns>A compiled delegate that can be used to filter a collection of type T</returns>
        public static Func<T, bool> BuildPredicate<T>(params Expression<Func<T, bool>>[] conditions)
        {
            // ParameterExpression is to represent the input object
            ParameterExpression param = Expression.Parameter(typeof(T), "entity");
            Expression? body = null;

            // loop through each condition to build up the predicate
            foreach (Expression<Func<T, bool>> condition in conditions)
            {
                // ReplaceParameter extension method to replace the parameter of each condition
                // with the ParameterExpression created earlier, and then combine the resulting
                // expressions using AndAlso.
                Expression conditionBody = condition.Body.ReplaceParameter(condition.Parameters[0], param);
                body = body == null ? conditionBody : Expression.AndAlso(body, conditionBody);
            }

            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(body, param);
            return lambda.Compile();
        }
    }

    public class Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}