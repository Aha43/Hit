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
            var attrs = type.UsAsAttributes<World>();
            int n = attrs.Count();
            var retVal = new TestNode<World>[n];
            for (int i = 0; i < n; i++)
            {
                retVal[i] = new TestNode<World>(type, attrs[i].Name, attrs[i].Follows, attrs[i].Options, attrs[i].UnitTest);
            }
            return retVal;
        }

        internal static SysCon SysConAttribute<World>(this Type type)
        {
            if (!type.IsConfigurationType<World>())
            {
                throw new Exception("Not configuration type");
            }

            return Attribute.GetCustomAttributes(type)
                .Where(e => e is SysCon)
                .Select(e => e as SysCon).FirstOrDefault();
        }

        internal static UseAs[] UsAsAttributes<World>(this Type type)
        {
            if (!type.IsTestLogicType<World>())
            {
                throw new Exception("Not test logic type");
            }

            return Attribute.GetCustomAttributes(type)
                .Where(e => e is UseAs)
                .Select(e => e as UseAs).ToArray();
        }

        public static bool IsTestLogicType<World>(this Type type)
        {
            var tt = typeof(ITestLogic<World>);
            return tt.IsAssignableFrom(type);
        }

        public static bool IsConfigurationType<World>(this Type type)
        {
            var tt = typeof(ISystemConfiguration<World>);
            return tt.IsAssignableFrom(type);
        }

    }

}
