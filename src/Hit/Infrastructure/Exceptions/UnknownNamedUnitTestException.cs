using System;

namespace Hit.Infrastructure.Exceptions
{
    public class UnknownNamedUnitTestException : Exception
    {
        public UnknownNamedUnitTestException(string name) : base(name)
        {
        }
    }
}
