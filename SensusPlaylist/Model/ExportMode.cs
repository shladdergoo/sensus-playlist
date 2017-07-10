using System;

namespace SensusPlaylist
{
    [Flags]
    public enum ExportMode
    {
        None = 0,
        PlaylistFile = 1,
        PlaylistContents = 2
    }
}