using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Logging;

namespace SensusPlaylist
{
    public class ID3MetaDataEditor
    {
        private ILogger _logger = ServiceProvider.GetLogger<PlaylistExporter>();
        private INodeServices _nodeServices;

        public ID3MetaDataEditor(INodeServices nodeServices)
        {
            if (nodeServices == null) throw new ArgumentNullException();

            _nodeServices = nodeServices;
        }

        public void WriteArtist(string artist)
        {
            _logger.LogDebug("[WriteArtist] Start");

            try
            {
                var result = _nodeServices.InvokeAsync<string>("foo.js").Result;
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