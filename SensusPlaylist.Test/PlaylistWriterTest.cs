using Xunit;

using System;
using System.IO;

namespace SensusPlaylist.Test
{
    public class PlaylistWriterTest
    {
        [Fact]
        public void WriteAll_PlaylistNull_ThrowsException()
        {
            PlaylistWriter sut = new PlaylistWriter(new MemoryStream());

            Assert.Throws<ArgumentNullException>(() => sut.WriteAll(null));
        }
    }
}