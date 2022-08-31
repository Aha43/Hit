using System;

namespace Hit.Infrastructure.Exceptions
{
    public class SysConAttributeMissingSystemPropertyException : Exception
    {
        public SysConAttributeMissingSystemPropertyException() : base("A SysCon attribute does not provide the mandatory System property (name of the system)")
        {
        }
    }
}
