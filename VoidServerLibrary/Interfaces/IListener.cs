using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace VoidServerLibrary.Interfaces
{
    public interface IListener
    {
        void Start(CancellationToken token, string[] args);
    }
}
