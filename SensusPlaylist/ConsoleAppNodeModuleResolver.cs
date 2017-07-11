using System;
using System.IO;

namespace SensusPlaylist
{
    public class ConsoleAppNodeModuleResolver : IModuleResolver
    {
        const string ModuleDirectory = "node_modules";

        string[] _modulePaths = {".\\", "..\\..\\..\\..\\"};

        public string Resolve()
        {
            string modulePath = null;

            foreach (string path in _modulePaths)
            {
                string targetPath = String.Concat(path, ModuleDirectory);
                if(Directory.Exists(targetPath))
                {
                    modulePath = targetPath;
                    break;
                }
            }

            return modulePath;
        }
    }
}