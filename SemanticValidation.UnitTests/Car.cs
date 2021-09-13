using SemanticValidation.Contract_Assembler;
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
                .Must_Not_Be___Null_Empty_WhiteSpace()
                .Must_Have___Maximum_Length_Of_(4)
                .WithMessage("A marca precisa ser informada");

            contract.Property(() => Model)
                .Must_Not_Be___Null_Empty_WhiteSpace()
                .WithMessage("O modelo precisa ser informado");

            contract.Property(() => Model)
                .Must_Not_Be___Null_Empty_WhiteSpace()
                .WithMessage("O modelo precisa ser informado");

            contract.Property(() => Model)
            .Must_Not_Be___Null_Empty_WhiteSpace();
        }
    }
}
