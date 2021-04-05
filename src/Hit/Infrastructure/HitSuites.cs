using Hit.Specification.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hit.Infrastructure
{
    public class HitSuites<World> : IHitSuites<World>
    {
        private readonly List<IHitSuite<World>> _suites = new List<IHitSuite<World>>();

        public HitSuites<World> AddSuite(Action<HitSuiteOptions> conf = null)
        {
            _suites.Add(new HitSuite<World>(conf));
            return this;
        }

        public IEnumerable<IHitSuite<World>> Suites => _suites.AsReadOnly();

        public IHitSuite<World> GetNamedSuite(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            return _suites.Find(e => name.Equals(e.Name));
        }

        public async Task<IEnumerable<IHitSuiteTestResults>> RunTestsAsync()
        {
            var suites = _suites.ToArray();
            var n = suites.Count();
            var retVal = new IHitSuiteTestResults[n];
            for (var i = 0; i < n; i++)
            {
                retVal[i] = await suites[i].RunTestsAsync().ConfigureAwait(false);
            }
            return retVal;
        }

    }

}
