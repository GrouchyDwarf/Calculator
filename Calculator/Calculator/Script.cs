using System;
using System.Text;
using System.IO;

namespace Calculator
{
    public class Script
    {
        private readonly IInteractive _interactive;
        public Script(IInteractive input)
        {
            _interactive = input;
        }
        public void RunScript(RPN rpn)
        {
            var mode = string.Empty;
            const string keyboard = "keyboard";
            const string file = "file";
            const string exit = "exit";
            var flag = "";
            do
            {
                do
                {
                    _interactive.OutputAsync($"Choose operating mode\nEnter '{keyboard}'(to input from keyboard) or '{file}':");
                    mode = _interactive.InputLineAsync().Result;
                } while (mode != keyboard && mode != file);
                if (mode == keyboard)
                {
                    _interactive.OutputAsync("Enter expression:");
                    string input = _interactive.InputLineAsync().Result;
                    string answer = rpn.GetAnswer(ref input);
                    _interactive.OutputAsync($"{input}={answer}\n");
                }
                else if (mode == file)
                {
                    var path = string.Empty;
                    do
                    {
                        _interactive.OutputAsync("Please,enter the path to the input file:");
                        path = _interactive.InputLineAsync().Result;

                    } while (!File.Exists(path));
                    var inputFile = new StreamReader(path);
                    string input;
                    var output = new StringBuilder();
                    while ((input = inputFile.ReadLine()) != null)
                    {
                        string answer = rpn.GetAnswer(ref input);
                        output.Append($"{input}={answer}\n");
                    }
                    do
                    {
                        _interactive.OutputAsync("Please,enter the path to the output file:");
                        path = _interactive.InputLineAsync().Result;

                    } while (!File.Exists(path));
                    File.WriteAllText(path, output.ToString());
                }
                _interactive.OutputAsync($"Enter '{exit}' or any key to try again:");
                flag = _interactive.InputLineAsync().Result;
            } while (flag != exit);
        }
    }
}
