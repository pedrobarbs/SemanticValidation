using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SemanticValidation.Util.EqualityComparers
{
    public class ConditionEqualityComparer : IEqualityComparer<Condition>
    {
        private static ExpressionEqualityComparer expressionEqualityComparer = new();

        public bool Equals(Condition? x, Condition? y)
        {
            // TODO: comparar referencias, e nulls

            return expressionEqualityComparer.Equals(x?.Expression, y?.Expression);
        }

        public int GetHashCode([DisallowNull] Condition obj)
        {
            // TODO: fazer null check 
            // TODO: verificar "^" é correto
            return HashCode.Combine(expressionEqualityComparer.GetHashCode(obj.Expression) ^ obj.ValidationMessage.GetHashCode());
        }
    }
}
