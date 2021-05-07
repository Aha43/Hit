using Hit.Specification.Infrastructure;
using System;

namespace HitUnitTests.Assertions
{
    public static class TestResultAsserions
    {
        public static ITestResult IsForTest(this ITestResult testResult, string expectedTest)
        {
            if (testResult == null)
            {
                throw new ArgumentNullException(nameof(testResult));
            }

            if (!expectedTest.Equals(testResult.TestName))
            {
                throw new Exception($"Expected result be for test named '{expectedTest}' but is for test '{testResult.TestName}'");
            }

            return testResult;
        }

        public static ITestResult HasStatus(this ITestResult testResult, TestStatus status)
        {
            if (testResult == null)
            {
                throw new ArgumentNullException(nameof(testResult));
            }

            if (status != testResult.Status)
            {
                throw new Exception($"Expected status '{status}' but is '{testResult.Status}'");
            }

            return testResult;
        }

    }

}
