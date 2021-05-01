using System;

namespace Hit.Infrastructure.Exceptions
{
    public class DuplicateSystemNameException : Exception
    {
        public DuplicateSystemNameException(string name) : base(name)
        {
        }
    }
}
