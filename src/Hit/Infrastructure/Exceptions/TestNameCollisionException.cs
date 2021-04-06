using System;

namespace Hit.Infrastructure.Exceptions
{
    public class TestNameCollisionException : Exception
    {
        public TestNameCollisionException(string name) : base(name)
        {
        }
    }
}
