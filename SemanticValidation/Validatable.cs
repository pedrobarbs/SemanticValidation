using SemanticValidation.Contracts;
using System;
using System.Linq;

namespace SemanticValidation
{
    public abstract class Validatable<T> where T : class
    {
        private Contract<T> Contract { get; set; }

        public abstract void Validate(Contract<T> contract);

        public bool IsInvalid
        {
            get
            {
                Contract = new Contract<T>();

                Validate(Contract);

                if (Contract is null) 
                {
                    // TODO: Colocar em arquivo separado de mensagens com região.
                    throw new ArgumentNullException(nameof(Contract), "Contract cannot be null.");
                }

                return Contract.IsValid() is false;
            }
        }


        public bool IsValid { get => IsInvalid is false; }
    }
}