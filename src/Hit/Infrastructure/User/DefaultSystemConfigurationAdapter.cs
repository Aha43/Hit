using Microsoft.Extensions.Configuration;

namespace Hit.Infrastructure.User
{
    public abstract class DefaultSystemConfigurationAdapter<World> : SystemConfigurationAdapter<World>
    {
        private readonly string _jsonPath;

        public DefaultSystemConfigurationAdapter() => _jsonPath = null;
        public DefaultSystemConfigurationAdapter(string jsonPath) => _jsonPath = jsonPath;

        public override IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<DefaultSystemConfigurationAdapter<World>>();
                
            if (!string.IsNullOrWhiteSpace(_jsonPath))
            {
                builder.AddJsonFile(_jsonPath, true);
            }

            var retVal = builder.Build();
            return retVal;
        }

    }

}
