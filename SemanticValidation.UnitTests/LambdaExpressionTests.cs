using SemanticValidation.Extensions;
using System;
using System.Linq.Expressions;
using Xunit;

namespace SemanticValidation.UnitTests
{
    public class LambdaExpressionTests
    {
        [Fact]
        public void Expression_Equalities()
        {
            Expression<Func<bool>> expression = () => true;

            // GetType().Fullname retorna o tipo da própria expression,
            // enquanto o .Type.Fullname retorna o tipo da func que está envelopada pela Expression.
            Assert.NotEqual(expression.GetType().FullName, expression.Type.FullName);
            Assert.Equal(ExpressionType.Lambda, expression.NodeType);
        }

        [Fact]
        public void Expression_WhenConstantExpression()
        {
            Expression<Func<bool>> expression = () => true;

            var converted = expression.Body as ConstantExpression;

            Assert.NotNull(converted);
            Assert.Equal(ExpressionType.Constant, converted?.NodeType);
        }

        [Fact]
        public void Expression_WhenNotAMemberExpression()
        {
            Expression<Func<bool>> expression = () => true;

            // Exceção lançada pois Expression não é do tipo Unary nem do tipo MemberExpression.
            Assert.Throws<ArgumentException>(() => expression.AsMemberExpression());
            Assert.Null(expression.Body as MemberExpression);
        }

        [Fact]
        public void Expression_WhenMemberExpression()
        {
            var car = new Car() { Brand = "Volvo" };

            Expression<Func<string>> expression = () => car.Brand;
            var converted = expression.Body as MemberExpression;

            // Exceção lançada pois Expression não é do tipo Unary nem do tipo MemberExpression.
            Assert.NotNull(converted);
            Assert.Equal(ExpressionType.MemberAccess, converted?.NodeType);
        }

        [Fact]
        public void Expression_WhenUnaryExpression()
        {
            var car = new Car() { Brand = "Volvo", NumbersOfDoors = 4 };

            Expression<Func<uint>> expression = () => (uint)car.NumbersOfDoors;

            var converted = expression.Body as UnaryExpression;

            // Exceção lançada pois Expression não é do tipo Unary nem do tipo MemberExpression.
            Assert.NotNull(converted);
            Assert.Equal(ExpressionType.Convert, converted?.NodeType);

            // Quando há conversão a expressão NÃO é considerada como MemberExpression
            Assert.Null(expression.Body as MemberExpression);
        }

        [Fact]
        public void Expression_WhenMethodCallExpression()
        {
            var car = new Car() { Brand = "Volvo", NumbersOfDoors = 4 };

            Expression<Func<uint?>> expression = () => GetNumbersOfDoors(car);

            var converted = expression.Body as MethodCallExpression;

            Assert.NotNull(converted);
            Assert.Equal(ExpressionType.Call, converted?.NodeType);
        }

        [Fact]
        public void Expression_WhenInvocationExpression()
        {
            InvocationExpression expression = Expression.Invoke(largeSumTest, Expression.Constant(10), Expression.Constant(999));

            // Necessário converter pois InvocationExpression não é do tipo genérico "Expression<TDelegate>", que por herança, também seria LambdaExpression.
            // Neste caso é necessário converter, explicitamente, a Expression para uma LambdaExpression, a fim de compilar e invocá-la.
            var lambda = Expression.Lambda<Func<bool>>(expression);

            var result = lambda.Compile()();

            Assert.True(result);
            Assert.Equal(ExpressionType.Invoke, expression?.NodeType);
        }

        [Fact]
        public void Expression_WhenParameterExpression()
        {
            ParameterExpression param = Expression.Parameter(typeof(int));

            MethodCallExpression methodCall = Expression.Call(
                typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) })!,
                param
            );

            Expression.Lambda<Action<int>>(
                methodCall,
                new ParameterExpression[] { param }
            ).Compile()(10);
        }

        uint? GetNumbersOfDoors(Car? car)
        {
            return (uint?)car?.NumbersOfDoors;
        }

        string DummyMethod() => string.Empty;

        Expression<Func<int, int, bool>> largeSumTest = (num1, num2) => (num1 + num2) > 1000;
    }
}
