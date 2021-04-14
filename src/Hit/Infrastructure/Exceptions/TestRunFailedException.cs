using System;

namespace Hit.Infrastructure.Exceptions
{
    public class TestRunFailedException : Exception
    {
        public TestRunFailedException(string rapport) : base(rapport) { }
    }
}
