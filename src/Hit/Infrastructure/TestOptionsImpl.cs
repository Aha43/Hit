using Hit.Specification.Infrastructure;
using System;
using System.Collections.Generic;

namespace Hit.Infrastructure
{
    internal class TestOptionsImpl : ITestOptions
    {
        private readonly Dictionary<string, string> _opts = new Dictionary<string, string>();

        internal static ITestOptions Empty => new TestOptionsImpl();

        private TestOptionsImpl() { }

        internal TestOptionsImpl(string s)
        {
            var tokens = s.Split(',');
            foreach (var token in tokens)
            {
                var (name, val) = ParseOptPair(token, s);
                _opts[name] = val;
            }
        }

        private (string name, string val) ParseOptPair(string ps, string s)
        {
            try
            {
                ps = ps.Trim();
                int idx = ps.IndexOf('=');
                if (idx != -1)
                {
                    var name = ps.Substring(0, idx).Trim();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        var val = ps.Substring(idx + 1).Trim();
                        if (!string.IsNullOrWhiteSpace(val))
                        {
                            return (name, val);
                        }
                    }
                }
                throw new ArgumentException(s);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(s, ex);
            }
        }

        public string Get(string name, string def = null)
        {
            if (_opts.TryGetValue(name, out string val)) return val;
            return def;
        }

        public bool Equals(string name, string val, string def = null) => val.Equals(Get(name, def), StringComparison.Ordinal);

        public bool EqualsIgnoreCase(string name, string val, string def = null) => val.Equals(Get(name, def), StringComparison.OrdinalIgnoreCase);

    }

}
