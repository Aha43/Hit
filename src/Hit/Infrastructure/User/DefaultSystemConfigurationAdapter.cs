using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Hit.Infrastructure.User
{
    public abstract class DefaultSystemConfigurationAdapter<World> : SystemConfigurationAdapter<World>
    {
        private string _jsonPath;
        private Assembly _assembly;

        protected void UserSecretForClient(object client) => _assembly = client?.GetType().Assembly;

        protected void LoadFromJsonFile(string path) => _jsonPath = path;

        public override IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder();
                
            if (!string.IsNullOrWhiteSpace(_jsonPath))
            {
                builder.AddJsonFile(_jsonPath, true);
            }

            if (_assembly != null)
            {
                builder.AddUserSecrets(_assembly, true);
            }

            var retVal = builder.Build();
            return retVal;
        }

    }

}
