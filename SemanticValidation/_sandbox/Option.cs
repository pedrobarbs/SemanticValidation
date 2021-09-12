using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SemanticValidation._sandbox
{
    [Serializable]
    public struct Option<A>
    {
        public static implicit operator Option<A>(A a) => new Option<A>();
    }
}
