using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoidServerLibrary.Requests;

namespace VoidServerLibrary.Util
{
    public class Calculator
    {
        public int Calculate(CalculationRequest request)
        {
            return Calculate(request.Operation, request.a, request.b);

        }

        public int Calculate(Operation operation, int a, int b)
        {
            switch (operation)
            {
                case Operation.Addition:
                    return a + b;                    
                case Operation.Division:
                    if (a == 0 || b == 0)
                        throw new ArgumentException("argument cannot be zero");
                    return a / b;
                case Operation.Multiplication:
                    return a * b;
                case Operation.Subtraction:
                    return a - b;
                case Operation.PowerOf:
                    return a ^ b;
                default:
                    return a;
            }
        }
    }

    //public class CalculationRequest
    //{
    //    public int A { get; set; }
    //    public int B { get; set; }
    //    Operation Operation { get; set; }

    //    public CalculationRequest()
    //    {

    //    }

    //    public CalculationRequest(Operation operation, params int[] values)
    //    {
    //        this.Operation = operation;
    //        this.Values = values;
    //    }
    //}
}
