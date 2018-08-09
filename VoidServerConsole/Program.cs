using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoidServerLibrary;
using VoidServerLibrary.Interfaces;
using VoidServerLibrary.Listeners;

namespace VoidServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<VHttpListener>().As<IListener>();
            builder.RegisterType<Server>();
            IContainer container = builder.Build();

            var server = container.Resolve<Server>();
            server.Start(new string[] { "http://127.0.0.1:8080/" });
            //server.Start(new string[] { "127.0.0.1" });
            Console.ReadLine();
        }
    }
}
