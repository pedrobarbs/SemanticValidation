using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SemanticValidation.Util.EqualityComparers
{
    // TODO: Ler, entender e melhorar, principalmente o GetHashCode
    public class ExpressionEqualityComparer : IEqualityComparer<Expression>
    {
        public bool Equals(Expression? x, Expression? y)
        {
            // TODO: fazer null check
            return EqualsRecursive(x, y);
        }

        private bool EqualsRecursive(Expression x, Expression y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x.GetType() != y.GetType() ||
                x.Type != y.Type ||
                x.NodeType != y.NodeType)
            {
                return false;
            }

            return (x, y) switch
            {
                (LambdaExpression lambdaExpressionX, LambdaExpression lambdaExpressionY) =>
                    AreAllArgumentsEqual(lambdaExpressionX.Parameters, lambdaExpressionY.Parameters) && Equals(lambdaExpressionX.Body, lambdaExpressionY.Body),

                (BinaryExpression binaryExpressionX, BinaryExpression binaryExpressionY) =>
                    binaryExpressionX.Method == binaryExpressionY.Method &&
                    Equals(binaryExpressionX.Left, binaryExpressionY.Left) &&
                    Equals(binaryExpressionX.Right, binaryExpressionY.Right),

                (UnaryExpression unaryExpressionX, UnaryExpression unaryExpressionY) =>
                    unaryExpressionX.Method == unaryExpressionY.Method &&
                    Equals(unaryExpressionX.Operand, unaryExpressionY.Operand),

                (MethodCallExpression methodCallExpressionX, MethodCallExpression methodCallExpressionY) =>
                    methodCallExpressionX.Method == methodCallExpressionY.Method &&
                    AreAllArgumentsEqual(methodCallExpressionX.Arguments, methodCallExpressionY.Arguments) &&
                    Equals(methodCallExpressionX.Object, methodCallExpressionY.Object),

                (ConditionalExpression conditionalExpressionX, ConditionalExpression conditionalExpressionY) =>
                    Equals(conditionalExpressionX.Test, conditionalExpressionY.Test) &&
                    Equals(conditionalExpressionX.IfTrue, conditionalExpressionY.IfTrue) &&
                    Equals(conditionalExpressionX.IfFalse, conditionalExpressionY.IfFalse),

                (InvocationExpression invocationExpressionX, InvocationExpression invocationExpressionY) =>
                    AreAllArgumentsEqual(invocationExpressionX.Arguments, invocationExpressionY.Arguments) &&
                    Equals(invocationExpressionX.Expression, invocationExpressionY.Expression),

                (MemberExpression memberExpressionX, MemberExpression memberExpressionY) =>
                    memberExpressionX.Member == memberExpressionY.Member &&
                    Equals(memberExpressionX.Expression, memberExpressionY.Expression),

                (ConstantExpression constantExpressionX, ConstantExpression constantExpressionY) =>
                    constantExpressionX.Value.Equals(constantExpressionY.Value),

                (ParameterExpression parameterExpressionX, ParameterExpression parameterExpressionY) =>
                    parameterExpressionX.Name == parameterExpressionY.Name,

                (NewExpression newExpressionX, NewExpression newExpressionY) =>
                    AreAllArgumentsEqual(newExpressionX.Arguments, newExpressionY.Arguments) &&
                    newExpressionX.Constructor == newExpressionY.Constructor,

                _ => false
            };
        }

        private bool AreAllArgumentsEqual<T>(IEnumerable<T> xArguments, IEnumerable<T> yArguments)
            where T : Expression
        {
            var argumentEnumeratorX = xArguments.GetEnumerator();
            var argumentEnumeratorY = yArguments.GetEnumerator();

            bool haveNotEnumeratedAllOfX = argumentEnumeratorX.MoveNext();
            bool haveNotEnumeratedAllOfY = argumentEnumeratorY.MoveNext();

            bool areAllArgumentsEqual = true;

            while (haveNotEnumeratedAllOfX && haveNotEnumeratedAllOfY && areAllArgumentsEqual)
            {
                areAllArgumentsEqual = Equals(argumentEnumeratorX.Current, argumentEnumeratorY.Current);
                haveNotEnumeratedAllOfX = argumentEnumeratorX.MoveNext();
                haveNotEnumeratedAllOfY = argumentEnumeratorY.MoveNext();
            }

            if (haveNotEnumeratedAllOfX || haveNotEnumeratedAllOfY)
            {
                return false;
            }

            return areAllArgumentsEqual;
        }

        public int GetHashCode(Expression x)
        {
            if (x is LambdaExpression lambdaExpressionX)
            {
                return 
                    HashCode.Combine(
                        AccumulatedHashCodes(lambdaExpressionX.Parameters),
                        GetHashCode(lambdaExpressionX.Body));
            }


            if (x is BinaryExpression binaryExpressionX)
            {
                int methodHashCode = binaryExpressionX.Method != null ? binaryExpressionX.Method.GetHashCode() : binaryExpressionX.NodeType.GetHashCode();
                
                return HashCode.Combine(
                    methodHashCode, 
                    GetHashCode(binaryExpressionX.Left),
                    GetHashCode(binaryExpressionX.Right));
            }


            if (x is UnaryExpression unaryExpressionX)
            {
                int methodHashCode = unaryExpressionX.Method != null ? unaryExpressionX.Method.GetHashCode() : unaryExpressionX.NodeType.GetHashCode();

                return HashCode.Combine(
                    methodHashCode,
                    GetHashCode(unaryExpressionX.Operand));
            }


            if (x is MethodCallExpression methodCallExpressionX)
            {
                return HashCode.Combine(
                    AccumulatedHashCodes(methodCallExpressionX.Arguments),
                    methodCallExpressionX.Method.GetHashCode(),
                    GetHashCode(methodCallExpressionX.Object));
            }


            if (x is ConditionalExpression conditionalExpressionX)
            {
                return
                    HashCode.Combine(
                        GetHashCode(conditionalExpressionX.Test),
                        GetHashCode(conditionalExpressionX.IfTrue),
                        GetHashCode(conditionalExpressionX.IfFalse));
            }


            if (x is InvocationExpression invocationExpressionX)
            {
                return
                    HashCode.Combine(
                        AccumulatedHashCodes(invocationExpressionX.Arguments),
                        GetHashCode(invocationExpressionX.Expression));
            }


            if (x is MemberExpression memberExpressionX)
            {
                return
                    HashCode.Combine(
                        memberExpressionX.Member.GetHashCode(),
                        GetHashCode(memberExpressionX.Expression));
            }


            if (x is ConstantExpression constantExpressionX)
            {
                int valueHash = constantExpressionX.Value != null ? constantExpressionX.Value.GetHashCode() : constantExpressionX.GetHashCode();
                return valueHash;
            }

            if (x is NewExpression newExpressionX)
            {

                return
                    HashCode.Combine(
                        AccumulatedHashCodes(newExpressionX.Arguments),
                        newExpressionX.Constructor.GetHashCode());
            }

            return 0;
        }

        private int AccumulatedHashCodes<T>(IEnumerable<T> expressions)
            where T : Expression
        {
            int accumulatedHashCode = 0;
            foreach (var expression in expressions)
            {
                accumulatedHashCode = HashCode.Combine(GetHashCode(expression), accumulatedHashCode);
            }

            return accumulatedHashCode;
        }
    }
}