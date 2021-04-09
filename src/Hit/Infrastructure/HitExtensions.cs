using Hit.Infrastructure.Attributes;
using Hit.Specification.User;
using System;
using System.Linq;

namespace Hit.Infrastructure
{
    internal static class HitExtensions
    {
        internal static TestNode<World>[] CreateTestNodes<World>(this Type type)
        {
            var attrs = type.TestAttribute<World>();
            int n = attrs.Count();
            var retVal = new TestNode<World>[n];
            for (int i = 0; i < n; i++)
            {
                retVal[i] = new TestNode<World>(type, attrs[i].Name, attrs[i].Follows, attrs[i].Options, attrs[i].TestRun);
            }
            return retVal;
        }

        internal static UseAs[] TestAttribute<World>(this Type type)
        {
            if (!type.IsTest<World>())
            {
                throw new Exception("Not test type");
            }

            return Attribute.GetCustomAttributes(type)
                .Where(e => e is UseAs)
                .Select(e => e as UseAs).ToArray();
        }

        public static bool IsTest<World>(this Type type)
        {
            var tt = typeof(ITestLogic<World>);
            return tt.IsAssignableFrom(type);
        }

    }

}
