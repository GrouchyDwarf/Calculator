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
            Console.Write(message);
            return Task.CompletedTask;
            //return Task.Factory.StartNew(() => Console.Write(message));
            
        }
        public Task<string> InputLineAsync()
        {
            return Task.FromResult(Console.ReadLine());
            //return Task.Factory.StartNew(() => Console.ReadLine());
        }
    }
}

