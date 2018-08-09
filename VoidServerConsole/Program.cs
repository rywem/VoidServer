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
            //var builder = new ContainerBuilder();
            //builder.RegisterType<VHttpListener>().As<IListener>();
            //builder.RegisterType<Server>();
            //IContainer container = builder.Build();

            //var server = container.Resolve<Server>();
            string[] socket = new string[] { "http://127.0.0.1:8080/" };
            string[] http = new string[] { "127.0.0.1" };
            //Console.ReadLine();
            bool testsCompleted = false;
            ServerManager.Start<VSocketListener>(socket);
            Console.WriteLine("Hit [ESC] to exit.");
            while (testsCompleted == false)
            {
                try
                {
                    System.Threading.Thread.Sleep(200);                    
                    testsCompleted = RunTest(socket[0]);
                }
                catch(Exception ex)
                {
                    break;
                }
            }
            Console.WriteLine("Stopping in 5 seconds.");
            System.Threading.Thread.Sleep(5000);
        }

        static bool RunTest(string url)
        {
            
            // todo: put tests here
            VSocketClient client = new VSocketClient();
            //client.Send(new VRequest() { Message = "1", URL = url });
            client.Send(new CalculationRequest() {a = 1, b = 3, Operation = VoidServerLibrary.Util.Operation.Addition, URL = "127.0.0.1" });
            return true;
        }
    }
}

