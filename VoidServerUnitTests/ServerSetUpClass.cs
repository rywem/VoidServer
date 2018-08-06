using NUnit.Framework;
using VoidServerLibrary;
using VoidServerLibrary.Listeners;

namespace VoidServerUnitTests
{
    [SetUpFixture]
    public class ServerSetUpClass 
    {
        [OneTimeSetUp]
        public void RunBeforeTests()
        {
            ServerManager.Start<VSocketListener>(new string[] { "127.0.0.1" });
        }
        [OneTimeTearDown]
        public void RunAfterTests()
        {
            ServerManager.Stop();
        }
    }
}
