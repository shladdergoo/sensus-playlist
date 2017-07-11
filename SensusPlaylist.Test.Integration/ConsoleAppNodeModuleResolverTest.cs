using System;
using System.IO;

using Xunit;

namespace SensusPlaylist.Test.Integration
{
    public class ConsoleAppNodeModuleResolverTest
    {
        [Fact]
        public void Resolve_Debug_Succeeds()
        {
            ConsoleAppNodeModuleResolver sut = new ConsoleAppNodeModuleResolver();

            string result = sut.Resolve();

            Assert.NotNull(result);
        }
    }
}