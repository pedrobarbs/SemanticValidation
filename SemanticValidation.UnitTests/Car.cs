using System;

namespace SemanticValidation
{
    public class Car : Validatable<Car>
    {
        public string Brand { get; set; }
        public string Model { get; set; }

        public override void Validate(Contract<Car> contract)
        {
            contract.Property(() => Brand)
                .Cannot_Be__Null_Empty_WhiteSpace()
                .WithMessage("A marca precisa ser informada");

            contract.Property(() => Model)
                .Cannot_Be__Null_Empty_WhiteSpace()
                .WithMessage("O modelo precisa ser informado");

        }
    }
}
