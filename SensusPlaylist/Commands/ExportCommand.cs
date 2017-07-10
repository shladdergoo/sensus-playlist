using Microsoft.Extensions.CommandLineUtils;

using System;

namespace SensusPlaylist
{
    class ExportCommand
    {
        const string HelpOptionTemplate = "-? | -h | --help";

        public static void Configure(CommandLineApplication command)
        {
            CommandArgument filename = command.Argument(
                "filename",
                "Enter the name and path of the iTunes playlist file");

            CommandArgument outputDirectory = command.Argument(
                "outputDirectory",
                "Enter the path of the output directory");

            CommandOption exportPlaylistFile = command.Option(
                "-e | --e | --exportPlaylistFile", "Export playlist file", CommandOptionType.NoValue);

            command.HelpOption(HelpOptionTemplate);

            command.OnExecute(() =>
            {
                if (filename.Value != null && outputDirectory != null)
                {
                    ServiceProvider.GetService<IPlaylistExporter>().Export(filename.Value,
                        outputDirectory.Value, Configuration.Config.LibraryRoot,
                        GetExportMode(exportPlaylistFile));
                }
                else
                {
                    command.ShowHelp();
                }
                return 0;
            });
        }

        private static ExportMode GetExportMode(CommandOption playlistFileOption)
        {
            ExportMode exportMode = ExportMode.PlaylistContents;

            if(playlistFileOption.HasValue()) exportMode |= ExportMode.PlaylistFile;

            return exportMode;
        }
    }
}