using Xunit;

using System;
using System.Collections.Generic;

namespace SensusPlaylist.Test
{
    public class PlaylistTest
    {
        [Fact]
        public void Ctor_NameNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Playlist sut = new Playlist(null, new List<string>());
            });
        }

        [Fact]
        public void Ctor_FilesNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Playlist sut = new Playlist("someName", null);
            });
        }
    }
}