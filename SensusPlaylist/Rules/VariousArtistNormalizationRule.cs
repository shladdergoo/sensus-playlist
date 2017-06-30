using System;
using System.Collections.Generic;
using System.Linq;

namespace SensusPlaylist
{
    public class VariousArtistNormalizationRule : IArtistNormalizationRule
    {
        const string VariousString = "Various";

        public string Evaluate(IEnumerable<string> artists)
        {
            if (artists == null) throw new ArgumentNullException(nameof(artists));

            if (!artists.Any()) return null;

            return VariousString;
        }
    }
}