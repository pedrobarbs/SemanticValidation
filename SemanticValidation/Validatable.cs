using System;
using System.Collections.Generic;

namespace SemanticValidation
{
    public abstract class Validatable<T> where T : class
    {
        private Contract<T> Contract { get; set; }

        public abstract void Validate(Contract<T> contract);

        private void Validate()
        {
            Contract = new Contract<T>();

            Validate(Contract);

            if (Contract is null)
            {
                // TODO: Colocar em arquivo separado de mensagens com região.
                throw new ArgumentNullException(nameof(Contract), "Contract cannot be null.");
            }
        }

        public bool IsValid { get => IsInvalid is false; }

        public bool IsInvalid
        {
            get
            {
                Validate();
                return Contract.IsValid() is false;
            }
        }

        public List<ValidationMessage> ValidationMessages
        {
            get
            {
                Validate();
                return Contract.ValidationMessages;
            }
        }
    }
}