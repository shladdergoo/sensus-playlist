using Xunit;
using NSubstitute;

using System;
using System.IO;

namespace SensusPlaylist.Test
{
    public class SensusPlaylistFormatterTest
    {
        private IFileSystem _fileSystem;

        private readonly char OSDirSep = Path.DirectorySeparatorChar;
        private readonly char SensusDirSep = '\\';

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
                sut.FormatPlaylistFile("someFilename", null);
            });
        }

        [Fact]
        public void FormatPlaylistFile_Formats_GoodUri()
        {
            _fileSystem.GetRelativePath(Arg.Is<string>($"{OSDirSep}Users{OSDirSep}jfox{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}iTunes Media{OSDirSep}Music{OSDirSep}Jamie T{OSDirSep}Trick{OSDirSep}01 Tinfoil Boy.m4a"),
                Arg.Is<string>($"{OSDirSep}Users{OSDirSep}jfox{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}iTunes Media{OSDirSep}Music"))
                .Returns($"Jamie T{OSDirSep}Trick{OSDirSep}01 Tinfoil Boy.m4a");

            IPlaylistFormatter sut = new SensusPlaylistFormatter(_fileSystem);

            string result = sut.FormatPlaylistFile($"{OSDirSep}Users{OSDirSep}jfox{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}iTunes Media{OSDirSep}Music{OSDirSep}Jamie T{OSDirSep}Trick{OSDirSep}01 Tinfoil Boy.m4a",
                $"{OSDirSep}Users{OSDirSep}jfox{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}iTunes Media{OSDirSep}Music");

            Uri resultUri = new Uri(result, UriKind.Relative);   
        }

        [Fact]
        public void FormatPlaylistFile_Formats_NoPreceedingSeparator()
        {
            _fileSystem.GetRelativePath(Arg.Is<string>($"{OSDirSep}Users{OSDirSep}jfox{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}iTunes Media{OSDirSep}Music{OSDirSep}Jamie T{OSDirSep}Trick{OSDirSep}01 Tinfoil Boy.m4a"),
                Arg.Is<string>($"{OSDirSep}Users{OSDirSep}jfox{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}iTunes Media{OSDirSep}Music"))
                .Returns($"Jamie T{OSDirSep}Trick{OSDirSep}01 Tinfoil Boy.m4a");

            IPlaylistFormatter sut = new SensusPlaylistFormatter(_fileSystem);

            string result = sut.FormatPlaylistFile($"{OSDirSep}Users{OSDirSep}jfox{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}iTunes Media{OSDirSep}Music{OSDirSep}Jamie T{OSDirSep}Trick{OSDirSep}01 Tinfoil Boy.m4a",
                $"{OSDirSep}Users{OSDirSep}jfox{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}iTunes Media{OSDirSep}Music");

            Assert.False(result.StartsWith(OSDirSep.ToString()));
            Assert.False(result.StartsWith(SensusDirSep.ToString()));
        }

        [Fact]
        public void FormatPlaylistFile_Formats_UsesSensusDirSeperators()
        {
            _fileSystem.GetRelativePath(Arg.Is<string>($"{OSDirSep}Users{OSDirSep}jfox{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}iTunes Media{OSDirSep}Music{OSDirSep}Jamie T{OSDirSep}Trick{OSDirSep}01 Tinfoil Boy.m4a"),
                Arg.Is<string>($"{OSDirSep}Users{OSDirSep}jfox{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}iTunes Media{OSDirSep}Music"))
                .Returns($"Jamie T{OSDirSep}Trick{OSDirSep}01 Tinfoil Boy.m4a");

            IPlaylistFormatter sut = new SensusPlaylistFormatter(_fileSystem);

            string result = sut.FormatPlaylistFile($"{OSDirSep}Users{OSDirSep}jfox{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}iTunes Media{OSDirSep}Music{OSDirSep}Jamie T{OSDirSep}Trick{OSDirSep}01 Tinfoil Boy.m4a",
                $"{OSDirSep}Users{OSDirSep}jfox{OSDirSep}Music{OSDirSep}iTunes{OSDirSep}iTunes Media{OSDirSep}Music");

            Assert.Contains(SensusDirSep.ToString(), result);
        }
    }
}
