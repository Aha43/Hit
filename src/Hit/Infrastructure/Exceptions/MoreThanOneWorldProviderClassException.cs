using System;

namespace Hit.Infrastructure.Exceptions
{
    public class MoreThanOneWorldProviderClassException : Exception
    {
        public MoreThanOneWorldProviderClassException() : base("More than one world provider class not allowed")
        {
        }
    }
}
