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

        private string GetTestPlaylist()
        {
            return "#EXTM3U" + "\r\n" +
                    "#EXTINF:255,Simple Song - The Shins" + "\r\n" +
                    "D:\\shlad\\Music\\iTunes\\Music\\The Shins\\Port Of Morrow\\02 Simple Song.m4a" + "\r\n" +
                    "#EXTINF:286,Roscoe - Midlake" + "\r\n" +
                    "D:\\shlad\\Music\\iTunes\\Music\\Midlake\\The Trials Of Van Occupanther\01 Roscoe.m4a";
        }
    }
}