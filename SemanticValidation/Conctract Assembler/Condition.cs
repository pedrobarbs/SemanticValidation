using System;
using System.Linq.Expressions;

namespace SemanticValidation.Contract_Assembler
{
    public class Condition
    {
        public Expression<Func<bool>>? Expression { get; set; }
        public string? ValidationMessage { get; set; }
    }
}
