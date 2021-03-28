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

        // TODO: Tornar generico
        //public DateTimeClauseSpecification Property(Expression<Func<T, DateTime>> expression)
        //{
        //    var (name, type, value) = expression.AsMemberExpression().ExtractNameTypeAndValue<DateTime>();

        //    var clauseSpec = new DateTimeClauseSpecification(name, value);

        //    // TODO: verificar se não existe duplicado na lista
        //    ClauseSpecifications.Add(clauseSpec);

        //    return clauseSpec;
        //}

        //public StringClauseSpecification Property(Expression<Func<T, string>> expression)
        //{
        //    var (name, type, value) = expression.AsMemberExpression().ExtractNameTypeAndValue<string>();

        //    var clauseSpec = new StringClauseSpecification(name, value);

        //    // TODO: verificar se não existe duplicado na lista
        //    ClauseSpecifications.Add(clauseSpec);

        //    return clauseSpec;
        //}

        // TODO: colocar generics
        public StringClauseSpecification Property(Expression<Func<string>> expression)
        {
            var (name, type, value) = expression.AsMemberExpression().ExtractNameTypeAndValue<string>();

            var clauseSpec = new StringClauseSpecification(name, value);

            // TODO: verificar se não existe duplicado na lista
            ClauseSpecifications.Add(clauseSpec);

            return clauseSpec;
        }

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

        //public void HasClauses(params Func<ClauseSpecification, Clause>[] clauses)
        //{
        //    foreach (var clause in clauses)
        //    {
        //        Clauses.Add(clause(new Clause()));
        //    }
        //}
    }
}
