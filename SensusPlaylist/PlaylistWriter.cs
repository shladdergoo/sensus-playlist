using System;
using System.IO;
using System.Linq;

namespace SensusPlaylist
{
    public class PlaylistWriter : IPlaylistWriter
    {
        private Stream _outputStream;
        private IPlaylistFormatter _formatter;

        public PlaylistWriter(Stream outputStream, IPlaylistFormatter formatter)
        {
            if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            
            _outputStream = outputStream;
            _formatter = formatter;
        }

        public void WriteAll(Playlist playlist)
        {
            if (playlist == null) throw new ArgumentNullException(nameof(playlist));

            if (playlist.Files == null || !playlist.Files.Any()) return;

            StreamWriter writer = new StreamWriter(_outputStream);

            foreach(string filename in playlist.Files)
            {
                writer.WriteLine(_formatter.FormatPlaylistFile(filename));
            }

            writer.Flush();
            _outputStream.Position = 0;
        }
    }
}