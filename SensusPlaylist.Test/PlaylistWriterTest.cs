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

        private static readonly char OSDirSep = Path.DirectorySeparatorChar;
        private readonly string SensusNewLine = "\r\n";

        public PlaylistWriterTest()
        {
            _formatter = Substitute.For<IPlaylistFormatter>();
        }

        [Fact]
        public void Ctor_FormatterNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                IPlaylistWriter sut = new PlaylistWriter(null);
            });
        }

        [Fact]
        public void WriteAll_PlaylistNull_ThrowsException()
        {
            PlaylistWriter sut = new PlaylistWriter(_formatter);

            Assert.Throws<ArgumentNullException>(() => sut.WriteAll(null, new MemoryStream()));
        }

        [Fact]
        public void WriteAll_OutputStreamNull_ThrowsException()
        {
            PlaylistWriter sut = new PlaylistWriter(_formatter);

            Assert.Throws<ArgumentNullException>(() => sut.WriteAll(GetTestPlaylist(0), null));
        }

        [Fact]
        public void WriteAll_PlaylistHasNoFiles_WritesNothing()
        {
            Stream output = new MemoryStream();

            PlaylistWriter sut = new PlaylistWriter(_formatter);

            sut.WriteAll(GetTestPlaylist(0), output);

            Assert.Equal(0, output.Length);
        }

        [Fact]
        public void WriteAll_PlaylistHasFiles_WritesFiles()
        {
            _formatter.FormatPlaylistFile(Arg.Any<string>(), Arg.Any<string>()).Returns($"somme{OSDirSep}file");

            const int FileCount = 3;

            Stream output = new MemoryStream();

            PlaylistWriter sut = new PlaylistWriter(_formatter);

            sut.WriteAll(GetTestPlaylist(FileCount), output);

            Assert.NotEqual(0, output.Length);
            Assert.Equal(FileCount, GetOutputFileCount(output));
        }

        [Fact]
        public void WriteAll_Writes_UsesSensusLineEndings()
        {
            const int FileCount = 2;

            Stream output = new MemoryStream();

            PlaylistWriter sut = new PlaylistWriter(_formatter);

            sut.WriteAll(GetTestPlaylist(FileCount), output);

            string outputString = GetOutput(output);
            string[] lines = outputString.Split(SensusNewLine);

            Assert.NotEqual(0, lines.Length);
        }

        [Fact]
        public void WriteAll_Writes_EndsWithNewLine()
        {
            _formatter.FormatPlaylistFile(Arg.Any<string>(), Arg.Any<string>()).Returns($"somme{OSDirSep}file");

            const int FileCount = 3;

            Stream output = new MemoryStream();

            PlaylistWriter sut = new PlaylistWriter(_formatter);

            sut.WriteAll(GetTestPlaylist(FileCount), output);

            Assert.Equal(FileCount + 1, GetOutputLineCount(output));
            output.Position = 0;
            Assert.EndsWith(string.Empty, GetOutput(output));
        }

        private static Playlist GetTestPlaylist(int fileCount)
        {
            List<string> playlistFiles = new List<string>();

            for (int i = 0; i < fileCount; i++)
            {
                playlistFiles.Add($"{OSDirSep}someLibrary{OSDirSep}someFolder{OSDirSep}someFile{i}.m4a");
            }

            return new Playlist("somePlaylist", "someLibraryRoot", playlistFiles);
        }

        private int GetOutputFileCount(Stream output)
        {
            int count = 0;

            StreamReader reader = new StreamReader(output);

            string line;
            do
            {
                line = reader.ReadLine();

                if (line != null && line != string.Empty && !line.Equals(SensusNewLine))
                {
                    count++;
                }
            } while (line != null);

            return count;
        }

        private int GetOutputLineCount(Stream output)
        {
            int count = 0;

            StreamReader reader = new StreamReader(output);

            while (reader.ReadLine() != null)
            {
                count++;
            }

            return count;
        }

        private static string GetOutput(Stream output)
        {
            StreamReader reader = new StreamReader(output);

            return reader.ReadToEnd();
        }
    }
}