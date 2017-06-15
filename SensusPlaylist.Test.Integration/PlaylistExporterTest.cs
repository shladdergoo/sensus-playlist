using Xunit;

using System;
using System.IO;
using System.Collections.Generic;

namespace SensusPlaylist.Test.Integration
{
    public class PlaylistExporterTest
    {
        private IFileSystem _fileSystem;
        private IPlaylistReader _playlistReader;

        public PlaylistExporterTest()
        {
            _fileSystem = new FileSystem();
            _playlistReader = new PlaylistReader();
        }

        [Fact]
        public void Export_CorrectInputs_Succeeds()
        {
            PlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader);

            sut.Export(".\\TestData\\Laptop.m3u", "C:\\temp\\output",
                "C:\\Users\\jfox\\Music\\iTunes\\iTunes Media\\Music");
        }

       [Fact]
        public void Export_FilesAndPlaylist_Succeeds()
        {
            PlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader);

            sut.Export(".\\TestData\\Laptop.m3u", "C:\\temp\\output",
                "C:\\Users\\jfox\\Music\\iTunes\\iTunes Media\\Music");
        }
    }
}