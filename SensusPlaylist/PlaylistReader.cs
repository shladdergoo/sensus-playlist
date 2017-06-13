using System;
using System.IO;

namespace SensusPlaylist
{
    public class PlaylistReader: IPlaylistReader
    {
        public Playlist ReadAll(Stream playlist)
        {
            return new Playlist();
        }
    }
}