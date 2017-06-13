using System.Collections.Generic;
using System.IO;

namespace SensusPlaylist
{
    public class Playlist
    {
        public Playlist()
        {
            Files = new List<string>();
        }

        public Playlist(IEnumerable<string> files)
        {
            Files = new List<string>(files);
        }

        public IEnumerable<string> Files { get; private set; }
    }
}