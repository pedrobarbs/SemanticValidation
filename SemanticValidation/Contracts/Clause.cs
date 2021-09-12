using SemanticValidation.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SemanticValidation.Contracts
{
    public class Clause<T> : Clause where T : class
    {
        protected readonly T? Value;

        public Clause(string propertyName, T? value) : base(propertyName)
        {
            Value = value;
        }
    }

    public class Clause
    {
        public Clause(string propertyName)
        {
            PropertyName = propertyName;
            Conditions = new HashSet<Condition>(new ExpressionEqualityComparer());
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
        { }

        protected void BuildCondition(string message, Expression<Func<bool>> conditionExpression)
        {
            Conditions.Add(new Condition()
            {
                Expression = conditionExpression,
                ValidationMessage = message
            });
        }

        internal List<string> GetViolatedClauseMessages()
        {
            return Conditions
                .Where(condition => condition.Expression.Compile().Invoke() is false)
                .Select(p => p.ValidationMessage)
                .Distinct()
                .ToList();
        }
    }
}
