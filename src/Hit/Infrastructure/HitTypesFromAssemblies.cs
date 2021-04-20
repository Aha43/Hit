using Hit.Infrastructure.Exceptions;
using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hit.Infrastructure
{
    internal class HitTypesFromAssemblies<World>
    {
        internal Type[] ServiceTypes { get; }
        internal Type WorldProviderType { get; }
        internal Type UnitTestEventHandlerType { get; }
        internal Type[] SystemConfigurationTypes { get; }

        private static Type TheWorldProviderType => typeof(IWorldProvider<World>);
        private static Type TheUnitTestEventHandlerType => typeof(IUnitTestEventHandler<World>);
        private static Type TheSystemConfigurationType => typeof(ISystemConfiguration<World>);

        internal HitTypesFromAssemblies()
        {
            var hitBaseType = typeof(IHitType<World>);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes());

            var hitTypes = types.Where(p => hitBaseType.IsAssignableFrom(p));

            var configTypes = new List<Type>();
            var serviceTypes = new List<Type>();

            foreach (var type in hitTypes)
            {
                if (IsConcreteClass(TheWorldProviderType, type))
                {
                    if (WorldProviderType != null)
                    {
                        throw new MoreThanOneWorldProviderClassException();
                    }

                    WorldProviderType = type;
                }
                else if (IsConcreteClass(TheUnitTestEventHandlerType, type))
                {
                    if (UnitTestEventHandlerType != null)
                    {
                        throw new MoreThanOneUnitTestEventHandlerClassException();
                    }

                    UnitTestEventHandlerType = type;
                }
                else if (IsConcreteClass(TheSystemConfigurationType, type))
                {
                    configTypes.Add(type);
                }
                else if (IsConcreteClass(type))
                {
                    serviceTypes.Add(type);
                }
            }

            if (WorldProviderType == null)
            {
                throw new NoWorldProviderFoundException();
            }

            ServiceTypes = serviceTypes.ToArray();
            SystemConfigurationTypes = configTypes.ToArray();
        }

        private static bool IsConcreteClass(Type type) => type.IsClass && !type.IsAbstract;
        private static bool IsConcreteClass(Type isa, Type type) => isa.IsAssignableFrom(type) && IsConcreteClass(type);

    }

}
