using Xunit;

using System;
using System.IO;
using System.Linq;

namespace SensusPlaylist.Test
{
    public class PlaylistReaderTest
    {
        private readonly char OSDirSep = Path.DirectorySeparatorChar;

        [Fact]
        public void ReadAll_PlaylistNull_ThrowsException()
        {
            IPlaylistReader sut = new PlaylistReader();

            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.ReadAll(null, "somePlaylist", "someLibraryRoot");
            });
        }

        [Fact]
        public void ReadAll_PlayListHasFilesAndComments_ReturnsFiles()
        {
            IPlaylistReader sut = new PlaylistReader();

            Stream playlistStream = GetTestPlaylistStreamFilesAndComments();

            Playlist result = sut.ReadAll(playlistStream, "somePlaylist", "someLibraryRoot");

            Assert.Equal(2, result.Files.Count());
        }

        [Fact]
        public void ReadAll_PlayListHasOnlyFiles_ReturnsFiles()
        {
            IPlaylistReader sut = new PlaylistReader();

            Stream playlistStream = GetTestPlaylistStreamOnlyFiles();

            Playlist result = sut.ReadAll(playlistStream, "somePlaylist", "someLibraryRoot");

            Assert.Equal(2, result.Files.Count());
        }

        [Fact]
        public void ReadAll_PlayListHasOnlyComments_ReturnsNull()
        {
            IPlaylistReader sut = new PlaylistReader();

            Stream playlistStream = GetTestPlaylistStreamOnlyComments();

            Playlist result = sut.ReadAll(playlistStream, "somePlaylist", "someLibraryRoot");

            Assert.Null(result);
        }

        [Fact]
        public void ReadAll_PlayListEmpty_ReturnsNull()
        {
            IPlaylistReader sut = new PlaylistReader();

            Stream playlistStream = new MemoryStream();

            Playlist result = sut.ReadAll(playlistStream, "somePlaylist", "someLibraryRoot");

            Assert.Null(result);
        }

        private Stream GetTestPlaylistStreamFilesAndComments()
        {
            string playlistString = "#EXTM3U" + "\r\n" +
                    "#EXTINF:255,Simple Song - The Shins" + "\r\n" +
                    $"{OSDirSep}shlad{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}Music{OSDirSep}The Shins{OSDirSep}Port Of Morrow{OSDirSep}02 Simple Song.m4a" + "\r\n" +
                    "#EXTINF:286,Roscoe - Midlake" + "\r\n" +
                    $"{OSDirSep}shlad{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}Music{OSDirSep}Midlake{OSDirSep}The Trials Of Van Occupanther{OSDirSep}01 Roscoe.m4a";

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
                    $"{OSDirSep}shlad{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}Music{OSDirSep}The Shins{OSDirSep}Port Of Morrow{OSDirSep}02 Simple Song.m4a" + "\r\n" +
                    $"{OSDirSep}shlad{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}Music{OSDirSep}Midlake{OSDirSep}The Trials Of Van Occupanther{OSDirSep}01 Roscoe.m4a";

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
