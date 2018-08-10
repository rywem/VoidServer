using NUnit.Framework;
using VoidServerLibrary;
using VoidServerLibrary.Clients;
using VoidServerLibrary.Listeners;

namespace VoidServerUnitTests.IntegrationTests.Http
{
    [TestFixture]
    public class HttpTests
    {
        [SetUp]
        public void RunBeforeAnyTests()
        {
            ServerManager.Start<VHttpListener>(new string[] { "http://127.0.0.1:8080/" });
        }

        [TearDown]
        public void RunAfterAnyTests()
        {
            ServerManager.Stop();
        }
        [Test]
        public void TestHttpCalculatesAdditionAnswer()
        {
            // todo: put tests here
            VWebClient client = new VWebClient();
            client.Send(new VoidServerLibrary.Requests.CalculationRequest() { a = 2, b = 3, Operation = VoidServerLibrary.Util.Operation.Addition, URL = "http://127.0.0.1:8080/" });
            System.Console.WriteLine(client.VResponse);
            StringAssert.StartsWith((2 + 3).ToString(), client.VResponse);
        }
        [Test]
        public void TestHttpCalculatesMultiplicationAnswer()
        {
            // todo: put tests here
            VWebClient client = new VWebClient();
            client.Send(new VoidServerLibrary.Requests.CalculationRequest() { a = 2, b = 3, Operation = VoidServerLibrary.Util.Operation.Multiplication, URL = "http://127.0.0.1:8080/" });
            System.Console.WriteLine(client.VResponse);
            StringAssert.StartsWith((2 + 3).ToString(), client.VResponse);
        }
    }
}
