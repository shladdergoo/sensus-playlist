using Xunit;
using NSubstitute;

using System;

namespace SensusPlaylist.Test
{
    public class SensusPlaylistFormatterTest
    {
        private IFileSystem _fileSystem;

        public SensusPlaylistFormatterTest()
        {
            _fileSystem = Substitute.For<IFileSystem>();
        }

        [Fact]
        public void Ctor_FileSystemNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                SensusPlaylistFormatter sut = new SensusPlaylistFormatter(null);
            });
        }

        [Fact]
        public void FormatPlaylistFile_FilenameNull_ThrowsException()
        {
            IPlaylistFormatter sut = new SensusPlaylistFormatter(_fileSystem);

            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.FormatPlaylistFile(null, "someLibraryRoot");
            });
        }

        [Fact]
        public void FormatPlaylistFile_LibraryRootNull_ThrowsException()
        {
            IPlaylistFormatter sut = new SensusPlaylistFormatter(_fileSystem);

            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.FormatPlaylistFile("someFile", null);
            });
        }
    }
}