using Hit.Infrastructure.Attributes;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;

namespace Hit.Infrastructure.User
{
    public abstract class DefaultSystemConfigurationAdapter<World> : SystemConfigurationAdapter<World>
    {
        private string _jsonPath;
        private Assembly _assembly;

        protected void UserSecretForClient(object client) => _assembly = client?.GetType().Assembly;

        protected void LoadFromJsonFile(string path) => _jsonPath = path;

        public override IConfiguration GetConfiguration(SysCon meta) => GetPartConfiguration(meta);

        private IConfiguration GetPartConfiguration(SysCon meta)
        {
            var builder1 = new ConfigurationBuilder();
            var builder2 = default(ConfigurationBuilder);

            if (!string.IsNullOrWhiteSpace(_jsonPath))
            {
                builder1.AddJsonFile(_jsonPath, true);
            }

            if (_assembly != default)
            {
                builder1.AddUserSecrets(_assembly, true);
            }

            if (!string.IsNullOrWhiteSpace(meta.ConfigurationSections))
            {
                var sections = meta.ConfigurationSections.Split(',').Select(s => s.Trim()).ToArray();

                if (sections != null && sections.Length > 0)
                {
                    var configuration1 = builder1.Build();
                    foreach (var section in sections)
                    {
                        if (!string.IsNullOrWhiteSpace(section))
                        {
                            var sectionConfiguration = configuration1.GetSection(section);
                            if (sectionConfiguration != default)
                            {
                                if (builder2 == default)
                                {
                                    builder2 = new ConfigurationBuilder();
                                }
                                builder2.AddConfiguration(sectionConfiguration);
                            }
                        }
                    }
                }
            }
            
            var retVal = (builder2 != default) ? builder2.Build() : builder1.Build();
            return retVal;
        }

    }

}
