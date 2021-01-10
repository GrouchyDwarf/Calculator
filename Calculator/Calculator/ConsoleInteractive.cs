using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class ConsoleInteractive : IInteractive
    {
        public Task OutputAsync(string message)
        {
            return Task.Factory.StartNew(() => Console.Write(message));
            
        }
        public Task<string> InputLineAsync()
        {
            return Task.FromResult(Console.ReadLine());
        }
    }
}

