using Xunit;

using System;

namespace SensusPlaylist.Test
{
    public class PlaylistReaderTest
    {
        [Fact]
        public void ReadAAll_PlaylistNull_ThrowsException()
        {
            IPlaylistReader sut = new PlaylistReader();

            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.ReadAll(null);
            });
        }
    }
}