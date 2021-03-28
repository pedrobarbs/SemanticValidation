using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SemanticValidation.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var car = new Car() { Brand = "dsadsa"};
            //var car = new Car();

            var valid = car.IsValid;

            Assert.True(valid);
        }

        [Fact]
        public void Test2()
        {
            var list = new List<string>();

            var teste = list.Where(prop => prop.Length > 50).Select(p => p.ToLowerInvariant()).ToList();

            Assert.Null(teste);

        }
    }
}
