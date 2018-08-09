using System;
using System.Collections.Generic;
using System.Text;
using VoidServerLibrary.Util;

namespace VoidServerLibrary.Requests
{
    public class CalculationRequest
    {
        public string URL { get; set; }
        public int a { get; set; }
        public int b { get; set; }
        public Operation Operation { get; set; }        
    }
}
