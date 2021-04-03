//using System;

//namespace SemanticValidation
//{
//    public class ClauseBuilder : IClauseBuilder
//    {
//        private readonly Func<bool> condition;

//        private string Message { get; set; }

//        public ClauseBuilder(Func<bool> condition, string defaultMessage)
//        {
//            this.condition = condition;
//            Message = defaultMessage;
//        }

//        public Clause CreateClause()
//        {
//            if (condition() is false) 
//            {
//                return new ViolatedClause(Message);
//            }

//            return new Clause();
//        }

//        public Clause WithMessage(string message) 
//        {
//            Message = message;
//            return CreateClause();
//        }

//        public Clause WithDefaultMessage()
//        {
//            return CreateClause();
//        }
//    }
//}
