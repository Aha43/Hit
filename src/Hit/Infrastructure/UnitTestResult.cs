using Hit.Specification.Infrastructure;
using System.Collections.Generic;

namespace Hit.Infrastructure
{
    internal class UnitTestResult : IUnitTestResult
    {
        internal UnitTestResult(
            string system,
            string layer,
            string unitTestsDescription, 
            string unitTest, 
            ITestResultNode results, 
            bool systemAvailable)
        {
            System = system;
            Layer = layer;
            UnitTestsDescription = unitTestsDescription;
            UnitTest = unitTest;
            ResultHead = results;
            SystemAvailable = systemAvailable;
        }

        public string UnitTestsDescription { get; }

        public string System { get; }
        public string Layer { get; }
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
