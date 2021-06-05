using System;

namespace Hit.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class SysCon : Attribute
    {
        public string System { get; }
        public string Layers { get; set; }
        public string Description { get; set; }
        public string EnvironmentType { get; set; }
        public string JsonPath { get; set; }
        public string ConfigurationSections { get; set; }
        public SysCon(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("missing " + nameof(name));
            }

            System = name;
        }

        public SysCon()
        {
        }
    }

}
