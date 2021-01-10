using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public interface IInteractive
    {
        Task OutputAsync(string message);
        Task<string> InputLineAsync();
    }
}

