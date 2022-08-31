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
        public bool UserSecrets { get; set; }
        public string ConfigurationSections { get; set; }
        public SysCon(string system)
        {
            if (string.IsNullOrWhiteSpace(system))
            {
                throw new ArgumentException("missing " + nameof(system));
            }

            System = system;
        }
    }

}
