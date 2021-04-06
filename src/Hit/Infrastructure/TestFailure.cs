using Hit.Specification.Infrastructure;
using System;

namespace Hit.Infrastructure
{
    internal class TestFailure : ITestFailure
    {
        public Exception Exception { get; set; }

        internal TestFailure() { }

        internal TestFailure(ITestFailure o)
        {
            Exception = o.Exception;
        }

    }

}
