using Hit.Specification;
using System;

namespace Hit.Infrastructure
{
    internal class TestFailure : ITestFailure
    {
        public TestFailureSource Source { get; set; }
        public Exception Exception { get; set; }

        internal TestFailure() { }

        internal TestFailure(ITestFailure o)
        {
            Source = o.Source;
            Exception = o.Exception;
        }

    }

}
