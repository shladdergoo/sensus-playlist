using System;
using System.IO;

namespace SensusPlaylist
{
    public class PlaylistWriter : IPlaylistWriter
    {
        private Stream _outputStream;

        public PlaylistWriter(Stream outputStream)
        {
            if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));

            _outputStream = outputStream;
        }

        public void WriteAll(Playlist playlist)
        {
            if (playlist == null) throw new ArgumentNullException(nameof(playlist));

            throw new NotImplementedException();
        }
    }
}