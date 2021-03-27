using Hit.Attributes;
using Hit.Specification.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hit.Infrastructure
{
    public static class HitExtensions
    {
        public static string TestName<World>(this Type type)
        {
            var retVal = default(string);
            var attr = type.TestAttribute<World>();
            if (attr != default)
            {
                retVal = attr.TestName;
            }

            return string.IsNullOrWhiteSpace(retVal) ? type.FullName : retVal.Trim();
        }

        public static IEnumerable<string> ParentNames<World>(this Type type)
        {
            return type.ParentAttributes<World>().Select(e => e.ParentTestName);
        }

        public static Test TestAttribute<World>(this Type type)
        {
            if (!type.IsTest<World>())
            {
                throw new Exception("Not test type");
            }

            return Attribute.GetCustomAttributes(type)
                .Where(e => e is Test)
                .Select(e => e as Test)
                .FirstOrDefault();
        }

        public static IEnumerable<Parent> ParentAttributes<World>(this Type type)
        {
            if (!type.IsTest<World>())
            {
                throw new Exception("Not test type");
            }

            return Attribute.GetCustomAttributes(type)
                .Where(e => e is Parent)
                .Select(e => e as Parent);
        }

        public static bool IsTest<World>(this Type type)
        {
            var tt = typeof(ITest<World>);
            return tt.IsAssignableFrom(type);
        }

    }

}
