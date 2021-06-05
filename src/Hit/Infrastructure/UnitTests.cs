using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.Exceptions;
using Hit.Infrastructure.Visitors;
using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Infrastructure
{
    public class UnitTests<World> : IUnitTests<World>
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly TestHierarchy<World> _testHierarchy;

        private readonly IWorldProvider<World> _worldProvider;

        private readonly IUnitTestEventHandler<World> _unitTestEventHandler;

        private readonly ISystemConfiguration<World> _systemConfiguration;

        internal Action<string> TestLogicLogger { get; set; }

        public string System { get; }
        public string Layer { get; }
        public string Description { get; }
        public string EnvironmentType { get; }

        public string[] UnitTestsNames => _testHierarchy.UnitTestsNames;

        internal UnitTests(ISystemConfiguration<World> configuration, 
            SysCon meta, 
            string currentLayer, 
            HitTypesFromAssemblies<World> types)
        {
            _systemConfiguration = configuration;

            if (string.IsNullOrWhiteSpace(meta.System))
            {
                throw new MissingUnitTestsNameException();
            }

            System = meta.System;
            Layer = string.IsNullOrWhiteSpace(currentLayer) ? string.Empty : currentLayer;
            Description = meta.Description;
            EnvironmentType = meta.EnvironmentType;

            var services = new ServiceCollection();

            configuration.ConfigureServices(services, configuration.GetConfiguration(meta), meta, currentLayer);

            _serviceProvider = ConfigureUnitTestsServices(services, types);

            _testHierarchy = TestHierarchyBuilder<World>.Create().WithTestImplTypes(types.ServiceTypes).Build();

            ActivateTests();

            _worldProvider = _serviceProvider.GetRequiredService<IWorldProvider<World>>();
            _unitTestEventHandler = _serviceProvider.GetService<IUnitTestEventHandler<World>>();
        }

        private IServiceProvider ConfigureUnitTestsServices(IServiceCollection services, HitTypesFromAssemblies<World> types)
        {
            services.AddSingleton(typeof(IWorldProvider<World>), types.WorldProviderType);

            if (types.UnitTestEventHandlerType != null)
            {
                services.AddSingleton(typeof(IUnitTestEventHandler<World>), types.UnitTestEventHandlerType);
            }
            
            foreach (var type in types.ServiceTypes)
            {
                services.AddSingleton(type);
            }

            return services.BuildServiceProvider();
        }

        private void ActivateTests()
        {
            var activatorTestNodeVisitor = new ActivatorTestNodeVisitor<World>(_serviceProvider);
            _testHierarchy.Dfs(activatorTestNodeVisitor);
        }

        private TestContext<World> CreateContextForUnitTests()
        {
            return new TestContext<World>
            {
                System = System,
                Layer = Layer,
                EnvironmentType = EnvironmentType,
                World = _worldProvider.Get(),
                TestLogicLogger = TestLogicLogger
            };
        }

        public async Task<IUnitTestResult> RunUnitTestAsync(string unitTestName)
        {
            var unitTest = _testHierarchy.GetUnitTest(System, Layer, unitTestName);

            var testContext = CreateContextForUnitTests();

            var systemAvailable = await _systemConfiguration.AvailableAsync();

            if (systemAvailable)
            {
                await unitTest.RunUnitTestAsync(testContext, _unitTestEventHandler);
            }

            var results = unitTest.GetTestResult();

            return new UnitTestResult(System, Layer, Description, unitTestName, results, systemAvailable);
        }

        public ITestLogic<World> GetTest(string name) => _testHierarchy.GetNode(name)?.Test;

        public IEnumerable<string> UnitTestNames => _testHierarchy.UnitTestNames;

        public int UnitTestCount => UnitTestsNames.Count();

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var unitTestName in UnitTestsNames)
            {
                var unitTest = _testHierarchy.GetUnitTest(System, Layer, unitTestName);
                sb.AppendLine(unitTest.ToString());
            }
            return sb.ToString();
        }

    }

}
