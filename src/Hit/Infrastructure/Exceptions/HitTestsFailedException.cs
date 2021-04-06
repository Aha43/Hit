using System;

namespace Hit.Infrastructure.Exceptions
{
    public class HitTestsFailedException : Exception
    {
        public HitTestsFailedException(string rapport) : base(rapport) { }
    }
}
