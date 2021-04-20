using System;

namespace Hit.Infrastructure.Exceptions
{
    public class MissingUnitTestsNameException : Exception
    {
        public MissingUnitTestsNameException() : base("Missing unit tests name")
        {
        }
    }
}
