using System;
using System.Collections.Generic;
using System.IO;

namespace SensusPlaylist
{
    public class Playlist
    {
        public Playlist(string name, string libraryRoot, IEnumerable<string> files)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (libraryRoot == null) throw new ArgumentNullException(nameof(libraryRoot));
            if (files == null) throw new ArgumentNullException(nameof(files));

            Name = name;
            LibraryRoot = libraryRoot;
            Files = new List<string>(files);
        }

        public string Name { get; private set; }

        public string LibraryRoot { get; private set; }

        public IEnumerable<string> Files { get; private set; }
    }
}