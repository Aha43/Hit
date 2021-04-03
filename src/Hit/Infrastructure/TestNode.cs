using Hit.Specification;
using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System;

namespace Hit.Infrastructure
{
    internal class TestNode<World>
    {
        internal Type TestImplementationType { get; }

        internal ITestOptions TestOptions { get; }

        internal string ParentTestName { get; }

        internal string TestName { get; }

        internal ITestImplementation<World> Test { get; set; }

        internal ITestResult TestResult { get; }

        internal TestNode(Type testImplementationType, string testName, string parentTestName, string testOptionsSpec)
        {
            TestImplementationType = testImplementationType;
            TestName = testName;
            ParentTestName = parentTestName;
            TestResult = new TestResult(TestName);
            TestOptions = (testOptionsSpec == null) ? TestOptionsImpl.Empty : new TestOptionsImpl(testOptionsSpec);
        }

    }

}
