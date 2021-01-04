using SemanticValidation.Enums;
using SemanticValidation.Extensions;
using System;

namespace SemanticValidation
{
    // TODO: colocar generics
    public class StringClauseSpecification : ClauseSpecification<string>
    {
        private readonly string _value;
        private readonly string _propertyName;

        public StringClauseSpecification(string propertyName, string value)
        {
            _value = value;
            _propertyName = propertyName;
        }

        // Marketing do nome é bom
        // Marketing do detalhe da informação é ótimo
        public IClauseBuilder Cannot_Be__Null_Empty_WhiteSpace()
        {
            var defaultMessage = $"Property {_propertyName} cannot be null, empty or whitespace. Current value: {_value} ({_value.Categorize()})";

            return new ClauseBuilder(() => string.IsNullOrWhiteSpace(_value), defaultMessage);
        }
    }
}
