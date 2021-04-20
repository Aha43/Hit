using System;
using System.Collections.Generic;
using System.Text;

namespace Hit.Infrastructure.Exceptions
{
    public class NoWorldProviderFoundException : Exception
    {
        public NoWorldProviderFoundException() : base("Did not find a world provider")
        {
        }
    }
}
