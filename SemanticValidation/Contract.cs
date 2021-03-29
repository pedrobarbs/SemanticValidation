using SemanticValidation.Contracts;
using SemanticValidation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SemanticValidation
{
    public class Contract<T> where T : class
    {
        public Contract()
        {
            Clauses = new List<Clause>();
            ClauseSpecifications = new List<ClauseSpecification>();
        }

        public List<Clause> Clauses { get; private set; }
        public List<ClauseSpecification> ClauseSpecifications { get; private set; }

        // TODO: colocar generics
        private dynamic Property<U>(Expression<Func<U>> expression)
        {
            var (name, type, value) = expression.AsMemberExpression().ExtractNameTypeAndValue<U>();

            dynamic clauseSpec = GetTypeSpecificSpec(name, type, value);

            var casted = (ClauseSpecification)clauseSpec;

            // TODO: verificar se não existe duplicado na lista
            ClauseSpecifications.Add(casted);

            return clauseSpec;
        }

        private static dynamic GetTypeSpecificSpec<U>(string name, Type type, U value)
        {
            return type switch
            {
                Type stringType when stringType == typeof(string) => new StringClauseSpecification(name, (string)(object)value),

                _ => throw new NotImplementedException($"O tipo {type.Name} ainda não possui suporte no framework"),
            };
        }

        public StringClauseSpecification Property(Expression<Func<string>> expression)
            => (StringClauseSpecification)Property<string>(expression);


        public bool IsValid()
        {
            foreach (var clause in ClauseSpecifications)
            {
                if (clause.Condition() is false)
                {
                    return false;
                }
            }

            return true;
        }

        public List<ValidationMessage> ValidationMessages
        {
            get
            {
                var messages = new List<ValidationMessage>();

                foreach (var clause in ClauseSpecifications)
                {
                    if (clause.Condition() is false)
                    {
                        messages.Add(new ValidationMessage(clause.PropertyName, clause.Message));
                    }
                }

                return messages;
            }
        }
    }
}
