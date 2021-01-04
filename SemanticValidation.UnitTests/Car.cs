using System;

namespace SemanticValidation
{
    public class Car : Validatable
    {
        public string Brand { get; set; }

        public override void Validate(Contract contract)
        {
            contract
                .Requires()
                .MustBeGreaterThan(new DateTime(), new DateTime(), "teste");
        }
    }
}
