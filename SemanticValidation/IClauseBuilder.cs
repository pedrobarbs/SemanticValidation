namespace SemanticValidation
{
    public interface IClauseBuilder
    {
        Clause WithMessage(string message);

        Clause WithDefaultMessage();
    }
}
