using System;
using System.Collections.Generic;
using System.Text;

namespace SemanticValidation.UnitTests
{
    public class WreckedTrain : Validatable<WreckedTrain>
    {
        public int Length { get; set; }

        public override void Validate(Contract<WreckedTrain> contract)
        {
            //contract.Property(() => Length);
        }
    }
}
