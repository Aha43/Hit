using System;

namespace Hit.Infrastructure.Exceptions
{
    public class UnitTestsNotFoundException : Exception
    {
        public UnitTestsNotFoundException(string name) : base(name)
        {
        }
    }
}
