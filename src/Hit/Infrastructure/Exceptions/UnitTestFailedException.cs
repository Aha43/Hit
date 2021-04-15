using System;

namespace Hit.Infrastructure.Exceptions
{
    public class UnitTestFailedException : Exception
    {
        public UnitTestFailedException(string rapport) : base(rapport) { }
    }
}
