using System;

namespace Hit.Infrastructure.Exceptions
{
    public class UnknownNamedTestRunException : Exception
    {
        public UnknownNamedTestRunException(string name) : base(name)
        {
        }
    }
}
