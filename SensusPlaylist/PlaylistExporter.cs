using Microsoft.Extensions.Logging;

using System;
using System.IO;

namespace SensusPlaylist
{
    public class PlaylistExporter : IPlaylistExporter
    {
        private ILogger _logger = ServiceProvider.GetLogger<PlaylistExporter>();
        private IFileSystem _fileSystem;
        private IPlaylistReader _playlistReader;

        public PlaylistExporter(IFileSystem fileSystem, IPlaylistReader playlistReader)
        {
            if (fileSystem == null) throw new ArgumentNullException(nameof(fileSystem));
            if (playlistReader == null) throw new ArgumentNullException(nameof(playlistReader));

            _fileSystem = fileSystem;
            _playlistReader = playlistReader;
        }

        public void Export(string filename, string outputDirectory)
        {
            _logger.LogDebug("[Export] Start");

            if (filename == null) throw new ArgumentNullException(nameof(filename));
            if (outputDirectory == null) throw new ArgumentNullException(nameof(outputDirectory));

            Stream playlistFile = null;
            if (!_fileSystem.FileExists(filename)) throw new FileNotFoundException(filename);

            InitializeOutputDirectory(outputDirectory);

            Playlist playlist = _playlistReader.ReadAll(playlistFile);

            CopyPlaylistFiles(playlist, outputDirectory);

            _logger.LogDebug("[Export] End");
        }

        private void InitializeOutputDirectory(string outputDirectory)
        {
            if (!_fileSystem.DirectoryExists(outputDirectory))
            {
                _fileSystem.CreateDirectory(outputDirectory);
            }
            else
            {
                _fileSystem.CleanDirectory(outputDirectory);
            }
        }

        private void CopyPlaylistFiles(Playlist playlist, string outputDirectory)
        {
            foreach (string filename in playlist.Files)
            {
                CopyFileWithParentDirectory(filename, outputDirectory);
            }
        }

        private void CopyFileWithParentDirectory(string filename, string outputDirectory)
        {
            string fileParentDirectory = _fileSystem.GetParentDirectory(filename);
            string fileParentShortName = _fileSystem.GetShortName(fileParentDirectory);
            string fileShortName = _fileSystem.GetShortName(filename);

            string targetDirectory = Path.Combine(outputDirectory, fileParentShortName);

            if (!_fileSystem.DirectoryExists(targetDirectory))
            {
                _fileSystem.CreateDirectory(targetDirectory);
            }

            string targetFile = Path.Combine(targetDirectory, fileShortName);

            _fileSystem.FileCopy(filename, targetFile, true);
        }
    }
}