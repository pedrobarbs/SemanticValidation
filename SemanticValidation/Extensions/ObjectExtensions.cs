using System;

namespace SemanticValidation.Extensions
{
    public static class ObjectExtensions
    {
        public static void ThrowIfNull(this object @object, string paramName)
        {
            if (@object is null) throw new ArgumentNullException(paramName);
        }
    }
}
