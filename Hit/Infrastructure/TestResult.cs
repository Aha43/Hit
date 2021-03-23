using Hit.Specification;

namespace Hit.Infrastructure
{
    internal class TestResult : ITestResult
    {
        public string TestName { get; set; }
        public TestStatus Status { get; set; }
        public ITestFailure Failure { get; set; }

        internal TestResult() { }

        internal TestResult(ITestResult o)
        {
            TestName = o.TestName;
            Status = o.Status;
            Failure = new TestFailure(o.Failure);
        }

    }

}
