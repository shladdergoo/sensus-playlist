using Xunit;

using System;

namespace SensusPlaylist.Test
{
    public class SensusPlaylistFormatterTest
    {
        [Fact]
        public void Ctor_FileSystemNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                SensusPlaylistFormatter sut = new SensusPlaylistFormatter(null);
            });
        }
    }
}