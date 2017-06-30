using Xunit;

using System;
using System.Collections.Generic;

namespace SensusPlaylist.Test
{
    public class VariousArtistRulesTest
    {
        [Fact]
        public void Evaluate_ArtistsNull_ThrowsException()
        {
            VariousArtistNormalizationRule sut =
                new VariousArtistNormalizationRule();

            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.Evaluate(null);
            });
        }

        [Fact]
        public void Evaluate_ArtistsEmpty_ReturnsNull()
        {
            VariousArtistNormalizationRule sut =
                new VariousArtistNormalizationRule();

            Assert.Null(sut.Evaluate(new List<string>()));
        }

        [Fact]
        public void Evaluate_HasArtists_ReturnsVarious()
        {
            const string VariousString = "Various";

            VariousArtistNormalizationRule sut =
                new VariousArtistNormalizationRule();

            Assert.Equal(VariousString, sut.Evaluate(new List<string> { "someArtist" }));
        }
    }
}