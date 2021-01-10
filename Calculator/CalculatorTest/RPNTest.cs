using System;
using Xunit;
using Calculator;

namespace CalculatorTest
{
    public class RPNTests
    {
        [Fact]
        public void Calculate_Expression_Answer()
        {
            var rpn = new RPN();
            var expected = -2.0;
            double actual = rpn.Calculate("5-4*7/(1+3)");
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetAnswer_IncorrectInput_Warning()
        {
            var consoleInteracte = new ConsoleInteractive();
            var rpn = new RPN(consoleInteracte);
            var expected = "Incorrect input!";
            var input = "x+2";
            string actual = rpn.GetAnswer(ref input);
            Assert.Equal(actual, expected);
        }
    }
}

