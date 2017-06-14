using System;
using System.IO;

namespace SensusPlaylist
{
    public class PlaylistReader : IPlaylistReader
    {
        private const string playlistCommentPrefix = "#";

        public Playlist ReadAll(Stream playlistStream)
        {
            if (playlistStream == null) throw new ArgumentNullException(nameof(playlistStream));

            StreamReader reader = new StreamReader(playlistStream);

            Playlist playlist = ReadPlaylistStream(reader);

            return playlist.Files.Count == 0 ? null : playlist;
        }

        private static Playlist ReadPlaylistStream(StreamReader reader)
        {
            Playlist playlist = new Playlist();
            string playlistLine = null;
            do
            {
                playlistLine = reader.ReadLine();
                if (playlistLine == null) continue;

                if (!playlistLine.StartsWith(playlistCommentPrefix))
                {
                    playlist.Files.Add(playlistLine);
                }
            } while (playlistLine != null);

            return playlist;
        }
    }
}