﻿using Hit.Specification.Infrastructure;
using System.Collections.Generic;

namespace Hit.Infrastructure
{
    internal class TestResultNode : ITestResultNode
    {
        private readonly TestResult _testResult;

        internal TestResultNode(TestResult testResult)
        {
            _testResult = new TestResult(testResult);
        }

        public ITestResultNode Next { get; set; }

        public ITestResult TestResult => _testResult;

    }

}
