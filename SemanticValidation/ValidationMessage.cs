using System;

namespace SemanticValidation
{
    public class ValidationMessage : IEquatable<ValidationMessage>
    {
        public ValidationMessage(string property, string message)
        {
            Property = property;
            Message = message;
        }

        public string Property { get; set; }
        public string Message { get; set; }

        public bool Equals(ValidationMessage other)
        {
            if (other is null) return false;

            if (other.Message.Equals(Message, StringComparison.InvariantCultureIgnoreCase) &&
                other.Property.Equals(Property, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
