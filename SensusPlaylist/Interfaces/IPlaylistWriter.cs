using System.IO;

namespace SensusPlaylist
{
    public interface IPlaylistWriter
    {
         void WriteAll(Playlist playlist, Stream outputStream);
    }
}