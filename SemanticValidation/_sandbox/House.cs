using System;
using System.Collections.Generic;
using System.Text;

namespace SemanticValidation._sandbox
{
    public class House
    {
        public Option<House> GetHouseOption() 
        {
            return new House();
        }
    }
}
