using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.Exceptions;
using Hit.Infrastructure.Visitors;
using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using Microsoft.Extensions.DependencyInjection;
using System;
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

        public string Name { get; }
        public string Description { get; }
        public string EnvironmentType { get; }

        public string[] UnitTestsNames => _testHierarchy.UnitTestsNames;

        internal UnitTests(ISystemConfiguration<World> configuration, HitTypesFromAssemblies<World> types)
        {
            var configAttr = configuration.GetType().SysConAttribute<World>();
            if (configAttr == null)
            {
                throw new Exception("Configuration class " + configuration.GetType().FullName + " missing " + nameof(SysCon) + " attribute");
            }

            if (string.IsNullOrWhiteSpace(configAttr.Name))
            {
                throw new MissingUnitTestsNameException();
            }

            Name = configAttr.Name;
            Description = configAttr.Description;
            EnvironmentType = configAttr.EnvironmentType;

            var services = new ServiceCollection();

            configuration.ConfigureServices(services, configuration.GetConfiguration());

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
                UnitTestsName = Name,
                EnvironmentType = EnvironmentType,
                World = _worldProvider.Get()
            };
        }

        public async Task<IUnitTestResult> RunUnitTestAsync(string unitTestName)
        {
            var unitTest = _testHierarchy.GetUnitTest(unitTestName);

            var testContext = CreateContextForUnitTests();

            await unitTest.RunUnitTestAsync(testContext, _unitTestEventHandler);

            var results = unitTest.GetTestResult();

            return new UnitTestResult(Name, Description, unitTestName, results);
        }

        public ITestLogic<World> GetTest(string name) => _testHierarchy.GetNode(name)?.Test;

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var unitTestName in UnitTestsNames)
            {
                var unitTest = _testHierarchy.GetUnitTest(unitTestName);
                sb.AppendLine(unitTest.ToString());
            }
            return sb.ToString();
        }

    }

}
