using Hit.Specification.User;
using System;
using System.Collections.Generic;

namespace Hit.Infrastructure
{
    internal class TestHierarchyBuilder<World>
    {
        private readonly List<Type> _types = new List<Type>();

        private TestHierarchyBuilder() { }

        internal static TestHierarchyBuilder<World> Create() => new TestHierarchyBuilder<World>();

        internal TestHierarchyBuilder<World> WithTestImplTypes(IEnumerable<Type> types)
        {
            foreach (var type in types) WithTestType(type);
            return this;
        }

        internal TestHierarchyBuilder<World> WithTestType(Type type)
        {
            if (TestImplType.IsAssignableFrom(type))
            {
                _types.Add(type);
            }
            return this;
        }

        internal TestHierarchy<World> Build()
        {
            var retVal = new TestHierarchy<World>();
            foreach (var type in _types)
            {
                retVal.AddTestImplType(type);
            }

            retVal.AllTestImplTypesAdded();
            return retVal;
        }

        private static Type TestImplType => typeof(ITestLogic<World>);

    }

}
