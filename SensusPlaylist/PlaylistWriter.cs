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

            if (playlist.Files == null || playlist.Files.Count == 0) return;

            StreamWriter writer = new StreamWriter(_outputStream);

            foreach(string filename in playlist.Files)
            {
                writer.WriteLine(filename);
            }

            writer.Flush();
            _outputStream.Position = 0;
        }
    }
}