using SemanticValidation.Contracts;
using SemanticValidation.Extensions;
using System;
using System.Linq.Expressions;

namespace SemanticValidation
{
    public class Clause
    {
        //public DateTimeClauseSpecification Property(Expression<Func<T, DateTime>> expression)
        //{
        //    var (name, type, value) = expression.AsMemberExpression().ExtractNameTypeAndValue<DateTime>();

        //    return new DateTimeClauseSpecification(name, value);
        //}

        //public StringClauseSpecification Property(Expression<Func<T, string>> expression)
        //{
        //    var (name, type, value) = expression.AsMemberExpression().ExtractNameTypeAndValue<string>();

        //    return new StringClauseSpecification(name, value);
        //}
    }

    public class ViolatedClause : Clause
    {
        public string Message { get; set; }

        public ViolatedClause(string message)
        {
            Message = message;
        }
    }
}
