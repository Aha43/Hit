using System;

namespace Hit.Exceptions
{
    public class UnknownNamedTestRunException : Exception
    {
        public UnknownNamedTestRunException(string name) : base(name)
        {
        }
    }
}
