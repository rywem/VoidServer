using NUnit.Framework;
using VoidServerLibrary;
using VoidServerLibrary.Clients;

namespace VoidServerUnitTests.IntegrationTests
{
    [TestFixture]
    public class ServerTests
    {
        [Test]
        public void TestServerReturnsOne()
        {
            // todo: put tests here
            VSocketClient client = new VSocketClient();
            client.Send(new VRequest() { Message = "1", URL = "127.0.0.1" });

            StringAssert.StartsWith("1", client.VResponse);
        }
    }
}
