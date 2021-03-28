using System;

namespace SemanticValidation.Contracts
{
    //public class ComparableClauseSpecification<T> : ClauseSpecification<T> where T : class, IComparable
    //{
    //    public ComparableClauseSpecification(string propertyName, T? value) : base(propertyName, value)
    //    {
    //    }

    //    public IClauseBuilder Must_Be__Greater_Than_(T? comparator)
    //    {
    //        var defaultMessage = $"Property \"{PropertyName}\" must be greater than {comparator}. Current value: {Value}";

    //        if (Value.HasValue && comparator.HasValue)
    //        {
    //            return new ClauseBuilder(() => Value.Value.CompareTo(comparator.Value) > 0, defaultMessage);
    //        }
    //        else if (Value.HasValue && (comparator.HasValue == false))
    //        {
    //            return new ClauseBuilder(() => true, defaultMessage);
    //        }
    //        else if ((Value.HasValue == false) && comparator.HasValue)
    //        {
    //            return new ClauseBuilder(() => false, defaultMessage);
    //        }
    //        else
    //        {
    //            return new ClauseBuilder(() => false, defaultMessage);
    //        }
    //    }

    //    public IClauseBuilder Must_Be__Equal_Or_Greater_Than_(T? comparator)
    //    {
    //        var defaultMessage = $"Property \"{PropertyName}\" must be equal or greater than {comparator}. Current value: {Value}";

    //        if (Value.HasValue && comparator.HasValue)
    //        {
    //            return new ClauseBuilder(() => Value.Value.CompareTo(comparator.Value) >= 0, defaultMessage);
    //        }
    //        else if (Value.HasValue && (comparator.HasValue == false))
    //        {
    //            return new ClauseBuilder(() => true, defaultMessage);
    //        }
    //        else if ((Value.HasValue == false) && comparator.HasValue)
    //        {
    //            return new ClauseBuilder(() => false, defaultMessage);
    //        }
    //        else
    //        {
    //            return new ClauseBuilder(() => true, defaultMessage);
    //        }
    //    }

    //    public IClauseBuilder Must_Be__Equal_To_(T comparator)
    //    {
    //        var defaultMessage = $"Property \"{PropertyName}\" must be equal to {comparator}. Current value: {Value}";

    //        return new ClauseBuilder(() => Value.Value.CompareTo(comparator) == 0, defaultMessage);
    //    }

    //    public IClauseBuilder Must_Be__Less_Than_(T comparator)
    //    {
    //        var defaultMessage = $"Property \"{PropertyName}\" must be less than {comparator}. Current value: {Value}";

    //        return new ClauseBuilder(() => Value.Value.CompareTo(comparator) < 0, defaultMessage);
    //    }

    //    public IClauseBuilder Must_Be__Equal_Or_Less_Than_(T comparator)
    //    {
    //        var defaultMessage = $"Property \"{PropertyName}\" must be equal or less than {comparator}. Current value: {Value}";

    //        return new ClauseBuilder(() => Value.Value.CompareTo(comparator) <= 0, defaultMessage);
    //    }
    }

