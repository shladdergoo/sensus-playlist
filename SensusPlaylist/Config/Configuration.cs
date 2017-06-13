using Microsoft.Extensions.Configuration;

using System;
using System.IO;

namespace SensusPlaylist
{
    static class Configuration
    {
        private static IConfigurationRoot _configuration;

        const string ConfigFile = "appsettings.json";

        public static void Build()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ConfigFile);

            _configuration = builder.Build();
        }

        public static AppConfig Config
        {
            get
            {
                if (_configuration == null) throw new Exception("Configuration has not been built");

                return new AppConfig(_configuration);
            }
        }
    }
}