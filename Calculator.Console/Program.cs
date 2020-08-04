using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Resources;

[assembly: NeutralResourcesLanguage("en")]
namespace Calculator.Console
{
    class Program 
    {
        static void Main(string[] args)
        {
            bool flag = true;

            Lib.CalcEngine calcEngine = new Lib.CalcEngine();
            Lib.ArithmeticOperations arithmeticOperations = new Lib.ArithmeticOperations();
            Lib.ScientificOperations scientificOperations = new Lib.ScientificOperations();
            Input input = new Input();
            while (flag)
            {
                System.Console.WriteLine(ConsoleData.CalculatorHead);
                System.Console.WriteLine(ConsoleData.Choices);
                bool defaultCase = true;
                double result = 0;
                switch (System.Console.ReadLine())
                {
                    case "+":
                        System.Console.WriteLine(ConsoleData.Add);
                        input.InputValue(2);
                        result = arithmeticOperations.Add(input.FirstValue, input.SecondValue);
                        break;
                    case "-":
                        System.Console.WriteLine(ConsoleData.Subtract);
                        input.InputValue(2);
                        result = arithmeticOperations.Subtract(input.FirstValue, input.SecondValue);
                        break;
                    case "*":
                        System.Console.WriteLine(ConsoleData.Multiply);
                        input.InputValue(2);
                        result = arithmeticOperations.Multiply(input.FirstValue, input.SecondValue);
                        break;
                    case "/":
                        System.Console.WriteLine(ConsoleData.Divide);
                        input.InputValue(2);
                        input.CheckForZero(input.SecondValue);
                        result = arithmeticOperations.Divide(input.FirstValue, input.SecondValue);
                        break;
                    case "mod":
                        System.Console.WriteLine(ConsoleData.Modulus);
                        input.InputValue(2);
                        input.CheckForZero(input.SecondValue);
                        result = arithmeticOperations.Modulus(input.FirstValue, input.SecondValue);
                        break;
                    case "log":
                        System.Console.WriteLine(ConsoleData.Log10);
                        input.InputValue(1);
                        result =  scientificOperations.LogBase10(input.FirstValue);
                        break;
                    case "ln":
                        System.Console.WriteLine(ConsoleData.LogE);
                        input.InputValue(1);
                        result = scientificOperations.LogBaseE(input.FirstValue);
                        break; ;
                    case "e^":
                        System.Console.WriteLine(ConsoleData.Exponential);
                        input.InputValue(1);
                        result = scientificOperations.Exponential(input.FirstValue);
                        break;
                    case "sqrt":
                        System.Console.WriteLine(ConsoleData.SquareRoot);
                        input.InputValue(1);
                        result = scientificOperations.SquareRoot(input.FirstValue);
                        break;
                    case "^":
                        System.Console.WriteLine(ConsoleData.Power);
                        input.InputValue(2);
                        result = scientificOperations.Power(input.FirstValue, input.SecondValue);
                        break;
                    case "sin":
                        System.Console.WriteLine(ConsoleData.Sine);
                        input.InputValue(1);
                        result = scientificOperations.Sine(input.FirstValue);
                        break;
                    case "cos":
                        System.Console.WriteLine(ConsoleData.Cosine);
                        input.InputValue(1);
                        result = scientificOperations.Cosine(input.FirstValue);
                        break;
                    case "tan":
                        System.Console.WriteLine(ConsoleData.Tangent);
                        input.InputValue(1);
                        result = scientificOperations.Tangent(input.FirstValue);
                        break;
                    default:
                        System.Console.WriteLine(ConsoleData.WrongChoice);
                        defaultCase = false;
                        break;
                }
                if (defaultCase)
                {
                    if (!double.IsNaN(result) && !double.IsInfinity(result))
                    {
                        System.Console.WriteLine(ConsoleData.Result + result);
                    }
                    else
                    {
                        System.Console.WriteLine(ConsoleData.MathError);
                    }
                }
                System.Console.WriteLine(ConsoleData.Continue);
                if (System.Console.ReadLine() != ConsoleData.One)
                {
                    flag = false;
                }
                System.Console.Clear();
            }
        }
    }
    /*
    The internal Input class
    Contains all methods for performing taking input 
    */
    /// <summary>
    /// The internal <c>Input</c> class.
    /// Contains all methods for taking input and check input for zero to have non-zero input.
    /// <list type="bullet">
    /// <item>
    /// <term>InputValue</term>
    /// <description>Input the values to operate on them.</description>
    /// </item>
    /// <item>
    /// <term>CheckForZero</term>
    /// <description>checks the value for non-zero to take only non-zero element.</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>This class can take input and check input for zero value. </para>
    /// <para>These operations can be performed on doubles.</para>
    /// </remarks>
    internal class Input
    {
        private double _first, _second;

        public double FirstValue => _first;
        public double SecondValue => _second;


        // Takes the input whether one input or two input according to the value of given parammeter
        ///<summary>
        /// Takes one input if an integer <paramref name="noOfInput"/> is 1 and of 2 it takes two inputs .
        ///</summary>
        ///<param name= "noOfInput" > An integer.</param>
        public void InputValue(int noOfInput)
        {
            string inputNum1, inputNum2;
            System.Console.WriteLine(ConsoleData.x_value);
            inputNum1 = System.Console.ReadLine();
            while (!double.TryParse(inputNum1, out _first))
            {
                System.Console.WriteLine(ConsoleData.x_not_value);
                inputNum1 = System.Console.ReadLine();
            }
            if (noOfInput == 2)
            {
                System.Console.WriteLine(ConsoleData.y_value);
                inputNum2 = System.Console.ReadLine();
                while (!double.TryParse(inputNum2, out _second))
                {
                    System.Console.WriteLine(ConsoleData.y_not_value);
                    inputNum2 = System.Console.ReadLine();
                }
            }
        }
        // Check the input  for non-zero if zero try to take non-zero element
        ///<summary>
        /// Check the input of <paramref name="value"/> for non-zero if zero try to take non-zero element.
        ///</summary>
        ///<param name= "value" > A double precision number.</param>
        public void CheckForZero(double value)
        {
            string inputNum2 = Convert.ToString(value);
            while (!double.TryParse(inputNum2, out _second) || (_second == 0))
            {
                if (SecondValue == 0)
                {
                    System.Console.WriteLine(ConsoleData.NonZero);
                }
                else
                {
                    System.Console.WriteLine(ConsoleData.x_not_value);
                }
                inputNum2 = System.Console.ReadLine();
            }
        }
    }
}