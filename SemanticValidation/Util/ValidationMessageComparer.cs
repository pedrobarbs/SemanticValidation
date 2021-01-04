using System;
using System.Collections.Generic;

namespace SemanticValidation.Util
{
    internal class ValidationMessageComparer : EqualityComparer<ValidationMessage>
    {
        private readonly IEqualityComparer<string> _c = EqualityComparer<string>.Default;

        public override bool Equals(ValidationMessage first, ValidationMessage second)
        {
            if (first is null && second is null) return true;
            if (first is null && !(second is null)) return false;
            if (!(first is null) && second is null) return false;

            if (first.Property.Equals(second.Property, StringComparison.InvariantCultureIgnoreCase)
                && first.Message.Equals(second.Message, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode(ValidationMessage obj)
        {
            return _c.GetHashCode(obj.Property.ToUpperInvariant() + obj.Message.ToUpperInvariant());
        }
    }
}
