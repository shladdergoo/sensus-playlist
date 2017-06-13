using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;

namespace SensusPlaylist
{
    static class ServiceProvider
    {
        private static IServiceProvider _serviceProvider;

        public static void Build()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IFileSystem, FileSystem>()
                .AddSingleton<IPlaylistExporter, PlaylistExporter>()
                .BuildServiceProvider();

            ConfigureLogging();
        }

        public static T GetService<T>()
        {
            if (_serviceProvider == null)
            {
                Build();
            }

            return _serviceProvider.GetService<T>();
        }

        public static ILogger GetLogger<T>()
        {
            if (_serviceProvider == null)
            {
                Build();
            }

            return _serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<T>();
        }

        private static void ConfigureLogging()
        {
            _serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);
        }
    }
}