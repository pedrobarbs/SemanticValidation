using System;

namespace SemanticValidation
{
    public class Car : Validatable
    {
        public string Brand { get; set; }

        public override void Validate(Contract<Car> contract)
        {
            //contract
            //    .Requires()
            //    .MustBeGreaterThan(new DateTime(), new DateTime(), "teste");


            contract
                .Property(prop => prop.Brand)
                .Cannot_Be__Null_Empty_WhiteSpace()
                .WithDefaultMessage();
        }
    }
}
