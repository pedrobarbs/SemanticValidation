using SemanticValidation.Enums;

namespace SemanticValidation.Extensions
{
    internal static class StringsExtensions
    {
        internal static StringsCategories Categorize(this string value) 
        {
            StringsCategories category = StringsCategories.Undefined;

            if (value is null) category = StringsCategories.Null;
            else if (value == string.Empty) category = StringsCategories.Empty;
            else if (string.IsNullOrWhiteSpace(value)) category = StringsCategories.WhiteSpace;

            return category;
        }
    }
}
