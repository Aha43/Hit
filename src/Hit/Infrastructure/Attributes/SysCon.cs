using System;

namespace Hit.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SysCon : Attribute
    {
        public string Name { get; }
        public string Description { get; set; }
        public string EnvironmentType { get; set; }
        public SysCon(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("missing " + nameof(name));
            }

            Name = name;
        }

    }

}
