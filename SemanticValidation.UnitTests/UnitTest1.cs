using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SemanticValidation.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void CarIsValid()
        {
            var car = new Car() { Brand = "ABC", Model = "DEF"};
            //var car = new Car();

            var messages = car.ValidationMessages;
            Assert.True(car.IsValid);
            Assert.True(messages is null);
        }

        [Fact]
        public void CarIsInvalid()
        {
            var car = new Car();
            //var car = new Car();

            var messages = car.ValidationMessages;

            Assert.True(car.IsInvalid);
            Assert.Equal(2, messages.Count);
        }

        [Fact]
        public void ThrowsException()
        {
            var car = new Car();
            //var car = new Car();

            var messages = car.ValidationMessages;

            Assert.True(car.IsInvalid);
            Assert.Equal(2, messages.Count);
        }
    }
}
