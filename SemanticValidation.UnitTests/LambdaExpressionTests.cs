using SemanticValidation.Extensions;
using System;
using System.Linq.Expressions;
using Xunit;

namespace SemanticValidation.UnitTests
{
    public class LambdaExpressionTests
    {
        [Fact]
        public void LambdaExpression_Equalities()
        {
            Expression<Func<bool>> expression = () => true;

            // GetType().Fullname retorna o tipo da própria expression,
            // enquanto o .Type.Fullname retorna o tipo da func que está envelopada pela Expression.
            Assert.NotEqual(expression.GetType().FullName, expression.Type.FullName);
            Assert.Equal(ExpressionType.Lambda, expression.NodeType);
        }

        [Fact]
        public void LambdaExpression_WhenConstantExpression()
        {
            Expression<Func<bool>> expression = () => true;

            var converted = expression.Body as ConstantExpression;

            Assert.NotNull(converted);
            Assert.Equal(ExpressionType.Constant, converted?.NodeType);
        }

        [Fact]
        public void LambdaExpression_WhenNotAMemberExpression()
        {
            Expression<Func<bool>> expression = () => true;

            // Exceção lançada pois Expression não é do tipo Unary nem do tipo MemberExpression.
            Assert.Throws<ArgumentException>(() => expression.AsMemberExpression());
            Assert.Null(expression.Body as MemberExpression);
        }

        [Fact]
        public void LambdaExpression_WhenMemberExpression()
        {
            var car = new Car() { Brand = "Volvo" };

            Expression<Func<string>> expression = () => car.Brand;
            var converted = expression.Body as MemberExpression;

            // Exceção lançada pois Expression não é do tipo Unary nem do tipo MemberExpression.
            Assert.NotNull(converted);
            Assert.Equal(ExpressionType.MemberAccess, converted?.NodeType);
        }

        [Fact]
        public void LambdaExpression_WhenUnaryExpression()
        {
            var car = new Car() { Brand = "Volvo" };

#pragma warning disable IDE0004
            Expression<Func<object>> expression = () => (object)car.Brand;
#pragma warning restore IDE0004

            var converted = expression.Body as UnaryExpression;

            // Exceção lançada pois Expression não é do tipo Unary nem do tipo MemberExpression.
            Assert.NotNull(converted);
            Assert.Equal(ExpressionType.Convert, converted?.NodeType);

            // Quando há conversão a expressão NÃO é considerada como MemberExpression
            Assert.Null(expression.Body as MemberExpression);
        }
    }
}
