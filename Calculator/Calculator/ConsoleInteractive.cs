using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class ConsoleInteractive : IInteractive
    {
        public void Output(string message)
        {
            Console.Write(message);
        }
        public string InputLine()
        {
            return Console.ReadLine();
        }
    }
}

