using System;
using System.Linq.Expressions;

namespace SemanticValidation.Extensions
{
    public static class ExpressionExtensions
    {
        public static T GetValue<T>(this MemberExpression expression) 
        {
            var objectMember = Expression.Convert(expression, typeof(T));

            var getterLambda = Expression.Lambda<Func<T>>(objectMember);

            var getter = getterLambda.Compile();

            return getter();
        }

        public static MemberExpression AsMemberExpression<T, U>(this Expression<Func<T, U>> expression)
        {
            return (expression.Body as MemberExpression);
        }

        public static (string, T) ExtractNameAndValue<T>(this MemberExpression expression) 
        {
            return (expression.Member.Name, expression.GetValue<T>());
        }
    }
}
