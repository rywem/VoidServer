using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoidServerLibrary;
using VoidServerLibrary.Clients;
using VoidServerLibrary.Interfaces;
using VoidServerLibrary.Listeners;
using VoidServerLibrary.Requests;

namespace VoidServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] http = new string[] { "http://127.0.0.1:8081/" };
            string[] socket = new string[] { "127.0.0.1" };            
            bool testsCompleted = false;
            ServerManager.Start<VHttpListener>(http);
            Console.ReadKey();
            Console.WriteLine("Hit [ESC] to exit.");
            while (testsCompleted == false)
            {
                try
                {
                    System.Threading.Thread.Sleep(200);                    
                    testsCompleted = RunTest(http[0]);
                }
                catch(Exception ex)
                {
                    break;
                }
            }
            ServerManager.Stop();
            Console.WriteLine("Stopping in 5 seconds.");
            System.Threading.Thread.Sleep(5000);
        }

        static bool RunTest(string url)
        {

            VWebClient client = new VWebClient();
            client.Send(new CalculationRequest() {a = 1, b = 3, Operation = VoidServerLibrary.Util.Operation.Addition, URL = url });
            return true;
        }
    }
}

