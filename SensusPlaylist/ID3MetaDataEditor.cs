using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Logging;

namespace SensusPlaylist
{
    public class ID3MetaDataEditor
    {
        private ILogger _logger = ServiceProvider.GetLogger<PlaylistExporter>();
        private INodeServices _nodeServices;
        private IModuleResolver _moduleResolver;

        public ID3MetaDataEditor(INodeServices nodeServices, IModuleResolver moduleResolver)
        {
            if (nodeServices == null) throw new ArgumentNullException(nameof(nodeServices));
            if (moduleResolver == null) throw new ArgumentNullException(nameof(moduleResolver));

            _nodeServices = nodeServices;
            _moduleResolver = moduleResolver;
        }

        public void WriteArtist(string artist)
        {
            _logger.LogDebug("[WriteArtist] Start");

            try
            {
                string modulePath  = _moduleResolver.Resolve();
                var result = _nodeServices.InvokeAsync<string>(Path.Combine(modulePath,"foo.js"))
                    .Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, null);
                throw ex;
            }

            _logger.LogDebug("[WriteArtist] End");
        }
    }
}