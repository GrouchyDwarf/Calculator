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
        public async void RunScript(RPN rpn)
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
                    await _interactive.OutputAsync($"Choose operating mode\nEnter '{keyboard}'(to input from keyboard) or '{file}':");
                    mode = await _interactive.InputLineAsync();
                } while (mode != keyboard && mode != file);
                if (mode == keyboard)
                {
                    await _interactive.OutputAsync("Enter expression:");
                    string input = await _interactive.InputLineAsync();
                    string answer = rpn.GetAnswer(ref input);
                    await _interactive.OutputAsync($"{input}={answer}\n");
                }
                else if (mode == file)
                {
                    var path = string.Empty;
                    do
                    {
                        await _interactive.OutputAsync("Please,enter the path to the input file:");
                        path = await _interactive.InputLineAsync();

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
                        await _interactive.OutputAsync("Please,enter the path to the output file:");
                        path = await _interactive.InputLineAsync();

                    } while (!File.Exists(path));
                    File.WriteAllText(path, output.ToString());
                }
                await _interactive.OutputAsync($"Enter '{exit}' or any key to try again:");
                flag = await _interactive.InputLineAsync();
            } while (flag != exit);
        }
    }
}
