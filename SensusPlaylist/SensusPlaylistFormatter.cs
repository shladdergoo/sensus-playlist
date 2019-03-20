using System;
using System.IO;

namespace SensusPlaylist
{
    public class SensusPlaylistFormatter : IPlaylistFormatter
    {
        private readonly IFileSystem _fileSystem;

        private readonly char OSDirSep = Path.DirectorySeparatorChar;
        private readonly char SensusDirSep = '\\';

        public SensusPlaylistFormatter(IFileSystem fileSystem)
        {
            if (fileSystem == null) throw new ArgumentNullException(nameof(fileSystem));

            _fileSystem = fileSystem;
        }

        public string FormatPlaylistFile(string filename, string libraryRoot)
        {
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            if (libraryRoot == null) throw new ArgumentNullException(nameof(libraryRoot));

            string relativeFilename = _fileSystem.GetRelativePath(filename, libraryRoot);

            relativeFilename = relativeFilename.Replace(OSDirSep, SensusDirSep);

            return relativeFilename;
        }
    }
}
