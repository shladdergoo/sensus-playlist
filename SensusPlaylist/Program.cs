using Microsoft.Extensions.CommandLineUtils;

namespace SensusPlaylist
{
    class Program
    {
        const string HelpOptionTemplate = "-? | -h | --help";
        static void Main(string[] args)
        {
            ServiceProvider.Build();

            Configuration.Build();

            CommandLineApplication commandLineApplication =
               new CommandLineApplication(false);

            commandLineApplication.Command("export", ExportCommand.Configure);
            commandLineApplication.HelpOption(HelpOptionTemplate);
            commandLineApplication.Execute(args);
        }
    }
}
