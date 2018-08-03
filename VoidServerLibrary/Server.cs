using System;
using VoidServerLibrary.Interfaces;

namespace VoidServerLibrary
{
    public class Server //: IServer
    {
        IListener _listener;
        public Server(IListener listener)
        {
            this._listener = listener;
        }

        public void Start()
        {

        }
        public void Start(string[] args)
        {
            _listener.Start(args);
        }
    }
}
