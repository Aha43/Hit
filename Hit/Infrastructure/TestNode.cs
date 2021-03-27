using Hit.Specification;
using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System;

namespace Hit.Infrastructure
{
    internal class TestNode<World>
    {
        internal Type TestType { get; }

        internal string TestName { get; }

        internal ITest<World> Test { get; set; }

        internal ITestResult TestResult { get; }

        internal TestNode(Type testType)
        {
            TestType = testType;
            TestName = testType.TestName<World>();
            TestResult = new TestResult(TestName);
        }

    }

}
