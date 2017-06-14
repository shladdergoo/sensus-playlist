using Xunit;

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
            _fileSystem = new FileSystem();
            _playlistReader = new PlaylistReader();
        }

        [Fact]
        public void Export_CorrectInputs_Succeeds()
        {
            PlaylistExporter sut = new PlaylistExporter(_fileSystem, _playlistReader);

            Assert.True(1 == 2);
        }
    }
}