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
                    _interactive.Output($"Choose operating mode\nEnter '{keyboard}'(to input from keyboard) or '{file}':");
                    mode = _interactive.InputLine();
                } while (mode != keyboard && mode != file);
                if (mode == keyboard)
                {
                    _interactive.Output("Enter expression:");
                    string input = _interactive.InputLine();
                    string answer = rpn.GetAnswer(ref input);
                    _interactive.Output($"{input}={answer}\n");
                }
                else if (mode == file)
                {
                    var path = string.Empty;
                    do
                    {
                        _interactive.Output("Please,enter the path to the input file:");
                        path = _interactive.InputLine();

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
                        _interactive.Output("Please,enter the path to the output file:");
                        path = _interactive.InputLine();

                    } while (!File.Exists(path));
                    File.WriteAllText(path, output.ToString());
                }
                _interactive.Output($"Enter '{exit}' or any key to try again:");
                flag = _interactive.InputLine();
            } while (flag != exit);
        }
    }
}
