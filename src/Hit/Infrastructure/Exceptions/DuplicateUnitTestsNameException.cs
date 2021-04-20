using System;

namespace Hit.Infrastructure.Exceptions
{
    public class DuplicateUnitTestsNameException : Exception
    {
        public DuplicateUnitTestsNameException(string name) : base(name)
        {
        }
    }
}
