using System;
namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleInteractive = new ConsoleInteractive();
            var rpn = new RPN(consoleInteractive);
            var script = new Script(consoleInteractive);
            script.RunScript(rpn);
        }
    }
}

