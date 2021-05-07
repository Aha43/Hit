using Hit.Specification.Infrastructure;
using System;

namespace HitUnitTests.Assertions
{
    public static class TestResultNodeAssertions
    {
        public static ITestResultNode GotNextNode(this ITestResultNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.Next == null)
            {
                throw new Exception("No next node");
            }

            return node.Next;
        }

        public static void NoNextNode(this ITestResultNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.Next != null)
            {
                throw new Exception("Got next node");
            }
        }

        public static ITestResultNode NodeGotTestResult(this ITestResultNode node, Action<ITestResult> assertOnTestResult = null)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.TestResult == null)
            {
                throw new Exception("Node got no " + nameof(ITestResult));
            }

            if (assertOnTestResult != null)
            {
                assertOnTestResult.Invoke(node.TestResult);
            }

            return node;
        }

    }

}
