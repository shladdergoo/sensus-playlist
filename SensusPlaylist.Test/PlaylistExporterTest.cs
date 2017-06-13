using Xunit;
using NSubstitute;

using System;
using System.IO;
using System.Collections.Generic;

namespace SensusPlaylist.Test
{
    public class PlaylistExporterTest
    {
        private IFileSystem _fileSystem;
        private IPlaylistReader _playlistReader;

        public PlaylistExporterTest()
        {
            _fileSystem = Substitute.For<IFileSystem>();
            _playlistReader = Substitute.For<IPlaylistReader>();
        }

        [Fact]
        public void Export_FileNotFound_ThrowsException()
        {
            _fileSystem.FileExists(Arg.Any<string>()).Returns(false);

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader);

            Assert.Throws<FileNotFoundException>(() => sut.Export("someFile", "someOutputDir"));
        }

        [Fact]
        public void Export_FileFound_ReadsFile()
        {
            _fileSystem.FileExists(Arg.Any<string>()).Returns(true);

            _playlistReader.ReadAll(Arg.Any<Stream>()).Returns(new Playlist());

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader);

            sut.Export("someFile", "someOutputDir");

            _playlistReader.Received(1).ReadAll(Arg.Any<Stream>());
        }

        [Fact]
        public void Export_OutputDirectoryNotFound_CreatesDirectory()
        {
            _fileSystem.FileExists(Arg.Any<string>()).Returns(true);

            _fileSystem.DirectoryExists(Arg.Is<string>("someOutputDir")).Returns(false);

            _playlistReader.ReadAll(Arg.Any<Stream>()).Returns(new Playlist());

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader);

            sut.Export("someFile", "someOutputDir");

            _fileSystem.Received(1).CreateDirectory(Arg.Any<string>());
        }

        [Fact]
        public void Export_OutputDirectoryFound_CleansDirectory()
        {
            _fileSystem.FileExists(Arg.Any<string>()).Returns(true);

            _fileSystem.DirectoryExists(Arg.Is<string>("someOutputDir")).Returns(true);

            _playlistReader.ReadAll(Arg.Any<Stream>()).Returns(new Playlist());

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader);

            sut.Export("someFile", "someOutputDir");

            _fileSystem.Received(1).CleanDirectory(Arg.Any<string>());
        }

        [Fact]
        public void Export_PlaylistRead_AllFilesProcessed()
        {
            const int PlaylistTracks = 5;

            _fileSystem.FileExists(Arg.Any<string>()).Returns(true);
            _fileSystem.DirectoryExists(Arg.Is<string>("someOutputDir")).Returns(true);
            _fileSystem.GetParentDirectory(Arg.Any<string>()).Returns("C:\\someParent");
            _fileSystem.GetShortName(Arg.Is<string>("C:\\someParent\\someFile")).Returns("someFile");
            _fileSystem.GetShortName(Arg.Is<string>("C:\\someParent")).Returns("someParent");

            _playlistReader.ReadAll(Arg.Any<Stream>()).Returns(GetTestPlaylist(PlaylistTracks));

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader);

            sut.Export("someFile", "someOutputDir");

            _fileSystem.Received(PlaylistTracks).FileCopy(Arg.Is<string>("C:\\someParent\\someFile"), 
                Arg.Is<string>("someOutputDir\\someParent\\someFile"), Arg.Any<bool>());
        }

        private Playlist GetTestPlaylist(int playlistTracks)
        {
            List<string> files = new List<string>();
            for (int i = 0; i < playlistTracks; i++)
            {
                files.Add("C:\\someParent\\someFile");
            }

            return new Playlist(files);
        }
    }
}