using System;
using System.IO;

namespace SensusPlaylist
{
    public class PlaylistReader: IPlaylistReader
    {
        public Playlist ReadAll(Stream playlist)
        {
            if(playlist == null) throw new ArgumentNullException(nameof(playlist));

            return new Playlist();
        }
    }
}