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

        public string FormatPlaylistFile(string filename)
        {
            if (filename == null) throw new ArgumentNullException(nameof(filename));

            throw new NotImplementedException();
        }
    }
}