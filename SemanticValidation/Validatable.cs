using System;
using System.Linq;

namespace SemanticValidation
{
    public abstract class Validatable
    {
        private ClauseSpecification Contract { get; set; }

        public abstract void Validate(Contract contract);

        public bool IsInvalid
        {
            get
            {
                Contract = new ClauseSpecification();

                Validate(Contract);

                if (Contract is null) 
                {
                    // TODO: Colocar em arquivo separado de mensagens com região.
                    throw new ArgumentNullException(nameof(Contract), "Contract cannot be null.");
                }

                return Contract.Messages.Any();
            }
        }


        public bool IsValid { get => !IsInvalid; }
    }
}