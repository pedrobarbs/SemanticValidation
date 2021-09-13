using SemanticValidation.Contract_Assembler;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SemanticValidation.Util.EqualityComparers
{
    public class ConditionEqualityComparer : IEqualityComparer<Condition>
    {
        private static readonly ExpressionEqualityComparer _expressionEqualityComparer = new();

        public bool Equals(Condition? x, Condition? y)
        {
            // TODO: comparar referencias, e nulls
            return SameConditions(x!, y!) && SameMessages(x!, y!);
        }

        private static bool SameMessages(Condition x, Condition y)
            => x.ValidationMessage == y.ValidationMessage;

        private static bool SameConditions(Condition x, Condition y)
            => _expressionEqualityComparer.Equals(x.Expression, y.Expression);

        public int GetHashCode([DisallowNull] Condition obj)
        {
            // TODO: fazer null check 
            // TODO: não funciona
            return HashCode.Combine(
                _expressionEqualityComparer.GetHashCode(obj.Expression),
                obj.ValidationMessage.GetHashCode());
        }
    }
}
