using Hit.Specification.Infrastructure;
using System;
using System.Text;

namespace Hit.Infrastructure
{
    public class TestContext<TheWorld> : ITestContext<TheWorld>
    {
        public TheWorld World { get; set; } 
        public string EnvironmentType { get; set; }
        public string System { get; set; }
        public string Layer { get; set; }
        public string UnitTest { get; set; } 
        public string ParentTestName { get; set; } 
        public string TestName { get; set; } 
        public ITestResult TestResult { get; set; } 
        public ITestOptions Options { get; set; }

        internal Action<string> TestLogicLogger { get; set; }

        public void Log(string msg)
        {
            if (TestLogicLogger != null)
            {
                TestLogicLogger.Invoke(msg);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            WriteLocation(sb);
            
            //if (!string.IsNullOrWhiteSpace(System))
            //{
            //    sb.Append(nameof(System)).Append(" = ").Append(System);
            //}
            if (!string.IsNullOrWhiteSpace(EnvironmentType))
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append(nameof(EnvironmentType)).Append(" = ").Append(EnvironmentType);
            }
            //if (!string.IsNullOrWhiteSpace(UnitTest))
            //{
            //    if (sb.Length > 0) sb.Append(", ");
            //    sb.Append(nameof(UnitTest)).Append(" = ").Append(UnitTest);
            //}
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
                sb.Append("World exists");
            }
            else
            {
                sb.Append("No world");
            }

            return sb.ToString();
        }

        private void WriteLocation(StringBuilder sb)
        {
            if (!string.IsNullOrWhiteSpace(UnitTest))
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append(nameof(UnitTest)).Append(" '").Append(UnitTest).Append("'");
            }
            if (!string.IsNullOrWhiteSpace(System))
            {
                sb.Append(" in ").Append(nameof(System).ToLower()).Append(" '").Append(System).Append("'");
            }
            if (!string.IsNullOrWhiteSpace(Layer))
            {
                sb.Append(" at ").Append(nameof(Layer).ToLower()).Append(" '").Append(Layer).Append("'");
            }
        }

    }

}
