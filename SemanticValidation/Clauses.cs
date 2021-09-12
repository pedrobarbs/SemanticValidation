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
        // TODO: Fazer IEqualityCOmparer
        public Contract() => ClauseCluster = new HashSet<Clause>();

        internal HashSet<Clause> ClauseCluster { get; private set; }

        private Clause Property<TParam>(Expression<Func<TParam>> expression)
        {
            var clause = (Clause)GetClause(expression);

            ClauseCluster.Add(clause);

            return clause;
        }

        private static dynamic GetClause<TParam>(Expression<Func<TParam>> expression)
        {
            var (name, _, value) = expression.AsMemberExpression().ExtractNameTypeAndValue<TParam>();

            var clause = BuildClause(name, value);

            return clause;
        }

        public StringClauses Property(Expression<Func<string>> expression)
            => (StringClauses)Property<string>(expression);

        private static dynamic BuildClause<TParam>(string name, TParam? value)
        {
            return typeof(TParam) switch
            {
                var type when IsSameType<string>(type) => new StringClauses(name, (string?)(object?)value),

                //var type => throw new NotImplementedException($"O tipo {type.FullName} ainda não possui suporte no framework"),
                _ => new {}
            };
        }

        private static bool IsSameType<TParam>(Type type) => type == typeof(TParam);

        public bool IsValid()
        {
            foreach (var clause in ClauseCluster)
            {
                if (ClauseIsInvalid(clause))
                {
                    return false;
                }
            }

            return true;
        }

        public List<ValidationMessage>? ValidationMessages
        {
            get
            {
                var messages = GetViolatedContractMessages();
                return messages?.ToList();
            }
        }

        private HashSet<ValidationMessage>? GetViolatedContractMessages()
        {
            bool violated = false;
            HashSet<ValidationMessage>? messages = null;

            foreach (var clause in ClauseCluster)
            {
                var violatedClausesMessages = clause.GetViolatedClauseMessages();

                if (violatedClausesMessages.Any())
                {
                    if (violated is false)
                    {
                        messages = new();
                        violated = true;
                    }

                    foreach (var clauseMessage in violatedClausesMessages)
                    {
                        messages!.Add(new ValidationMessage(clause.PropertyName, clauseMessage));
                    }
                }
            }

            return messages;
        }

        private static bool ClauseIsInvalid(Clause clause) => ClauseIsValid(clause) is false;

        private static bool ClauseIsValid(Clause clause) => clause.Conditions.All(condition => condition.Expression.Compile().Invoke() is true);
    }
}
