using System;
using System.IO;

using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Xunit;

namespace SensusPlaylist.Test.Integration
{
    public class ID3MetaDataEditorTest
    {
        INodeServices _nodeServices;

        public ID3MetaDataEditorTest()
        {
            NodeServicesOptions options = new NodeServicesOptions(ServiceProvider.Current);
            options.ProjectPath = Directory.GetCurrentDirectory();

            _nodeServices = NodeServicesFactory.CreateNodeServices(options);
        }

        [Fact]
        public void WriteArtist_Succeeds()
        {
            ID3MetaDataEditor sut = new ID3MetaDataEditor(_nodeServices);

            sut.WriteArtist("foo");
        }
    }
}