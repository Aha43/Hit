using Hit.Infrastructure.Attributes;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace Hit.Infrastructure.User
{
    public abstract class DefaultSystemConfigurationAdapter<World> : SystemConfigurationAdapter<World>
    {
        public override IConfiguration GetConfiguration(SysCon meta)
        {
            var jsonPath = meta.JsonPath;

            var builder1 = new ConfigurationBuilder();
            var builder2 = default(ConfigurationBuilder);

            if (!string.IsNullOrWhiteSpace(jsonPath))
            {
                if (!File.Exists(jsonPath))
                {
                    throw new Exception($"Configuration file {jsonPath} do not exists");
                }
                builder1.AddJsonFile(jsonPath);
            }

            if (meta.UserSecrets)
            {
                builder1.AddUserSecrets(GetType().Assembly, false);
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
