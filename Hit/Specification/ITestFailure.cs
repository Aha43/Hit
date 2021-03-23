using System;

namespace Hit.Specification
{
    public interface ITestFailure
    {
        TestFailureSource Source { get; }
        Exception Exception { get; }
    }
}
