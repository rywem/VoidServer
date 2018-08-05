using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoidServerLibrary;
using VoidServerLibrary.Clients;
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
