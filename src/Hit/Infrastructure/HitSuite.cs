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
        private IServiceProvider _serviceProvider;

        private readonly TestHierarchy<World> _testHierarchy;

        private readonly IWorldProvider<World> _worldProvider;

        private readonly ITestRunEventHandler<World> _testRunEventHandler;

        public string Name { get; }
        public string Description { get; }
        public string EnvironmentType { get; }

        public HitSuite(Action<HitSuiteOptions> conf = null)
        {
            var opt = new HitSuiteOptions();
            conf?.Invoke(opt);

            Name = opt.Name;
            Description = opt.Description;
            EnvironmentType = opt.EnvironmentType;

            var testImplTypes = FindTestImplTypes();

            _serviceProvider = ConfigureTestServices(testImplTypes, opt);

            _testHierarchy = TestHierarchyBuilder<World>.Create().WithTestImplTypes(testImplTypes).Build();

            ActivateTests();

            _worldProvider = _serviceProvider.GetRequiredService<IWorldProvider<World>>();
            _testRunEventHandler = _serviceProvider.GetService<ITestRunEventHandler<World>>();
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
        private static Type TestRunEventHandlerType => typeof(ITestRunEventHandler<World>);

        private IServiceProvider ConfigureTestServices(IEnumerable<Type> types, HitSuiteOptions opt)
        {
            var services = opt.Services;

            foreach (var type in types)
            {
                if (WorldProviderType.IsAssignableFrom(type))
                {
                    AddWorldProvider(type, services);
                }
                if (TestRunEventHandlerType.IsAssignableFrom(type))
                {
                    AddTestRunEventHandler(type, services);
                }
                else
                {
                    services.AddSingleton(type);
                }
            }

            return services.BuildServiceProvider();
        }

        private void AddWorldProvider(Type type, IServiceCollection services)
        {
            var instance = Activator.CreateInstance(type) as IWorldProvider<World>;
            services.AddSingleton(instance);
        }

        private void AddTestRunEventHandler(Type type, IServiceCollection services)
        {
            var instance = Activator.CreateInstance(type) as ITestRunEventHandler<World>;
            services.AddSingleton(instance);
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

        public async Task<IHitSuiteTestResults> RunTestsAsync()
        {
            var testRuns = new TestRuns<World>(_testHierarchy);

            var testContext = CreateContextForSuite();

            await testRuns.RunTestsAsync(testContext, _testRunEventHandler).ConfigureAwait(false);

            var results = testRuns.CreateTestResults();

            return new HitSuiteTestResults(Name, Description, results);
        }

        public async Task<IHitSuiteTestResults> RunTestRunAsync(string name)
        {
            var testRun = _testHierarchy.GetNamedTestRun(name);

            var testContext = CreateContextForSuite();

            await testRun.RunTestsAsync(testContext, _testRunEventHandler);

            var results = testRun.GetTestResult();

            return new HitSuiteTestResults(Name, Description, results);
        }

        public ITestLogic<World> GetTest(string name) => _testHierarchy.GetNode(name)?.Test;

    }

}
