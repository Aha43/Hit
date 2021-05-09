using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.Exceptions;
using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hit.Infrastructure
{
    public class UnitTestsSpace<World> : IUnitTestsSpace<World>
    {
        private readonly List<UnitTests<World>> _flatLand = new List<UnitTests<World>>(); // all tests to ease some coding

        private readonly Dictionary<string, Dictionary<string, IUnitTests<World>>> _space = new Dictionary<string, Dictionary<string, IUnitTests<World>>>();

        private readonly HashSet<string> _systemNames = new HashSet<string>();

        private readonly SortedSet<(string system, string layer, string unitTest)> _tupples = new SortedSet<(string system, string layer, string unitTest)>();

        public UnitTestsSpace()
        {
            var types = new HitTypesFromAssemblies<World>();

            if (types.SystemConfigurationTypes.Length == 0)
            {
                throw new NoConfigurationsFoundException();
            }

            foreach (var confType in types.SystemConfigurationTypes)
            {
                var metas = confType.SysConAttribute<World>();
                if (metas == null || !metas.Any())
                {
                    throw new MissingSysConAttributesException(confType);
                }

                foreach (var meta in metas)
                {
                    if (string.IsNullOrWhiteSpace(meta.Layers))
                    {
                        CreateUnitTests(confType, meta, null, types);
                    }
                    else
                    {
                        var layers = meta.Layers.Split(',').Select(s => s.Trim());
                        foreach (var layer in layers)
                        {
                            CreateUnitTests(confType, meta, layer, types);
                        }
                    }
                }
            }
        }

        private void CreateUnitTests(Type confType, SysCon meta, string layer, HitTypesFromAssemblies<World> types)
        {
            var configuration = Activator.CreateInstance(confType) as ISystemConfiguration<World>;

            var unitTests = new UnitTests<World>(configuration, meta, layer, types);

            if (_space.ContainsKey(unitTests.System))
            {
                throw new DuplicateSystemNameException(unitTests.System);
            }

            AddUnitTests(unitTests);
        }

        private void AddUnitTests(UnitTests<World> tests)
        {
            var layer = tests.Layer;
            if (_space.TryGetValue(layer, out Dictionary<string, IUnitTests<World>> layeredTests))
            {
                layeredTests.Add(tests.System, tests);
                _flatLand.Add(tests);
            }
            else
            {
                _space[layer] = new Dictionary<string, IUnitTests<World>>
            {
                { tests.System, tests }
            };

                _flatLand.Add(tests);
            }

            _systemNames.Add(tests.System);

            AddTupples(tests);
        }

        private void AddTupples(UnitTests<World> tests)
        {
            var system = tests.System;
            var layer = tests.Layer;
            foreach (var unitTest in tests.UnitTestNames)
            {
                _tupples.Add((system, layer, unitTest));
            }
        }

        public IUnitTests<World> GetUnitTests()
        {
            if (Dimension == 1)
            {
                return GetUnitTests(_systemNames.First());
            }

            throw new UnitTestsSpaceIsNotOneDimensional();
        }

        public IUnitTests<World> GetUnitTests(string system) => GetUnitTests(system, string.Empty);

        public IUnitTests<World> GetUnitTests(string system, string layer)
        {
            if (string.IsNullOrWhiteSpace(system) && string.IsNullOrWhiteSpace(layer))
            {
                return GetUnitTests();
            }

            if (_space.TryGetValue(layer, out Dictionary<string, IUnitTests<World>> dict))
            {
                if (dict.TryGetValue(system, out IUnitTests<World> retVal))
                {
                    return retVal;
                }
            }

            throw new UnitTestsNotFoundException(system);
        }

        public async Task<IUnitTestResult> RunUnitTestAsync(string unitTest)
        {
            var unitTests = GetUnitTests();
            var results = await unitTests.RunUnitTestAsync(unitTest).ConfigureAwait(false);
            return results;
        }

        public async Task<IUnitTestResult> RunUnitTestAsync(string system, string unitTest)
        {
            var unitTests = GetUnitTests(system);
            var results = await unitTests.RunUnitTestAsync(unitTest).ConfigureAwait(false);
            return results;
        }

        public async Task<IUnitTestResult> RunUnitTestAsync(string system, string layer, string unitTest)
        {
            var unitTests = GetUnitTests(system, layer);
            var results = await unitTests.RunUnitTestAsync(unitTest).ConfigureAwait(false);
            return results;
        }

        public IEnumerable<string> SystemNames => _systemNames.ToArray();

        public IEnumerable<string> LayerNames => _space.Keys.ToArray();

        public void SetTestLogicLogger(Action<string> testLogicLogger)
        {
            foreach (var tests in _flatLand)
            {
                tests.TestLogicLogger = testLogicLogger;
            }
        }

        public int Dimension
        {
            get
            {
                var systemCount = SystemCount;
                var layerCount = LayerCount;

                if (systemCount == 1)
                {
                    if (layerCount == 1)
                    {
                        return 1;
                    }
                    return 2;
                }
                if (layerCount == 1)
                {
                    return 2;
                }

                return 3;
            }
        }

        public int SystemCount => _systemNames.Count;

        public int LayerCount => _space.Count;

        public IEnumerable<(string system, string layer, string unitTests)> UnitTestCoordinates => _tupples.ToArray();

    }

}
