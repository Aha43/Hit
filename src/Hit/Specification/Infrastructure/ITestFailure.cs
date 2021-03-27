using System;

namespace Hit.Specification.Infrastructure
{
    public interface ITestFailure
    {
        TestFailureSource Source { get; }
        Exception Exception { get; }
    }
}
