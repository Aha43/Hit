using System;

namespace Hit.Specification.Infrastructure
{
    public interface ITestFailure
    {
        Exception Exception { get; }
    }
}
