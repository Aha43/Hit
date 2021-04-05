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
            foreach (var res in results)
            {
                Report(res, sb);
                sb.AppendLine();
            }
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
            if (resultNode.Next != null)
            {
                Report(resultNode.Next, level + 1, sb);
            }
        }

        private void Report(ITestResult result, StringBuilder sb)
        {
            sb.Append("Test: ")
              .Append(result.TestName)
              .Append(" Status: ")
              .Append(result.Status.ToString())
              .AppendLine();
            if (result.Failure != null)
            {
                Report(result.Failure, sb);
            }
        }

        private void Report(ITestFailure failure, StringBuilder sb)
        {
            sb.AppendLine("!!!");
            if (failure.Exception != null)
            {
                sb.AppendLine(failure.Exception.ToString());
            }
            else
            {
                sb.AppendLine("Test failed with no catched exception!");
            }
            sb.AppendLine("!!!");
        }

    }

}
