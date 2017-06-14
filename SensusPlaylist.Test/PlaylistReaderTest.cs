using Xunit;

using System;
using System.IO;

namespace SensusPlaylist.Test
{
    public class PlaylistReaderTest
    {
        [Fact]
        public void ReadAll_PlaylistNull_ThrowsException()
        {
            IPlaylistReader sut = new PlaylistReader();

            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.ReadAll(null);
            });
        }

        [Fact]
        public void ReadAll_PlayListHasFilesAndComments_ReturnsFiles()
        {
            IPlaylistReader sut = new PlaylistReader();

            Stream playlistStream = GetTestPlaylistStreamFilesAndComments();

            Playlist result = sut.ReadAll(playlistStream);

            Assert.Equal(2, result.Files.Count);
        }

        [Fact]
        public void ReadAll_PlayListHasOnlyFiles_ReturnsFiles()
        {
            IPlaylistReader sut = new PlaylistReader();

            Stream playlistStream = GetTestPlaylistStreamOnlyFiles();

            Playlist result = sut.ReadAll(playlistStream);

            Assert.Equal(2, result.Files.Count);
        }

        [Fact]
        public void ReadAll_PlayListHasOnlyComments_ReturnsNull()
        {
            IPlaylistReader sut = new PlaylistReader();

            Stream playlistStream = GetTestPlaylistStreamOnlyComments();

            Playlist result = sut.ReadAll(playlistStream);

            Assert.Null(result);
        }

        [Fact]
        public void ReadAll_PlayListEmpty_ReturnsNull()
        {
            IPlaylistReader sut = new PlaylistReader();

            Stream playlistStream = new MemoryStream();

            Playlist result = sut.ReadAll(playlistStream);

            Assert.Null(result);
        }

        private Stream GetTestPlaylistStreamFilesAndComments()
        {
            string playlistString = "#EXTM3U" + "\r\n" +
                    "#EXTINF:255,Simple Song - The Shins" + "\r\n" +
                    "D:\\shlad\\Music\\iTunes\\Music\\The Shins\\Port Of Morrow\\02 Simple Song.m4a" + "\r\n" +
                    "#EXTINF:286,Roscoe - Midlake" + "\r\n" +
                    "D:\\shlad\\Music\\iTunes\\Music\\Midlake\\The Trials Of Van Occupanther\01 Roscoe.m4a";

            Stream playlistStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(playlistStream);
            writer.Write(playlistString);
            writer.Flush();
            playlistStream.Position = 0;

            return playlistStream;
        }

        private Stream GetTestPlaylistStreamOnlyFiles()
        {
            string playlistString =
                    "D:\\shlad\\Music\\iTunes\\Music\\The Shins\\Port Of Morrow\\02 Simple Song.m4a" + "\r\n" +
                    "D:\\shlad\\Music\\iTunes\\Music\\Midlake\\The Trials Of Van Occupanther\01 Roscoe.m4a";

            Stream playlistStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(playlistStream);
            writer.Write(playlistString);
            writer.Flush();
            playlistStream.Position = 0;

            return playlistStream;
        }

        private Stream GetTestPlaylistStreamOnlyComments()
        {
            string playlistString = "#EXTM3U" + "\r\n" +
                    "#EXTINF:255,Simple Song - The Shins" + "\r\n" +
                    "#EXTINF:286,Roscoe - Midlake";

            Stream playlistStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(playlistStream);
            writer.Write(playlistString);
            writer.Flush();
            playlistStream.Position = 0;

            return playlistStream;
        }
    }
}