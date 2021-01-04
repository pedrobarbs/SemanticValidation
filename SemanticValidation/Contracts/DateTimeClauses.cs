using System;

namespace SemanticValidation
{
    // TODO: colocar generics
    public class DateTimeClauseSpecification : ClauseSpecification<DateTime>
    {
        private readonly DateTime? _value;
        private readonly string _propertyName;

        public DateTimeClauseSpecification(string propertyName, DateTime? value)
        {
            _value = value;
            _propertyName = propertyName;
        }

        public IClauseBuilder Must_Be__GreaterThan_(DateTime comparator)
        {
            var defaultMessage = $"Property \"{_propertyName}\" must be greater than {comparator}. Current value: {_value}";

            return new ClauseBuilder(() => _value > comparator, defaultMessage);
        }
    }

    public class ClauseSpecification<T> { }
}
