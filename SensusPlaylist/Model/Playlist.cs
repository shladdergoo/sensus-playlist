using System;
using System.Collections.Generic;
using System.IO;

namespace SensusPlaylist
{
    public class Playlist
    {
        public Playlist(string name, IEnumerable<string> files)
        {
            if(name == null) throw new ArgumentNullException(nameof(name));
            if(files == null) throw new ArgumentNullException(nameof(files));

            Name = name;
            Files = new List<string>(files);
        }

        public string Name { get; private set; }

        public IEnumerable<string> Files { get; private set; }
    }
}