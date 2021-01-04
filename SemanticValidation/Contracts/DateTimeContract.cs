using System;

namespace SemanticValidation
{
    public partial class Contract
    {
        public Contract MustBeGreaterThan(DateTime value, DateTime comparator, string propertyName, string message = null)
        {
            if (string.IsNullOrWhiteSpace(message)) 
            {
                message = $"Property {propertyName} has value equals to {value}, but it has to be greater than {comparator}";
            }

            if (value > comparator)
                InsertMessage(propertyName, message);

            return this;
        }
    }
}
