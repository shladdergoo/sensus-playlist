using Xunit;
using NSubstitute;

using System;
using System.IO;

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

        [Fact] void FormatPlaylistFile_GetsFile_CorrectlyFormats()
        {
            _fileSystem.GetRelativePath(Arg.Is<string>("C:\\Users\\jfox\\Music\\iTunes\\iTunes Media\\Music\\Jamie T\\Trick\\01 Tinfoil Boy.m4a"), 
                Arg.Is<string>("C:\\Users\\jfox\\Music\\iTunes\\iTunes Media\\Music"))
                .Returns("Jamie T\\Trick\\01 Tinfoil Boy.m4a");

            IPlaylistFormatter sut = new SensusPlaylistFormatter(_fileSystem);

            string result = sut.FormatPlaylistFile("C:\\Users\\jfox\\Music\\iTunes\\iTunes Media\\Music\\Jamie T\\Trick\\01 Tinfoil Boy.m4a", 
                "C:\\Users\\jfox\\Music\\iTunes\\iTunes Media\\Music");

            Uri resultUri = new Uri(result, UriKind.Relative);

            Assert.False(result.StartsWith(Path.DirectorySeparatorChar.ToString()));
        }
    }
}