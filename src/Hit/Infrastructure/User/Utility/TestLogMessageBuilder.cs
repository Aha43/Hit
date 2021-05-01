using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hit.Infrastructure.User.Utility
{
    public class TestLogMessageBuilder<World>
    {
        private Type _testLogicType;

        private readonly List<(Type, Type)> _servicesUsed = new List<(Type, Type)>();

        private ITestContext<World> _context;

        private readonly List<string> _message = new List<string>();

        public TestLogMessageBuilder(ITestLogic<World> testLogic) => _testLogicType = testLogic.GetType();

        public TestLogMessageBuilder<World> ClearTransient()
        {
            _context = null;
            _message.Clear();
            return this;
        }

        public TestLogMessageBuilder<World> UsingService(object implementation)
        {
            _servicesUsed.Add((implementation.GetType(), null));
            return this;
        }

        public TestLogMessageBuilder<World> UsingService(Type specification, object implementation)
        {
            _servicesUsed.Add((specification, implementation.GetType()));
            return this;
        }

        public TestLogMessageBuilder<World> InContext(ITestContext<World> context)
        {
            _context = context;
            return this;
        }

        public TestLogMessageBuilder<World> WithMessage(string message)
        {
            _message.Add(string.IsNullOrWhiteSpace(message) ? string.Empty : message);
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine();

            BuildTestLogicPart(sb);
            BuildServiceUsed(sb);
            BuildContextPart(sb);
            BuildMessages(sb);

            return sb.ToString();
        }

        private void BuildTestLogicPart(StringBuilder sb)
        {
            sb.Append("Test: ")
                .Append(_testLogicType.Name)
                .Append(" (")
                .Append(_testLogicType.FullName)
                .AppendLine(")");
        }

        private void BuildServiceUsed(StringBuilder sb)
        {
            foreach (var serviceUsed in _servicesUsed)
            {
                sb.Append("Using implementation '").Append(serviceUsed.Item2).Append("'");
                if (serviceUsed.Item2 != null)
                {
                    sb.Append(" <-- isa -- '").Append(serviceUsed.Item1.Name).Append("'");
                }
                sb.AppendLine();
            }
        }

        private void BuildContextPart(StringBuilder sb)
        {
            if (_context != null)
            {
                sb.Append("Context: ").AppendLine(_context.ToString());
            }
        }

        private void BuildMessages(StringBuilder sb)
        {
            foreach (var msg in _message)
            {
                sb.AppendLine(msg);
            }
        }

    }

}
