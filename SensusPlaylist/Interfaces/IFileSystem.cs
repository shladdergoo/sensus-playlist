using System.IO;

namespace SensusPlaylist
{
    public interface IFileSystem
    {
        bool FileExists(string filename);
        Stream FileOpen(string filename);
        void FileCopy(string source, string destination, bool overwrite);
        bool DirectoryExists(string path);
        bool DirectoryOpen(string path);
        void CreateDirectory(string path);
        void CleanDirectory(string path);
        string GetDirectoriesFromRelativePath(string path);
        string GetRelativePath(string path, string rootPath);
        string GetShortName(string path);
    }
}