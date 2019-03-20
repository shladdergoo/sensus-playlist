using Xunit;
using NSubstitute;

using System;
using System.IO;
using System.Collections.Generic;

namespace SensusPlaylist.Test
{
    public class PlaylistExporterTest
    {
        private readonly IFileSystem _fileSystem;
        private readonly IPlaylistReader _playlistReader;
        private readonly IPlaylistWriter _playlistWriter;

        private readonly char OSDirSep = Path.DirectorySeparatorChar;

        public PlaylistExporterTest()
        {
            _fileSystem = Substitute.For<IFileSystem>();
            _playlistReader = Substitute.For<IPlaylistReader>();
            _playlistWriter = Substitute.For<IPlaylistWriter>();
        }

        [Fact]
        public void Ctor_FilesystemNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                IPlaylistExporter sut = new PlaylistExporter(null, _playlistReader, _playlistWriter);
            });
        }

        [Fact]
        public void Ctor_PlaylistReaderNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                IPlaylistExporter sut = new PlaylistExporter(_fileSystem, null, _playlistWriter);
            });
        }

        [Fact]
        public void Ctor_PlaylistWriterNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, null);
            });
        }

        [Fact]
        public void Export_FilenameNull_ThrowsException()
        {
            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            Assert.Throws<ArgumentNullException>(() => sut.Export(null, "someOutputDir", "somelibraryRoot",
                ExportMode.None));
        }

        [Fact]
        public void Export_OutputDirNull_ThrowsException()
        {
            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            Assert.Throws<ArgumentNullException>(() => sut.Export("someFile", null, "somelibraryRoot",
                ExportMode.None));
        }

        [Fact]
        public void Export_LibraryRootNull_ThrowsException()
        {
            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            Assert.Throws<ArgumentNullException>(() => sut.Export("someFile", "someOutputDir", null,
                ExportMode.None));
        }

        [Fact]
        public void Export_FileNotFound_ThrowsException()
        {
            _fileSystem.FileExists(Arg.Any<string>()).Returns(false);

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            Assert.Throws<FileNotFoundException>(() => sut.Export("someFile", "someOutputDir", "somelibraryRoot",
                ExportMode.None));
        }

        [Fact]
        public void Export_ExportModeNone_DoesNothing()
        {
            _fileSystem.FileExists(Arg.Any<string>()).Returns(true);

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            sut.Export("someFile", "someOutputDir", "somelibraryRoot",
                ExportMode.None);

            _playlistReader.DidNotReceive().ReadAll(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void Export_FileFound_ReadsFile()
        {
            _fileSystem.FileExists(Arg.Any<string>()).Returns(true);

            _playlistReader.ReadAll(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(GetTestPlaylist(0));

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            sut.Export("someFile", "someOutputDir", "somelibraryRoot",
                ExportMode.PlaylistContents);

            _playlistReader.Received(1).ReadAll(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public void Export_OutputDirectoryNotFound_CreatesDirectory()
        {
            _fileSystem.FileExists(Arg.Any<string>()).Returns(true);

            _fileSystem.DirectoryExists(Arg.Is<string>("someOutputDir")).Returns(false);

            _playlistReader.ReadAll(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(GetTestPlaylist(0));

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            sut.Export("someFile", "someOutputDir", "somelibraryRoot",
                ExportMode.PlaylistContents);

            _fileSystem.Received(1).CreateDirectory(Arg.Any<string>());
        }

        [Fact]
        public void Export_OutputDirectoryFound_CleansDirectory()
        {
            _fileSystem.FileExists(Arg.Any<string>()).Returns(true);

            _fileSystem.DirectoryExists(Arg.Is<string>("someOutputDir")).Returns(true);

            _playlistReader.ReadAll(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(GetTestPlaylist(0));

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            sut.Export("someFile", "someOutputDir", "somelibraryRoot",
                ExportMode.PlaylistContents);

            _fileSystem.Received(1).CleanDirectory(Arg.Any<string>());
        }

        [Fact]
        public void Export_OutputDirectoryDoesntExist_CreatesOutputDirectory()
        {
            const int PlaylistTracks = 5;

            _fileSystem.FileExists(Arg.Any<string>()).Returns(true);
            _fileSystem.DirectoryExists(Arg.Is<string>("someOutputDir")).Returns(false);
            _fileSystem.GetRelativePath(Arg.Any<string>(), Arg.Any<string>()).Returns("someFile");
            _fileSystem.GetShortName(Arg.Is<string>($"{OSDirSep}someParent{OSDirSep}someFile")).Returns("someFile");

            _playlistReader.ReadAll(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(GetTestPlaylist(PlaylistTracks));

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            sut.Export($"{OSDirSep}someParent{OSDirSep}someFile", "someOutputDir", $"{OSDirSep}someParent",
                ExportMode.PlaylistContents);

            _fileSystem.Received().CreateDirectory(Arg.Any<string>());
        }

        [Fact]
        public void Export_PlaylistRead_AllFilesProcessed()
        {
            const int PlaylistTracks = 5;

            _fileSystem.FileExists(Arg.Any<string>()).Returns(true);
            _fileSystem.DirectoryExists(Arg.Is<string>("someOutputDir")).Returns(true);
            _fileSystem.GetRelativePath(Arg.Is<string>($"{OSDirSep}someParent{OSDirSep}someFile"),
                Arg.Is<string>($"{OSDirSep}someParent")).Returns("someFile");
            _fileSystem.GetRelativePath(Arg.Is<string>($"{OSDirSep}someParent"),
                Arg.Is<string>($"{OSDirSep}someParent")).Returns("");
            _fileSystem.GetDirectory(Arg.Is<string>($"{OSDirSep}someParent{OSDirSep}someFile")).Returns($"{OSDirSep}someParent");
            _fileSystem.GetShortName(Arg.Is<string>($"{OSDirSep}someParent{OSDirSep}someFile")).Returns("someFile");

            _playlistReader.ReadAll(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(GetTestPlaylist(PlaylistTracks));

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            sut.Export($"{OSDirSep}someParent{OSDirSep}someFile", "someOutputDir", $"{OSDirSep}someParent",
                ExportMode.PlaylistContents);

            _fileSystem.Received(PlaylistTracks).FileCopy(Arg.Is<string>($"{OSDirSep}someParent{OSDirSep}someFile"),
                Arg.Is<string>($"someOutputDir{OSDirSep}someFile"), Arg.Any<bool>());
        }

        [Fact]
        public void Export_ExportPlaylist_WritesPlaylistFile()
        {
            const int PlaylistTracks = 5;

            _fileSystem.FileExists(Arg.Any<string>()).Returns(true);
            _playlistReader.ReadAll(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(GetTestPlaylist(PlaylistTracks));

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            sut.Export($"{OSDirSep}someParent{OSDirSep}someFile", "someOutputDir", $"{OSDirSep}someParent",
                ExportMode.PlaylistFile | ExportMode.PlaylistContents);

            _playlistWriter.Received().WriteAll(Arg.Any<Playlist>(), Arg.Any<Stream>());
        }

        [Fact]
        public void Export_ExportPlaylistFileOnly_ExportssPlaylistFile()
        {
            const int PlaylistTracks = 5;

            _fileSystem.FileExists(Arg.Any<string>()).Returns(true);
            _playlistReader.ReadAll(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(GetTestPlaylist(PlaylistTracks));

            IPlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            sut.Export($"{OSDirSep}someParent{OSDirSep}someFile", "someOutputDir", $"{OSDirSep}someParent",
                ExportMode.PlaylistFile);

            _playlistWriter.Received().WriteAll(Arg.Any<Playlist>(), Arg.Any<Stream>());
            _fileSystem.DidNotReceive().FileCopy(Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<bool>());
        }
        private Playlist GetTestPlaylist(int playlistTracks)
        {
            List<string> files = new List<string>();
            for (int i = 0; i < playlistTracks; i++)
            {
                files.Add($"{OSDirSep}someParent{OSDirSep}someFile");
            }

            return new Playlist("somePlaylist", "someLibraryRoot", files);
        }
    }
}
