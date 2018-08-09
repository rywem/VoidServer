using NUnit.Framework;
using VoidServerLibrary;
using VoidServerLibrary.Clients;

namespace VoidServerUnitTests.IntegrationTests
{
    [TestFixture]
    public class ServerTests
    {
        [Test]
        public void TestServerReturnsAdditionAnswer()
        {
            // todo: put tests here
            VSocketClient client = new VSocketClient();
            client.Send(new VoidServerLibrary.Requests.CalculationRequest() { a = 2, b = 3, Operation = VoidServerLibrary.Util.Operation.Addition, URL = "127.0.0.1" });
            System.Console.WriteLine(client.VResponse);
            StringAssert.StartsWith((2 + 3).ToString(), client.VResponse);
        }

        [Test]
        public void TestServerReturnsSubstractionAnswer()
        {
            // todo: put tests here
            VSocketClient client = new VSocketClient();
            client.Send(new VoidServerLibrary.Requests.CalculationRequest() { a = 4, b = 3, Operation = VoidServerLibrary.Util.Operation.Subtraction, URL = "127.0.0.1" });
            System.Console.WriteLine(client.VResponse);
            StringAssert.StartsWith((4 - 3).ToString(), client.VResponse);
        }
    }
}
