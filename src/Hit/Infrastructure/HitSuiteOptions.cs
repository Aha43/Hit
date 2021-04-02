using Microsoft.Extensions.DependencyInjection;

namespace Hit.Infrastructure
{
    public class HitSuiteOptions
    {
        public IServiceCollection Services { get; }

        internal HitSuiteOptions()
        {
            Services = new ServiceCollection();
        }

    }

}
