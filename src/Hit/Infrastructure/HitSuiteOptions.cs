using Microsoft.Extensions.DependencyInjection;

namespace Hit.Infrastructure
{
    public class HitSuiteOptions
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string EnvironmentType { get; set; }
        public IServiceCollection Services { get; }

        internal HitSuiteOptions()
        {
            Services = new ServiceCollection();
        }

    }

}
