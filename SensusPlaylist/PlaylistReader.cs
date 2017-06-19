using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SensusPlaylist
{
    public class PlaylistReader : IPlaylistReader
    {
        private const string playlistCommentPrefix = "#";

        public Playlist ReadAll(Stream playlistStream, string playlistName)
        {
            if (playlistStream == null) throw new ArgumentNullException(nameof(playlistStream));

            StreamReader reader = new StreamReader(playlistStream);

            Playlist playlist = ReadPlaylistStream(reader, playlistName);

            return playlist.Files.Any() ? playlist : null;
        }

        private static Playlist ReadPlaylistStream(StreamReader reader, string playlistName)
        {
            List<string> playlistFiles = new List<string>();

            string playlistLine = null;
            do
            {
                playlistLine = reader.ReadLine();
                if (playlistLine == null) continue;

                if (!playlistLine.StartsWith(playlistCommentPrefix))
                {
                    playlistFiles.Add(playlistLine);
                }
            } while (playlistLine != null);

            return new Playlist(playlistName, playlistFiles);
        }
    }
}