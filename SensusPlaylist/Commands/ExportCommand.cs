using Microsoft.Extensions.CommandLineUtils;

using System;

namespace SensusPlaylist
{
    class ExportCommand
    {
        const string HelpOptionTemplate = "-? | -h | --h | --help";

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

            CommandOption suppressPlaylistContents = command.Option(
                "-s | --s | --suppressPlaylistContents", "Suppress Playlist Contents", CommandOptionType.NoValue);

            command.HelpOption(HelpOptionTemplate);

            command.OnExecute(() =>
            {
                if (filename.Value != null && outputDirectory != null)
                {
                    ServiceProvider.GetService<IPlaylistExporter>().Export(filename.Value,
                        outputDirectory.Value, Configuration.Config.LibraryRoot,
                        GetExportMode(exportPlaylistFile, suppressPlaylistContents));
                }
                else
                {
                    command.ShowHelp();
                }
                return 0;
            });
        }

        private static ExportMode GetExportMode(CommandOption playlistFileOption,
            CommandOption suppressContentsOption)
        {
            ExportMode exportMode = ExportMode.None;

            if (!suppressContentsOption.HasValue())
            {
                exportMode |= ExportMode.PlaylistContents;
            }

            if (playlistFileOption.HasValue())
            {
                exportMode |= ExportMode.PlaylistFile;
            }
            return exportMode;
        }
    }
}