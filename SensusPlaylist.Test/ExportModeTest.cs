using Xunit;

namespace SensusPlaylist.Test
{
    public class ExportModeTest
    {
        [Fact]
        public void Enum_HasPlaylistFile_FindsPlaylistFileFlag()
        {
            ExportMode sut = ExportMode.PlaylistFile;

            Assert.True((sut & ExportMode.PlaylistFile) == ExportMode.PlaylistFile);
        }

        [Fact]
        public void Enum_HasPlaylistContents_FindsPlaylistContentsFlag()
        {
            ExportMode sut = ExportMode.PlaylistContents;

            Assert.True((sut & ExportMode.PlaylistContents) == ExportMode.PlaylistContents);
        }

        [Fact]
        public void Enum_HasPlaylistFileAndPlaylistContents_FindsPlaylistFileFlag()
        {
            ExportMode sut = ExportMode.PlaylistFile;
            sut |= ExportMode.PlaylistContents;

            Assert.True((sut & ExportMode.PlaylistFile) == ExportMode.PlaylistFile);
        }

        [Fact]
        public void Enum_HasPlaylistFileAndPlaylistContents_FindsPlaylistContentsFlag()
        {
            ExportMode sut = ExportMode.PlaylistFile;
            sut |= ExportMode.PlaylistContents;

            Assert.True((sut & ExportMode.PlaylistContents) == ExportMode.PlaylistContents);
        }

        [Fact]
        public void Enum_HasNone_DoesntFindPlaylistContentsFlag()
        {
            ExportMode sut = ExportMode.None;

            Assert.False((sut & ExportMode.PlaylistContents) == ExportMode.PlaylistContents);
        }

        [Fact]
        public void Enum_HasNone_DoesntFindPlaylistFileFlag()
        {
            ExportMode sut = ExportMode.None;

            Assert.False((sut & ExportMode.PlaylistFile) == ExportMode.PlaylistFile);
        }
    }
}