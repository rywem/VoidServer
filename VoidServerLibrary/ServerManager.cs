using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VoidServerLibrary.Interfaces;
using VoidServerLibrary.Listeners;

namespace VoidServerLibrary
{
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
