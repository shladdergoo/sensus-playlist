using Microsoft.Extensions.Configuration;

using System;

namespace SensusPlaylist
{
    class AppConfig
    {
        public string LibraryRoot { get; private set; }

        public AppConfig(IConfigurationRoot configuration)
        {
            const string settingLibraryRoot = "libraryRoot";

            LibraryRoot = configuration[settingLibraryRoot];
            if (LibraryRoot == null) throw new Exception($"Could not find config setting '{settingLibraryRoot}'");
        }
    }
}