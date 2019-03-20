using System;
using System.IO;
using System.Linq;

namespace SensusPlaylist
{
    public class PlaylistWriter : IPlaylistWriter
    {
        private readonly IPlaylistFormatter _formatter;

        private readonly string SensusNewLine = "\r\n";

        public PlaylistWriter(IPlaylistFormatter formatter)
        {
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            
            _formatter = formatter;
        }

        public void WriteAll(Playlist playlist, Stream outputStream)
        {
            if (playlist == null) throw new ArgumentNullException(nameof(playlist));
            if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));

            if (playlist.Files == null || !playlist.Files.Any()) return;

            StreamWriter writer = new StreamWriter(outputStream);
            writer.NewLine = SensusNewLine;

            foreach(string filename in playlist.Files)
            {
                writer.WriteLine(_formatter.FormatPlaylistFile(filename, playlist.LibraryRoot));
            }
            writer.WriteLine();

            writer.Flush();
            outputStream.Position = 0;
        }
    }
}