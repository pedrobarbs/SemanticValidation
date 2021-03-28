using System;

namespace SemanticValidation.Contracts
{
    // PARA TIPOS DE REFERENCIA
    // TODO: fazer para tipos primitivos
    public class ClauseSpecification<T> : ClauseSpecification where T : class
    {
        protected readonly T Value;

        public ClauseSpecification(string propertyName, T value): base(propertyName)
        {
            Value = value;
        }
    }

    public class ClauseSpecification
    {
        public ClauseSpecification(string propertyName)
        {
            PropertyName = propertyName;
        }

        protected readonly string PropertyName;
        internal Func<bool> Condition;
        protected string DefaultMessage;
        protected string Message { get; private set; }


        public void WithMessage(string message)
        {
            Message = message;
        }

        public void WithDefaultMessage()
        {
        }
    }
}
