using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System;

namespace Hit.Infrastructure
{
    internal class TestNode<World>
    {
        internal Type TestImplementationType { get; }

        internal ITestOptions TestOptions { get; }

        internal string UnitTest { get; }

        internal string ParentTestName { get; }

        internal string TestName { get; }

        internal ITestLogic<World> Test { get; set; }

        internal ITestResult TestResult { get; }

        internal TestNode(
            Type testImplementationType, 
            string testName, 
            string parentTestName, 
            string testOptionsSpec,
            string unitTest)
        {
            TestImplementationType = testImplementationType;
            TestName = testName;
            ParentTestName = parentTestName;
            TestResult = new TestResult(TestName);
            TestOptions = (testOptionsSpec == null) ? TestOptionsImpl.Empty : new TestOptionsImpl(testOptionsSpec);
            UnitTest = unitTest;
        }

        internal TestNode(TestNode<World> other)
        {
            TestImplementationType = other.TestImplementationType;
            TestName = other.TestName;
            ParentTestName = other.ParentTestName;
            TestResult = new TestResult(TestName);
            TestOptions = other.TestOptions;
            UnitTest = other.UnitTest;
            Test = other.Test;
        }

    }

}
