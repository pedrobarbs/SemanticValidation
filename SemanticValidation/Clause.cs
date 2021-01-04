namespace SemanticValidation
{
    public class Clause
    {
    }

    public class ViolatedClause
    {
        public string Message { get; set; }

        public ViolatedClause(string message)
        {
            Message = message;
        }
    }
}
