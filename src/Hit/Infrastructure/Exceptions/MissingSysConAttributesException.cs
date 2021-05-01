using Hit.Infrastructure.Attributes;
using System;

namespace Hit.Infrastructure.Exceptions
{
    public class MissingSysConAttributesException : Exception
    {
        public MissingSysConAttributesException(Type type) : base("Configuration class " + type.FullName + " missing " + nameof(SysCon) + " attributes")
        {
        }
    }
}
