namespace SensusPlaylist
{
    public interface IPlaylistExporter
    {
        void Export(string filename, string outputDirectory, string libraryRoot);
    }
}