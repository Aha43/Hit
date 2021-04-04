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

        private IWorldProvider<World> _worldProvider;

        public HitSuite(Action<HitSuiteOptions> conf = null)
        {
            var opt = new HitSuiteOptions();
            if (conf != null)
            {
                conf.Invoke(opt);
            }

            var testImplTypes = FindTestImplTypes();

            _serviceProvider = ConfigureTestServices(testImplTypes, opt);

            _testHierarchy = TestHierarchyBuilder<World>.Create().WithTestImplTypes(testImplTypes).Build();

            ActivateTests();

            _worldProvider = _serviceProvider.GetRequiredService<IWorldProvider<World>>();
        }

        private IEnumerable<Type> FindTestImplTypes()
        {
            var hitType = typeof(IHitType<World>);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => hitType.IsAssignableFrom(p));

            return types;
        }

        private static Type WorldProviderType => typeof(IWorldProvider<World>);

        private IServiceProvider ConfigureTestServices(IEnumerable<Type> types, HitSuiteOptions opt)
        {
            var services = opt.Services;

            foreach (var type in types)
            {
                if (WorldProviderType.IsAssignableFrom(type))
                {
                    AddWorldProvider(type, services);
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

        private void ActivateTests()
        {
            var activatorTestNodeVisitor = new ActivatorTestNodeVisitor<World>(_serviceProvider);
            _testHierarchy.Dfs(activatorTestNodeVisitor);
        }

        public async Task<IEnumerable<ITestResultNode>> RunTestsAsync()
        {
            var testRuns = new TestRuns<World>(_testHierarchy);

            await testRuns.TestsAsync(_worldProvider);

            return testRuns.CreateTestResultForrest();
        }

        public ITestImplementation<World> GetTest(string name) => _testHierarchy.GetNode(name)?.Test;

    }

}
