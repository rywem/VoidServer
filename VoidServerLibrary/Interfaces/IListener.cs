using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VoidServerLibrary.Interfaces
{
    public interface IListener
    {
        void Start(string[] args, CancellationToken token);
        void Stop();
    }
}
