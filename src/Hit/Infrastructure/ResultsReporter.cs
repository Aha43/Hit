using Hit.Specification.Infrastructure;
using System.Collections.Generic;
using System.Text;

namespace Hit.Infrastructure
{
    public class ResultsReporter : IResultsReporter
    {
        public string Report(IHitSuiteTestResults results)
        {
            var sb = new StringBuilder();
            Report(results, sb);
            return sb.ToString();
        }

        public string Report(IEnumerable<IHitSuiteTestResults> results)
        {
            var sb = new StringBuilder();
            foreach (var res in results) Report(res, sb);
            return sb.ToString();
        }

        private void Report(IHitSuiteTestResults results, StringBuilder sb)
        {
            if (!string.IsNullOrWhiteSpace(results.Name))
            {
                sb.Append("Suite: ")
                  .AppendLine(results.Name);
            }
            if (!string.IsNullOrWhiteSpace(results.Description))
            {
                sb.Append("Description: ")
                  .AppendLine(results.Description);
            }

            foreach (var resultNode in results.Results)
            {
                Report(resultNode, 1, sb);
            }
        }

        private const string Indent = "  ";

        private void Report(ITestResultNode resultNode, int level, StringBuilder sb)
        {
            for (var i = 0; i < level; i++) sb.Append(Indent);
            Report(resultNode.TestResult, sb);
            foreach (var childNode in resultNode.Children)
            {
                var nextLevel = level + 1;
                Report(childNode, nextLevel, sb);
            }
        }

        private void Report(ITestResult result, StringBuilder sb)
        {
            sb.Append("Test: ")
              .Append(result.TestName)
              .Append(" Status: ")
              .Append(result.Status.ToString())
              .AppendLine();
        }

    }

}
