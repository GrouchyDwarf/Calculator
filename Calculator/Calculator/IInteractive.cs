using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public interface IInteractive
    {
        void Output(string message);
        string InputLine();
    }
}

