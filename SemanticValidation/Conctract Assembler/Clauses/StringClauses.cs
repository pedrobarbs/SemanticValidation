using SemanticValidation.Extensions;
using System;
using System.Linq.Expressions;

namespace SemanticValidation.Contract_Assembler.Clauses
{
    public class StringClauses : Clause<string>
    {
        public StringClauses(string propertyName, string? value) : base(propertyName, value)
        { }

        public StringClauses Must_Not_Be___Null_Empty_WhiteSpace()
        {
            BuildCondition(
                conditionExpression: () => string.IsNullOrWhiteSpace(Value) == false,
                message: $"Property {PropertyName} cannot be null, empty or whitespace. Current value: {Value} ({Value.Categorize()})");

            return this;
        }

        public StringClauses Must_Have___Maximum_Length_Of_(int maximumLength)
        {
            BuildCondition(
                conditionExpression: CheckOnlyIfHasValue(() => Value.Length <= maximumLength),
                message: $"Property {PropertyName} must have a maximum length of {maximumLength}");

            return this;
        }

        #region Helpers
        private Expression<Func<bool>> CheckOnlyIfHasValue(Func<bool> func)
            => () => string.IsNullOrEmpty(Value) || func();
        #endregion
    }
}
