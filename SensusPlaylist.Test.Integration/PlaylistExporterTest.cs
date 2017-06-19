using Xunit;
using NSubstitute;

using System;
using System.IO;
using System.Collections.Generic;

namespace SensusPlaylist.Test.Integration
{
    public class PlaylistExporterTest
    {
        private IFileSystem _fileSystem;
        private IPlaylistReader _playlistReader;
        IPlaylistWriter _playlistWriter;

        public PlaylistExporterTest()
        {
            _fileSystem = new FileSystem();
            _playlistReader = new PlaylistReader();
        }

        [Fact]
        public void Export_CorrectInputs_Succeeds()
        {
            _playlistWriter = Substitute.For<IPlaylistWriter>();

            PlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader,
                _playlistWriter);

            sut.Export(".\\TestData\\Laptop.m3u", "C:\\temp\\output",
                "C:\\Users\\jfox\\Music\\iTunes\\iTunes Media\\Music");
        }

        [Fact]
        public void Export_FilesAndPlaylist_Succeeds()
        {
            IPlaylistWriter playlistWriter = new PlaylistWriter(
                new SensusPlaylistFormatter(_fileSystem,
                 "C:\\Users\\jfox\\Music\\iTunes\\iTunes Media\\Music"));

            PlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader, _playlistWriter);

            sut.Export(".\\TestData\\Laptop.m3u", "C:\\temp\\output",
                "C:\\Users\\jfox\\Music\\iTunes\\iTunes Media\\Music", true);
        }
    }
}