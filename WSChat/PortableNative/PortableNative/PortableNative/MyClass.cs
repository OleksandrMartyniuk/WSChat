using System;

namespace PortableNative
{
    public class CalcClass
    {
        public CalcClass()
        {

        }

        public string Calc(int num1, int num2, string op)
        {
            switch (op)
            {
                case "+":
                    return (num1 + num2).ToString();
                    break;
                case "-":
                    return (num1 - num2).ToString();
                    break;
                case "*":
                    return (num1 * num2).ToString();
                    break;
                case "/":
                    return (num1 / num2).ToString();
                    break;
                default:
                    return "Operator invalid";
                    break;
            }
        }
    }
}

