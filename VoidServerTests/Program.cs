using Autofac;
using System;
using System.Threading;
using System.Threading.Tasks;
using VoidServerLibrary;
using VoidServerLibrary.Interfaces;
using VoidServerLibrary.Listeners;

namespace VoidServerTests
{
    class Program
    {
        static string[] http = new string[] { "http://127.0.0.1:8080/" };
        static string[] socket = new string[] { "127.0.0.1" };
        static bool isCanceled = false;
        static bool testsStarted = false;
        static bool testsCompleted = false;
        static void Main(string[] args)
        {
            ServerManager.Start<VHttpListener>(http);
            Console.WriteLine("Hit [ESC] to exit.");
            while(isCanceled == false)
            {
                System.Threading.Thread.Sleep(100);

                //todo start tests
                testsStarted = true;
                if (testsStarted)
                {
                    
                    //todo: run tests
                    testsCompleted = RunTest();
                }
                if(Console.KeyAvailable == true || testsCompleted == true)
                {
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                    {
                        isCanceled = true;
                        Console.WriteLine("Cancellation detecting, shutting server down.");
                        ServerManager.Stop();
                    }

                }
            }
            Console.WriteLine("Stopping in 5 seconds.");
            System.Threading.Thread.Sleep(5000);
        }
        
        static bool RunTest()
        {
            // todo: put tests here
            return true;
        }
    }

    public class ServerManager
    {
        
        private static Task ServerTask { get; set; }
        private static CancellationTokenSource _cancellationSource = new CancellationTokenSource();
        public static void Start<T>(string[] args) where T : VoidServerLibrary.Interfaces.IListener
        {
            ServerTask = Task.Factory.StartNew(() => Runner<T>.Start(args), _cancellationSource.Token);
        }

        public static void Stop()
        {
            _cancellationSource.Cancel();

            //while (ServerTask.IsCancell == false)
            //    Thread.Sleep(100);

            //ServerTask.Wait();
        }
    }
    public class Runner<T> where T : IListener
    {
        public static void Start(string[] args)
        {
            var builder = new ContainerBuilder();            
            builder.RegisterType<T>().As<IListener>();
            builder.RegisterType<Server>();
            IContainer container = builder.Build();
            var server = container.Resolve<Server>();
            server.Start(args);

        }
    }
}
