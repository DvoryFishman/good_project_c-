//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BL.BO
//{
//    class Exception
//    {
       

//[Serializable]
//    public class BlNotFoundException : Exception
//    {
//        public BlNotFoundException(string? message) : base(message) { }
//        public BlNotFoundException(string? message, Exception e) : base(message, e) { }

//    }

//    [Serializable]
//    public class BlAlreadyExistsException : Exception
//    {
//        public BlAlreadyExistsException(string? message) : base(message) { }
//        public BlAlreadyExistsException(string? message, Exception? inner) : base(message, inner) { }
     
//    }


//        [Serializable]
//        public class BlException : Exception
//        {
//            public BlException(string? message) : base(message) { }
//            public BlException(string? message, Exception? inner) : base(message, inner) { }

//        }


//        [Serializable]
//        public class BlNotInStack : Exception
//        {
//            public BlNotInStack(string? message) : base(message) { }
//            public BlNotInStack(string? message, Exception? inner) : base(message, inner) { }

//        }

//    }
//}
