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

        private Hierarchy<World> _hierarchy;

        private IWorldProvider<World> _worldProvider;

        public HitSuite(Action<HitSuiteOptions> conf = null)
        {
            var opt = new HitSuiteOptions();
            if (conf != null)
            {
                conf.Invoke(opt);
            }

            var testTypes = FindTestTypes();

            _serviceProvider = ConfigureTestServices(testTypes, opt);

            _hierarchy = MakeHierarchy(testTypes);

            ActivateTests();

            _worldProvider = _serviceProvider.GetRequiredService<IWorldProvider<World>>();
        }

        private IEnumerable<Type> FindTestTypes()
        {
            var hitType = typeof(IHitType<World>);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => hitType.IsAssignableFrom(p));

            return types;
        }

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

        private Hierarchy<World> MakeHierarchy(IEnumerable<Type> types)
        {
            var retVal = new Hierarchy<World>();

            var testType = typeof(ITest<World>);
            foreach (var type in types)
            {
                if (testType.IsAssignableFrom(type))
                {
                    retVal.Add(type);
                }
            }

            retVal.Completed();

            return retVal;
        }

        private void ActivateTests()
        {
            var activatorTestNodeVisitor = new ActivatorTestNodeVisitor<World>(_serviceProvider);
            _hierarchy.Dfs(activatorTestNodeVisitor);
        }

        public async Task<IEnumerable<ITestResultNode>> RunTestsDfsAsync()
        {
            var world = _worldProvider.Get();

            _hierarchy.Dfs(new NotRunTestNodeVisitor<World>());

            await _hierarchy.DfsAsync(new RunTestNodeVisitorAsync<World>(world)).ConfigureAwait(false);

            return _hierarchy.CreateTestResultForrest();
        }

        public async Task<IEnumerable<ITestResultNode>> RunTestsAsync()
        {
            var testRuns = new TestRuns<World>(_hierarchy);

            await testRuns.TestsAsync(_worldProvider);

            return testRuns.CreateTestResultForrest();
        }

        public ITest<World> GetTest(string name) => _hierarchy.GetNode(name)?.Test;

        // Type constants

        internal static Type WorldProviderType => typeof(IWorldProvider<World>);

    }

}
