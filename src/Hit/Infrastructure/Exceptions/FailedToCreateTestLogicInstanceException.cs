using System;

namespace Hit.Infrastructure.Exceptions
{
    public class FailedToCreateTestLogicInstanceException : Exception
    {
        public FailedToCreateTestLogicInstanceException(Type type) : base(type.FullName)
        {
        }
        public FailedToCreateTestLogicInstanceException(Type type, Exception innerException) : base(type.FullName, innerException)
        { 
        }
    }
}
