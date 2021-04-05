using System;

namespace Hit.Exceptions
{
    public class HitTestsFailedException : Exception
    {
        public HitTestsFailedException(string rapport) : base(rapport) { }
    }
}
