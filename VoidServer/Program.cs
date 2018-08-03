using Autofac;
using System;
using VoidServerLibrary;
using VoidServerLibrary.Listeners;
using VoidServerLibrary.Interfaces;
namespace VoidServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<VSocketListener>().As<IListener>();
            builder.RegisterType<Server>();
            IContainer container = builder.Build();

            var server = container.Resolve<Server>();
            //server.Start(new string[] { "http://127.0.0.1:8080/" });
            server.Start(new string[] { "127.0.0.1" });
            Console.ReadLine();
        }
    }
}
