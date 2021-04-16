using Hit.Infrastructure.Exceptions;
using Hit.Specification.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hit.Infrastructure
{
    public class HitSuites<World> : IHitSuites<World>
    {
        private readonly Dictionary<string, IHitSuite<World>> _suites = new Dictionary<string, IHitSuite<World>>();

        public HitSuites<World> AddSuite(Action<HitSuiteOptions> conf)
        {
            if (conf == null)
            {
                throw new ArgumentNullException(nameof(conf));
            }

            var suite = new HitSuite<World>(conf);

            if (string.IsNullOrWhiteSpace(suite.Name))
            {
                throw new ArgumentException("A suite in suites must have name");
            }
            if (_suites.ContainsKey(suite.Name))
            {
                throw new ArgumentException("Duplicate suite name: " + suite.Name);
            }

            _suites.Add(suite.Name, suite);
            return this;
        }
        
        public IHitSuite<World> GetNamedSuite(string name)
        {
            if (_suites.TryGetValue(name, out IHitSuite<World> retVal))
            {
                return retVal;
            }

            throw new SuiteNotFoundException(name);
        }

        public async Task<IUnitTestResult> RunUnitTestAsync(string suiteName, string unitTest)
        {
            var suite = GetNamedSuite(suiteName);
            var results = await suite.RunUnitTestAsync(unitTest).ConfigureAwait(false);
            return results;
        }

    }

}
