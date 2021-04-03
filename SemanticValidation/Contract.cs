using SemanticValidation.Contracts;
using SemanticValidation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SemanticValidation
{
    public class Contract<T> where T : class
    {
        public Contract()
        {
            //Clauses = new List<Clause>();
            Clauses = new HashSet<Clauses>();
        }

        //public List<Clause> Clauses { get; private set; }
        public HashSet<Clauses> Clauses { get; private set; }

        private dynamic Property<U>(Expression<Func<U>> expression)
        {
            var (name, type, value) = expression.AsMemberExpression().ExtractNameTypeAndValue<U>();

            dynamic clause = GetTypeSpecificSpec(name, type, value);

            var casted = (Clauses)clause;

            // TODO: verificar se não existe duplicado na lista
            Clauses.Add(casted);

            return clause;
        }

        private static dynamic GetTypeSpecificSpec<U>(string name, Type type, U value)
        {
            return type switch
            {
                Type stringType when stringType == typeof(string) => new StringClauses(name, (string)(object)value),

                _ => throw new NotImplementedException($"O tipo {type.Name} ainda não possui suporte no framework"),
            };
        }

        public StringClauses Property(Expression<Func<string>> expression)
            => (StringClauses)Property<string>(expression);


        public bool IsValid()
        {
            foreach (var clause in Clauses)
            {
                if (ClauseIsInvalid(clause))
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
                var messages = new HashSet<ValidationMessage>();

                foreach (var clause in Clauses)
                {
                    if (ClauseIsInvalid(clause))
                    {
                        var clauseMessages = clause.Conditions
                            .Where(condition => condition.Expression.Compile().Invoke() is false)
                            .Select(p => p.ValidationMessage)
                            .Distinct();

                        foreach (var clauseMessage in clauseMessages)
                        {
                            messages.Add(new ValidationMessage(clause.PropertyName, clauseMessage));
                        }
                    }
                }

                return messages.ToList();
            }
        }

        private static bool ClauseIsInvalid(Clauses clause)
        {
            return ClauseIsValid(clause) is false;
        }

        private static bool ClauseIsValid(Clauses clause)
        {
            return clause.Conditions.All(condition => condition.Expression.Compile().Invoke() is true);
        }

    }
}
