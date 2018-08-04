using System;
using System.Collections.Generic;
using System.Text;

namespace VoidServerLibrary.Interfaces
{
    public interface IClient
    {
        string VResponse { get; set; }
        void Send(VRequest request);
    }
}
