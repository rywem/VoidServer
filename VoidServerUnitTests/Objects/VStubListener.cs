using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VoidServerLibrary.Interfaces;

namespace VoidServerUnitTests.Objects
{
    public class VStubListener : IListener
    {
        VoidServerLibrary.Requests.CalculationRequest Request;

        public VStubListener(VoidServerLibrary.Requests.CalculationRequest request)
        {
            this.Request = request;
        }

        public void Start(CancellationToken token, string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
