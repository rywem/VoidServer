using NUnit.Framework;
using VoidServerLibrary;
using VoidServerLibrary.Clients;
using VoidServerLibrary.Listeners;


namespace VoidServer.IntegrationTests
{
    [TestFixture]
    public class SocketTests
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            ServerManager.Start<VSocketListener>(new string[] { "127.0.0.1" });
        }
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            ServerManager.Stop();
        }
        [Test]
        public void TestSocketCalculatesAdditionAnswer()
        {
            VSocketClient client = new VSocketClient();
            client.Send(new VoidServerLibrary.Requests.CalculationRequest() { a = 2, b = 3, Operation = VoidServerLibrary.Util.Operation.Addition, URL = "127.0.0.1" });
            System.Console.WriteLine(client.VResponse);
            StringAssert.StartsWith((2 + 3).ToString(), client.VResponse);
        }

        [Test]
        public void TestSocketCalculatesSubstractionAnswer()
        {
            VSocketClient client = new VSocketClient();
            client.Send(new VoidServerLibrary.Requests.CalculationRequest() { a = 4, b = 3, Operation = VoidServerLibrary.Util.Operation.Subtraction, URL = "127.0.0.1" });
            System.Console.WriteLine(client.VResponse);
            StringAssert.StartsWith((4 - 3).ToString(), client.VResponse);
        }
    }
}
