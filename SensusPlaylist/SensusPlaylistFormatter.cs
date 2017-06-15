using System;

namespace SensusPlaylist
{
    public class SensusPlaylistFormatter : IPlaylistFormatter
    {
        private IFileSystem _fileSystem;
        private string _libraryRoot;

        public SensusPlaylistFormatter(IFileSystem fileSystem, string libraryRoot)
        {
            if (fileSystem == null) throw new ArgumentNullException(nameof(fileSystem));
            if (libraryRoot == null) throw new ArgumentNullException(nameof(libraryRoot));

            _fileSystem = fileSystem;
            _libraryRoot = libraryRoot;
        }

        public string FormatPlaylistFile(string filename)
        {
            if (filename == null) throw new ArgumentNullException(nameof(filename));

            string relativeFilename = _fileSystem.GetRelativePath(filename, _libraryRoot);

            return relativeFilename;
        }
    }
}