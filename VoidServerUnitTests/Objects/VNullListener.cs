﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoidServerLibrary.Interfaces;

namespace VoidServerUnitTests.Objects
{
    public class VNullListener : IListener
    {
        public void Start(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
