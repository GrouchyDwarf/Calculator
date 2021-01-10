using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class RPN//Reverse Polish Notation
    {
        private readonly IInteractive _interactive;
        private short GetPriority(char input) => input switch
        //'?' used to recognize unary minus
        {
            '+' => 1,
            '-' => 1,
            '*' => 2,
            '/' => 2,
            '?' => 3,
            _ => -1,
        };
        private bool IsOperator(char input)
        {
            return ("+-/*".IndexOf(input) != -1);
        }
        public void ExecuteOperation(ref Stack<double> numbers, char operation)
        {
            if (operation == '?')//for unary minus
            {
                double numb = numbers.Pop();
                numbers.Push(-numb);
                return;
            }
            else
            {
                double x = numbers.Pop();
                double y = numbers.Pop();
                switch (operation)
                {
                    case '+': numbers.Push(y + x); break;
                    case '-': numbers.Push(y - x); break;
                    case '*': numbers.Push(y * x); break;
                    case '/':
                        if (x == 0)
                        {
                            _interactive.OutputAsync("Division by zero!");
                        }
                        numbers.Push(y / x); break;
                }

            }

        }

        public double Calculate(string input)
        {
            var hasUnaryMinus = input[0] == '-' || false;
            var numbers = new Stack<double>();
            var operations = new Stack<char>();
            for (var i = 0; i < input.Length; ++i)
            {
                if (input[i] == '(')//after opening-unary
                {
                    hasUnaryMinus = true;
                    operations.Push('(');
                }
                else if (input[i] == ')')//after closing-operation
                {
                    while (operations.Peek() != '(')//execute all operations before bracket
                    {
                        ExecuteOperation(ref numbers, operations.Pop());
                    }
                    operations.Pop();
                    hasUnaryMinus = false;
                }
                else if (IsOperator(input[i]))
                {
                    char sign = hasUnaryMinus ? '?':input[i];
                    while (operations.Count > 0 && GetPriority(operations.Peek()) >= GetPriority(sign))
                    {

                        ExecuteOperation(ref numbers, operations.Pop());
                    }
                    operations.Push(sign);
                    hasUnaryMinus = true;
                }
                else
                {
                    var number = new StringBuilder();
                    while (i < input.Length && (char.IsDigit(input[i]) || input[i] == ',' || input[i] == '.'))
                    {
                        if (input[i] == '.')
                        {
                            number.Append(',');
                            ++i;
                        }
                        else
                        {
                            number.Append(input[i++]);
                        }
                    }
                        --i;
                    numbers.Push(double.Parse(number.ToString()));
                    hasUnaryMinus = false;
                }
            }
            while (operations.Count > 0)
            {
                ExecuteOperation(ref numbers, operations.Pop());
            }
            return numbers.Peek();
        }
        public string GetAnswer(ref string input)
        {
            input = Regex.Replace(input, @"\s", string.Empty);//remove spaces
            if (!Regex.IsMatch(input, @"[^0-9-+*/(),.=]") && input.Length != 0 && !Regex.IsMatch(input, @"([+*/,.()=])\1"))//check if correct input
            {
                input = Regex.Replace(input, @"=", string.Empty);//remove one equal
                return Calculate(input).ToString();
            }
            return "Incorrect input!";
        }
        public RPN(IInteractive input)
        {
            _interactive = input;
        }
        public RPN()
        {
        }
    }
}

