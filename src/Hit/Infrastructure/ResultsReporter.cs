using Hit.Specification.Infrastructure;
using System.Text;

namespace Hit.Infrastructure
{
    public static class ResultsReporterUtil
    {
        public static string Report(IUnitTestResult results)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(results.UnitTestsName))
            {
                sb.Append("Unit tests: ")
                  .AppendLine(results.UnitTestsName);
            }
            if (!string.IsNullOrWhiteSpace(results.UnitTestsDescription))
            {
                sb.Append("Unit tests description: ")
                  .AppendLine(results.UnitTestsDescription);
            }
            if (!string.IsNullOrWhiteSpace(results.UnitTest))
            {
                sb.Append("Unit test name: ")
                  .AppendLine(results.UnitTest);
            }

            foreach (var resultNode in results.Results)
            {
                Report(resultNode, 1, sb);
            }

            return sb.ToString();
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
