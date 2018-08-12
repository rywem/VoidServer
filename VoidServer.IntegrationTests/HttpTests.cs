using NUnit.Framework;
using VoidServerLibrary;
using VoidServerLibrary.Clients;
using VoidServerLibrary.Listeners;

namespace VoidServer.IntegrationTests
{
    [TestFixture]
    public class HttpTests
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            ServerManager.Start<VHttpListener>(new string[] { "http://127.0.0.1:8081/" });
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            ServerManager.Stop();
        }

        public void RunBetween()
        {

        }
        [Test]
        public void TestHttpCalculatesAdditionAnswer()
        {
            // todo: put tests here
            VWebClient client = new VWebClient();
            client.Send(new VoidServerLibrary.Requests.CalculationRequest() { a = 2, b = 3, Operation = VoidServerLibrary.Util.Operation.Addition, URL = "http://127.0.0.1:8081/" });
            System.Console.WriteLine(client.VResponse);
            StringAssert.StartsWith((2 + 3).ToString(), client.VResponse);
        }
        [Test]
        public void TestHttpCalculatesMultiplicationAnswer()
        {
            System.Threading.Thread.Sleep(100);
            // todo: put tests here
            VWebClient client = new VWebClient();
            client.Send(new VoidServerLibrary.Requests.CalculationRequest() { a = 2, b = 3, Operation = VoidServerLibrary.Util.Operation.Multiplication, URL = "http://127.0.0.1:8081/" });
            System.Console.WriteLine(client.VResponse);

            StringAssert.StartsWith((2 * 3).ToString(), client.VResponse);
        }

        [Test]
        public void TestHttpCalculatesSubtractionAnswer()
        {
            // todo: put tests here
            VWebClient client = new VWebClient();
            client.Send(new VoidServerLibrary.Requests.CalculationRequest() { a = 2, b = 3, Operation = VoidServerLibrary.Util.Operation.Subtraction, URL = "http://127.0.0.1:8081/" });
            System.Console.WriteLine(client.VResponse);
            StringAssert.StartsWith((2 - 3).ToString(), client.VResponse);
        }

        [Test]
        public void TestHttpCalculatesDivisionAnswer()
        {
            // todo: put tests here
            VWebClient client = new VWebClient();
            client.Send(new VoidServerLibrary.Requests.CalculationRequest() { a = 6, b = 3, Operation = VoidServerLibrary.Util.Operation.Division, URL = "http://127.0.0.1:8081/" });
            System.Console.WriteLine(client.VResponse);
            StringAssert.StartsWith((6 / 3).ToString(), client.VResponse);
        }
    }
}
