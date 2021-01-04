using SemanticValidation.Util;
using System.Collections.Generic;

namespace SemanticValidation
{
    public partial class Contract
    {
        public Contract()
        {
            Messages = new HashSet<ValidationMessage>(new ValidationMessageComparer());
        }

        public HashSet<ValidationMessage> Messages { get; private set; }

        private void InsertMessage(string property, string message)
        {
            Messages.Add(new ValidationMessage(property, message));
        }

        public Contract Requires() => this;
    }
}
