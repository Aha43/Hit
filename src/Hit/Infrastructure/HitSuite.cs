using Hit.Infrastructure.Visitors;
using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hit.Infrastructure
{
    public class HitSuite<World> : IHitSuite<World>
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly TestHierarchy<World> _testHierarchy;

        private readonly IWorldProvider<World> _worldProvider;

        private readonly IUnitTestEventHandler<World> _unitTestEventHandler;

        public string Name { get; }
        public string Description { get; }
        public string EnvironmentType { get; }

        public HitSuite(Action<HitSuiteOptions> conf)
        {
            if (conf == null)
            {
                throw new ArgumentNullException(nameof(conf));
            }

            var opt = new HitSuiteOptions();
            conf?.Invoke(opt);

            if (string.IsNullOrWhiteSpace(opt.Name))
            {
                throw new ArgumentException("Missing suite name");
            }

            Name = opt.Name;
            Description = opt.Description;
            EnvironmentType = opt.EnvironmentType;

            var testImplTypes = FindTestImplTypes();

            _serviceProvider = ConfigureTestServices(testImplTypes, opt);

            _testHierarchy = TestHierarchyBuilder<World>.Create().WithTestImplTypes(testImplTypes).Build();

            ActivateTests();

            _worldProvider = _serviceProvider.GetRequiredService<IWorldProvider<World>>();
            _unitTestEventHandler = _serviceProvider.GetService<IUnitTestEventHandler<World>>();
        }

        private IEnumerable<Type> FindTestImplTypes()
        {
            var hitBaseType = typeof(IHitType<World>);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes());

            var hitTypes = types.Where(p => hitBaseType.IsAssignableFrom(p));

            return hitTypes;
        }

        private static Type WorldProviderType => typeof(IWorldProvider<World>);
        private static Type UnitTestEventHandlerType => typeof(IUnitTestEventHandler<World>);

        private IServiceProvider ConfigureTestServices(IEnumerable<Type> types, HitSuiteOptions opt)
        {
            var services = opt.Services;

            foreach (var type in types)
            {
                if (WorldProviderType.IsAssignableFrom(type))
                {
                    services.AddSingleton(typeof(IWorldProvider<World>), type);
                }
                if (UnitTestEventHandlerType.IsAssignableFrom(type))
                {
                    services.AddSingleton(typeof(IUnitTestEventHandler<World>), type);
                }
                else
                {
                    services.AddSingleton(type);
                }
            }

            return services.BuildServiceProvider();
        }

        private void ActivateTests()
        {
            var activatorTestNodeVisitor = new ActivatorTestNodeVisitor<World>(_serviceProvider);
            _testHierarchy.Dfs(activatorTestNodeVisitor);
        }

        private TestContext<World> CreateContextForSuite()
        {
            return new TestContext<World>
            {
                SuiteName = Name,
                EnvironmentType = EnvironmentType,
                World = _worldProvider.Get()
            };
        }

        public async Task<IUnitTestResult> RunUnitTestAsync(string unitTestName)
        {
            var unitTest = _testHierarchy.GetUnitTest(unitTestName);

            var testContext = CreateContextForSuite();

            await unitTest.RunUnitTestAsync(testContext, _unitTestEventHandler);

            var results = unitTest.GetTestResult();

            return new UnitTestResult(Name, Description, unitTestName, results);
        }

        public ITestLogic<World> GetTest(string name) => _testHierarchy.GetNode(name)?.Test;

    }

}
