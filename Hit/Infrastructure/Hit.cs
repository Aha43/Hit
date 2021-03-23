using Hit.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hit.Infrastructure
{
    public class Hit<World> : IHit<World>
    {
        private IServiceProvider _serviceProvider;

        private Hierarchy<World> _hierarchy;

        private IWorldCreator<World> _worldCreator;

        public void Initialize()
        {
            var testTypes = FindTestTypes();

            _serviceProvider = ConfigureTestServices(testTypes);

            _hierarchy = MakeHierarchy(testTypes);

            ActivateTests();

            _worldCreator = _serviceProvider.GetRequiredService<IWorldCreator<World>>();
        }

        private IEnumerable<Type> FindTestTypes()
        {
            var hitType = typeof(IHitType<World>);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => hitType.IsAssignableFrom(p));

            return types;
        }

        private IServiceProvider ConfigureTestServices(IEnumerable<Type> types)
        {
            var services = new ServiceCollection();

            foreach (var type in types)
            {
                services.AddSingleton(type);
            }

            return services.BuildServiceProvider();
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

            return retVal;
        }

        private void ActivateTests()
        {
            var activatorTestNodeVisitor = new ActivatorTestNodeVisitor<World>(_serviceProvider);
            _hierarchy.Dfs(activatorTestNodeVisitor);
        }

        IEnumerable<ITestResult> IHit<World>.RunTests()
        {
            var world = _worldCreator.Create();

            _hierarchy.Dfs(new NotRunTestNodeVisitor<World>());

            return default;
        }

    }

}
