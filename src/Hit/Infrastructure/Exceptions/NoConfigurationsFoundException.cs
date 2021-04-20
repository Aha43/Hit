using System;

namespace Hit.Infrastructure.Exceptions
{
    public class NoConfigurationsFoundException : Exception
    {
        public NoConfigurationsFoundException() : base("No configuration class found, at least on must be provided")
        {
        }
    }
}
