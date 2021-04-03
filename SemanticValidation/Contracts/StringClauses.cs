using SemanticValidation.Contracts;
using SemanticValidation.Extensions;
using System;
using System.Linq.Expressions;

namespace SemanticValidation
{
    public class StringClauses : ClausesSpecification<string>
    {
        public StringClauses(string propertyName, string value) : base(propertyName, value)
        {
        }

        public StringClauses Cannot_Be__Null_Empty_WhiteSpace()
        {
            BuildCondition(
                conditionExpression: () => string.IsNullOrWhiteSpace(Value) == false,
                message: $"Property {PropertyName} cannot be null, empty or whitespace. Current value: {Value} ({Value.Categorize()})");

            return this;
        }

        public StringClauses Must_Have__Maximum_Length_Of_(int maximumLength)
        {
            BuildCondition(
                conditionExpression: CheckOnlyIfHasValue(() => Value.Length <= maximumLength),
                message: $"Property {PropertyName} must have a maximum length of {maximumLength}");

            return this;
        }

        public Expression<Func<bool>> CheckOnlyIfHasValue(Func<bool> func) 
        {
            return () => string.IsNullOrEmpty(Value) || func();
        }
    }
}
