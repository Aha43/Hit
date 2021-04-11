using System;

namespace Hit.Infrastructure.Exceptions
{
    public class SuiteNotFoundException : Exception
    {
        public SuiteNotFoundException(string name) : base(name)
        {
        }
    }
}
