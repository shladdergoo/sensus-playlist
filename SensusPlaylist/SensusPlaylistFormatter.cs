using System;

namespace SensusPlaylist
{
    public class SensusPlaylistFormatter : IPlaylistFormatter
    {
        private IFileSystem _fileSystem;

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

            return relativeFilename;
        }
    }
}