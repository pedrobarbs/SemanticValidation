using SemanticValidation.Extensions;
using SemanticValidation.Util;
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
            Clauses = new List<Clause>();
        }

        public List<Clause> Clauses { get; private set; }

        public DateTimeClauseSpecification Property(Expression<Func<T, DateTime>> expression) 
        {
            var (name, value) = expression.AsMemberExpression().ExtractNameAndValue<DateTime>();

            return new DateTimeClauseSpecification(name, value);
        }

        public StringClauseSpecification Property(Expression<Func<T, string>> expression)
        {
            var (name, value) = expression.AsMemberExpression().ExtractNameAndValue<string>();

            return new DateTimeClauseSpecification(name, value);
        }

        //public DateTimeClauseSpecification Property<TProperty>(MemberExpression expression)
        //{
        //    var name = expression.Member.Name;
        //    var value = expression.Member.Value;
        //    return new DateTimeClauseSpecification(name, value);
        //}

        //public void HasClauses(params Func<ClauseSpecification, Clause>[] clauses)
        //{
        //    foreach (var clause in clauses)
        //    {
        //        Clauses.Add(clause(new Clause()));
        //    }
        //}
    }
}
