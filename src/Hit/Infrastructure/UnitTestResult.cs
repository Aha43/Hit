using Hit.Specification.Infrastructure;
using System.Collections.Generic;

namespace Hit.Infrastructure
{
    internal class UnitTestResult : IUnitTestResult
    {
        internal UnitTestResult(string system, string unitTestsDescription, string unitTest, ITestResultNode results, bool systemAvailable)
        {
            System = system;
            UnitTestsDescription = unitTestsDescription;
            UnitTest = unitTest;
            ResultHead = results;
            SystemAvailable = systemAvailable;
        }

        public string System { get; }
        public string UnitTestsDescription { get; }
        public string UnitTest { get; }
        
        public bool SystemAvailable { get; }

        public ITestResultNode ResultHead { get; }

        public bool Success() => Success(ResultHead);
        
        private bool Success(ITestResultNode node)
        {
            if (node.TestResult.Status != TestStatus.Success)
            {
                return false;
            }

            return node.Next == null || Success(node.Next);
        }

    }

}
