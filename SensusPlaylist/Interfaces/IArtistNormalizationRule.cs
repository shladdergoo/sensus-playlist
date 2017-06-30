using System.Collections.Generic;

namespace SensusPlaylist
{
    public interface IArtistNormalizationRule
    {
        string Evaluate(IEnumerable<string> artists);
    }
}