using System;

namespace Hit.Exceptions
{
    public class TestNameCollisionException : Exception
    {
        public TestNameCollisionException(string name) : base(name)
        {
        }
    }
}
