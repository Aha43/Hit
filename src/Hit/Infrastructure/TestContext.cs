using Hit.Specification.Infrastructure;
using System.Text;

namespace Hit.Infrastructure
{
    public class TestContext<TheWorld> : ITestContext<TheWorld>
    {
        public TheWorld World { get; set; }

        public string SuiteName { get; set; } //
        public string TestRunName { get; set; } //
        public string ParentTestName { get; set; } //
        public string TestName { get; set; } //
        public ITestResult TestResult { get; set; } //

        public ITestOptions Options { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(SuiteName))
            {
                sb.Append(nameof(SuiteName)).Append(" = ").Append(SuiteName);
            }
            if (!string.IsNullOrWhiteSpace(TestRunName))
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append(nameof(TestRunName)).Append(" = ").Append(TestRunName);
            }
            if (!string.IsNullOrWhiteSpace(ParentTestName))
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append(nameof(ParentTestName)).Append(" = ").Append(ParentTestName);
            }
            if (!string.IsNullOrWhiteSpace(TestName))
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append(nameof(TestName)).Append(" = ").Append(TestName);
            }
            if (TestResult != null)
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append("Status").Append(" = ").Append(TestResult.Status);
            }
            if (Options != null && !Options.None)
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append(nameof(Options)).Append(" = ").Append("(").Append(Options).Append(")");
            }

            if (sb.Length > 0) sb.Append(", ");
            if (World != null)
            {
                sb.Append("world exists");
            }
            else
            {
                sb.Append("no world");
            }

            return sb.ToString();
        }

    }

}
