using Hit.Infrastructure.Visitors;
using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hit.Infrastructure
{
    public class HitSuite<World> : IHitSuite<World>
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
                if (WorldFactoryType.IsAssignableFrom(type))
                {
                    AddWorldFactory(type, services);
                }
                else
                {
                    services.AddSingleton(type);
                }
            }

            return services.BuildServiceProvider();
        }

        private void AddWorldFactory(Type type, IServiceCollection services)
        {
            var instance = Activator.CreateInstance(type) as IWorldCreator<World>;
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

            return retVal;
        }

        private void ActivateTests()
        {
            var activatorTestNodeVisitor = new ActivatorTestNodeVisitor<World>(_serviceProvider);
            _hierarchy.Dfs(activatorTestNodeVisitor);
        }

        public IEnumerable<ITestResultNode> RunTests()
        {
            var world = _worldCreator.Create();

            _hierarchy.Dfs(new NotRunTestNodeVisitor<World>());

            _hierarchy.Dfs(new RunTestNodeVisitor<World>(world));

            return _hierarchy.CreateTestResultForrest();
        }

        // Type constants

        internal static Type WorldFactoryType => typeof(IWorldCreator<World>);

    }

}
