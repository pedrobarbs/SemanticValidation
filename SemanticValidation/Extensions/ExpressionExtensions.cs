using System;
using System.Linq.Expressions;
using System.Reflection;

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

        public static MemberExpression AsMemberExpression<U>(this Expression<Func<U>> expression)
        {
            if (expression.Body is not MemberExpression member)
            {
                // The property access might be getting converted to object to match the func
                // If so, get the operand and see if that's a member expression
                member = (expression.Body as UnaryExpression)?.Operand as MemberExpression;
            }
            if (member == null)
            {
                throw new ArgumentException("Action must be a member expression.");
            }
            return member;
        }

        public static (string, Type, T) ExtractNameTypeAndValue<T>(this MemberExpression expression)
        {
            expression.ThrowIfNull(nameof(expression));

            var value = ExpressionUtilities.GetValueUsingCompile(expression);
            //return (expression.Member.Name, typeof(T), expression.GetValue2<T>());

            // TODO: Parametrizar por injeção dependencia se quer nome completo ou não
            var fullName = $"{expression.Member?.DeclaringType?.Name}.{expression.Member?.Name}";

            return (fullName, typeof(T), (T)value);
        }
    }
}
