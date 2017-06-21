using System.IO;

namespace SensusPlaylist
{
    public interface IPlaylistReader
    {
         Playlist ReadAll(Stream playlist, string playlistName, string libraryRoot);
    }
}