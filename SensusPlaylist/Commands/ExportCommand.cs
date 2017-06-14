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

            command.HelpOption(HelpOptionTemplate);

            command.OnExecute(() =>
            {
                if (filename.Value != null && outputDirectory != null)
                {
                    ServiceProvider.GetService<IPlaylistExporter>().Export(filename.Value, 
                        outputDirectory.Value, Configuration.Config.LibraryRoot);
                }
                else
                {
                    command.ShowHelp();
                }
                return 0;
            });
        }
    }
}