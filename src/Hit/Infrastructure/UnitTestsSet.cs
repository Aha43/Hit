using Hit.Infrastructure.Exceptions;
using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hit.Infrastructure
{
    public class UnitTestsSet<World> : IUnitTestsSet<World>
    {
        private readonly Dictionary<string, IUnitTests<World>> _unitTests = new Dictionary<string, IUnitTests<World>>();

        public UnitTestsSet()
        {
            var types = new HitTypesFromAssemblies<World>();

            if (types.SystemConfigurationTypes.Length == 0)
            {
                throw new NoConfigurationsFoundException();
            }

            foreach (var confType in types.SystemConfigurationTypes)
            {
                var configuration = Activator.CreateInstance(confType) as ISystemConfiguration<World>;
                var unitTests = new UnitTests<World>(configuration, types);

                if (_unitTests.ContainsKey(unitTests.Name))
                {
                    throw new DuplicateUnitTestsNameException(unitTests.Name);
                }

                _unitTests.Add(unitTests.Name, unitTests);
            }
        }
        
        public IUnitTests<World> GetNamedUnitTests(string name)
        {
            if (_unitTests.TryGetValue(name, out IUnitTests<World> retVal))
            {
                return retVal;
            }

            throw new UnitTestsNotFoundException(name);
        }

        public async Task<IUnitTestResult> RunUnitTestAsync(string unitTestsName, string unitTest)
        {
            var unitTests = GetNamedUnitTests(unitTestsName);
            var results = await unitTests.RunUnitTestAsync(unitTest).ConfigureAwait(false);
            return results;
        }

    }

}
