namespace SensusPlaylist
{
    public interface IPlaylistFormatter
    {
         string FormatPlaylistFile(string filename, string libraryRoot);
    }
}