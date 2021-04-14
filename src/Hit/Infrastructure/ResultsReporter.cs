using Hit.Specification.Infrastructure;
using System.Text;

namespace Hit.Infrastructure
{
    public static class ResultsReporterUtil
    {
        public static string Report(ITestRunResult results)
        {
            var sb = new StringBuilder();
            Report(results, sb);
            return sb.ToString();
        }

        private static void Report(ITestRunResult results, StringBuilder sb)
        {
            if (!string.IsNullOrWhiteSpace(results.SuiteName))
            {
                sb.Append("Suite: ")
                  .AppendLine(results.SuiteName);
            }
            if (!string.IsNullOrWhiteSpace(results.SuiteDescription))
            {
                sb.Append("Description: ")
                  .AppendLine(results.SuiteDescription);
            }

            foreach (var resultNode in results.Results)
            {
                Report(resultNode, 1, sb);
            }
        }

        private const string Indent = "  ";

        private static void Report(ITestResultNode resultNode, int level, StringBuilder sb)
        {
            for (var i = 0; i < level; i++) sb.Append(Indent);
            Report(resultNode.TestResult, sb);
            if (resultNode.Next != null)
            {
                Report(resultNode.Next, level + 1, sb);
            }
        }

        private static void Report(ITestResult result, StringBuilder sb)
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

        private static void Report(ITestFailure failure, StringBuilder sb)
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
