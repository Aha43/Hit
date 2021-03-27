using Hit.Specification.Infrastructure;
using System;

namespace Hit.Infrastructure
{
    internal class TestResult : ITestResult
    {
        public string TestName { get; }
        public TestStatus Status { get; private set; }
        public ITestFailure Failure { get; private set; }

        internal TestResult(string name)
        {
            TestName = name;
        }

        internal TestResult(ITestResult o)
        {
            TestName = o.TestName;
            Status = o.Status;
            Failure = (o.Failure == null) ? null : new TestFailure(o.Failure);
        }

        internal void NotRun() => WithNoFailure(TestStatus.NotRun);
        internal void Success() => WithNoFailure(TestStatus.Success);
        internal void NotReached() => WithNoFailure(TestStatus.NotReached);
        
        private void WithNoFailure(TestStatus status)
        {
            Failure = null;
            Status = status;
        }

        internal void Failed(Exception ex, TestFailureSource source)
        {
            Failure = new TestFailure
            {
                Exception = ex,
                Source = source
            };

            Status = TestStatus.Failed;
        }

    }

}
