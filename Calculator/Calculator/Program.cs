using System;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var consoleInteractive = new ConsoleInteractive();
            var rpn = new RPN(consoleInteractive);
            var script = new Script(consoleInteractive);
            await script.RunScript(rpn);
        }
    }
}

