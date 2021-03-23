using Hit.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hit.Infrastructure
{
    internal class TestNode<World>
    {
        internal Type TestType { get; }

        internal string TestName { get; }

        internal ITest<World> Test { get; set; }

        internal ITestResult TestResult => new TestResult();

        internal TestNode(Type testType)
        {
            TestType = testType;
            TestName = testType.TestName<World>();
        }

    }

}
