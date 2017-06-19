using Xunit;
using NSubstitute;

using System;
using System.IO;
using System.Collections.Generic;

namespace SensusPlaylist.Test
{
    public class PlaylistWriterTest
    {
        IPlaylistFormatter _formatter;

        public PlaylistWriterTest()
        {
            _formatter = Substitute.For<IPlaylistFormatter>();
        }

        [Fact]
        public void WriteAll_PlaylistNull_ThrowsException()
        {
            PlaylistWriter sut = new PlaylistWriter(new MemoryStream(),
                _formatter);

            Assert.Throws<ArgumentNullException>(() => sut.WriteAll(null));
        }

        [Fact]
        public void WriteAll_PlaylistHasNoFiles_WritesNothing()
        {
            Stream output = new MemoryStream();

            PlaylistWriter sut = new PlaylistWriter(output, _formatter);

            sut.WriteAll(GetTestPlaylist(0));

            Assert.Equal(0, output.Length);
        }

        [Fact]
        public void WriteAll_PlaylistHasFiles_WritesFiles()
        {
            const int FileCount = 3;

            Stream output = new MemoryStream();

            PlaylistWriter sut = new PlaylistWriter(output, _formatter);

            sut.WriteAll(GetTestPlaylist(FileCount));

            Assert.NotEqual(0, output.Length);
            Assert.Equal(FileCount, GetOutputFileCount(output));
        }

        private static Playlist GetTestPlaylist(int fileCount)
        {
            Playlist playlist = new Playlist();

            for (int i = 0; i < fileCount; i++)
            {
                playlist.Files.Add($"C:\\someLibrary\\someFolder\\someFile{i}.m4a");
            }

            return playlist;
        }

        private static int GetOutputFileCount(Stream output)
        {
            int count = 0;

            StreamReader reader = new StreamReader(output);

            while(reader.ReadLine() != null)
            {
                count++;
            }

            return count;
        }
    }
}