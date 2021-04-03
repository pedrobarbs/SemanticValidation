using SemanticValidation.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SemanticValidation.Contracts
{
    // PARA TIPOS DE REFERENCIA
    // TODO: fazer para tipos primitivos
    public class ClausesSpecification<T> : Clauses where T : class
    {
        protected readonly T Value;

        public ClausesSpecification(string propertyName, T value): base(propertyName)
        {
            Value = value;
        }
    }

    public class Clauses
    {
        public Clauses(string propertyName)
        {
            PropertyName = propertyName;
            Conditions = new HashSet<Condition>();
        }

        internal readonly string PropertyName;
        internal HashSet<Condition> Conditions;
        protected string DefaultMessage;
        internal string Message { get; private set; }


        public void WithMessage(string message)
        {
            Message = message;
        }

        public void WithDefaultMessage()
        {
        }

        protected void BuildCondition(string message, Expression<Func<bool>> conditionExpression)
        {
            // TODO: sobrescrever operador pra comparar expressoes corretamente
            var equal = Conditions.FirstOrDefault(c => c.Expression == conditionExpression);

            if (equal is null is false) 
            {
                equal.ValidationMessage = message;
                return;
            }

            Conditions.Add(new Condition()
            {
                Expression = conditionExpression,
                ValidationMessage = message
            });
        }
    }
}
