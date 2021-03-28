using SemanticValidation.Contracts;
using SemanticValidation.Enums;
using SemanticValidation.Extensions;
using System;

namespace SemanticValidation
{
    // TODO: colocar generics
    public class StringClauseSpecification : ClauseSpecification<string>
    {


        public StringClauseSpecification(string propertyName, string value) : base(propertyName, value)
        {
        }

        // Marketing do nome é bom
        // Marketing do detalhe da informação é ótimo
        public ClauseSpecification<string> Cannot_Be__Null_Empty_WhiteSpace()
        {
            DefaultMessage = $"Property {PropertyName} cannot be null, empty or whitespace. Current value: {Value} ({Value.Categorize()})";
            Condition = () => string.IsNullOrWhiteSpace(Value) is false;

            return this;
        }
    }
}
