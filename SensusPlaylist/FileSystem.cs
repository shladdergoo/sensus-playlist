using System;
using System.IO;

namespace SensusPlaylist
{
    public class FileSystem : IFileSystem
    {
        public bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        public Stream FileOpen(string filename)
        {
            if (filename == null) throw new ArgumentNullException(nameof(filename));

            if (!File.Exists(filename)) throw new FileNotFoundException(filename);

            return File.OpenRead(filename);
        }

        public void FileCopy(string source, string destination, bool overwrite)
        {
            File.Copy(source, destination, overwrite);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool DirectoryOpen(string path)
        {
            throw new NotImplementedException();
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void CleanDirectory(string path)
        {
            if (!Directory.Exists(path)) throw new FileNotFoundException();

            DirectoryInfo directory = new DirectoryInfo(path);

            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories())
            {
                subDirectory.Delete(true);
            }
        }

        public string GetDirectoriesFromRelativePath(string path)
        {
            if (Directory.Exists(path)) return path;

            if (File.Exists(path)) return new FileInfo(path).DirectoryName;

            throw new FileNotFoundException();
        }

        public string GetRelativePath(string path, string rootPath)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (rootPath == null) throw new ArgumentNullException(nameof(rootPath));

            if (!rootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                rootPath = string.Concat(rootPath, Path.DirectorySeparatorChar);
            }

            Uri target = new Uri(path);
            Uri root = new Uri(rootPath);

            Uri relativeUri = root.MakeRelativeUri(target);

            return Uri.UnescapeDataString(relativeUri.ToString())
                .Replace('/', Path.DirectorySeparatorChar);
        }

        public string GetShortName(string path)
        {
            if (File.Exists(path)) return new FileInfo(path).Name;

            if (Directory.Exists(path)) return new DirectoryInfo(path).Name;

            throw new FileNotFoundException();
        }
    }
}