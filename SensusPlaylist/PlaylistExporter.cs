using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.IO;

namespace SensusPlaylist
{
    public class PlaylistExporter : IPlaylistExporter
    {
        private ILogger _logger = ServiceProvider.GetLogger<PlaylistExporter>();
        private IFileSystem _fileSystem;
        private IPlaylistReader _playlistReader;
        private IPlaylistWriter _playlistWriter;

        public PlaylistExporter(IFileSystem fileSystem, IPlaylistReader playlistReader,
            IPlaylistWriter playlistWriter)
        {
            if (fileSystem == null) throw new ArgumentNullException(nameof(fileSystem));
            if (playlistReader == null) throw new ArgumentNullException(nameof(playlistReader));
            if (playlistWriter == null) throw new ArgumentNullException(nameof(playlistWriter));

            _fileSystem = fileSystem;
            _playlistReader = playlistReader;
            _playlistWriter = playlistWriter;
        }

        public void Export(string filename, string outputDirectory, string libraryRoot)
        {
            Export(filename, outputDirectory, libraryRoot, false);
        }

        public void Export(string filename, string outputDirectory, string libraryRoot,
            bool exportPlaylistFile)
        {
            _logger.LogDebug("[Export] Start");

            if (filename == null) throw new ArgumentNullException(nameof(filename));
            if (outputDirectory == null) throw new ArgumentNullException(nameof(outputDirectory));
            if (libraryRoot == null) throw new ArgumentNullException(nameof(libraryRoot));
            if (!_fileSystem.FileExists(filename)) throw new FileNotFoundException(filename);

            DoExport(filename, outputDirectory, libraryRoot, exportPlaylistFile);

            _logger.LogDebug("[Export] End");
        }

        private void DoExport(string filename, string outputDirectory, string libraryRoot,
            bool exportPlaylistFile)
        {
            InitializeOutputDirectory(outputDirectory);

            Playlist playlist;
            if ((playlist = ProcessPlaylist(filename, outputDirectory, libraryRoot)) != null)
            {
                if (exportPlaylistFile) ExportPlaylistFile(playlist, outputDirectory);
            }
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

        private Playlist ProcessPlaylist(string filename, string outputDirectory,
            string libraryRoot)
        {
            Playlist playlist = _playlistReader.ReadAll(_fileSystem.FileOpen(filename,
                FileMode.Open, FileAccess.Read), _fileSystem.GetShortName(filename),
                libraryRoot);
            if (playlist == null || !playlist.Files.Any())
            {
                _logger.LogDebug("[Export] No files");
                return null;
            }
            else
            {
                CopyPlaylistFiles(playlist, outputDirectory, libraryRoot);
            }

            return playlist;
        }

        private void CopyPlaylistFiles(Playlist playlist, string outputDirectory, string libraryRoot)
        {
            foreach (string filename in playlist.Files)
            {
                CopyFileWithParentsRelativeToRoot(filename, outputDirectory, libraryRoot);
            }
        }

        private void CopyFileWithParentsRelativeToRoot(string filename, string outputDirectory, string libraryRoot)
        {
            string filenameRelativeToRoot = _fileSystem.GetRelativePath(filename, libraryRoot);
            string fileDirectory = _fileSystem.GetDirectory(filename);
            string relativeDirectories = _fileSystem.GetRelativePath(fileDirectory, libraryRoot);
            string fileShortName = _fileSystem.GetShortName(filename);

            string targetDirectory = Path.Combine(outputDirectory, relativeDirectories);

            if (!_fileSystem.DirectoryExists(targetDirectory))
            {
                _fileSystem.CreateDirectory(targetDirectory);
            }

            string targetFile = Path.Combine(targetDirectory, fileShortName);

            _fileSystem.FileCopy(filename, targetFile, true);
        }

        private void ExportPlaylistFile(Playlist playlist, string outputDirectory)
        {
            _logger.LogDebug("[Export] Writing playlist file");

            string exportFilename = Path.Combine(outputDirectory, playlist.Name);

            _playlistWriter.WriteAll(playlist, _fileSystem.FileOpen(exportFilename,
                FileMode.Create, FileAccess.Write));
        }
    }
}