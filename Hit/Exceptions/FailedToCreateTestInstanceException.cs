using System;

namespace Hit.Exceptions
{
    public class FailedToCreateTestInstanceException : Exception
    {
        public FailedToCreateTestInstanceException(Type type) : base(type.FullName)
        {
        }
        public FailedToCreateTestInstanceException(Type type, Exception innerException) : base(type.FullName, innerException)
        { 
        }
    }
}
