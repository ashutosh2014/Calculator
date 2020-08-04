using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Win
{
    /*
    The main Expression class
    Contains all methods for checking expression. 
    */
    /// <summary>
    /// The main <c>ArithmeticOperations</c> class.
    /// Contains all methods for performing basic math functions.
    /// <list type="bullet">
    /// <item>
    /// <term>CheckResult</term>
    /// <description>Cheks the Result</description>
    /// </item>
    /// <item>
    /// <term>CheckInput</term>
    /// <description>Check the Input</description>
    /// </item>
    /// <item>
    /// <term>IsOperator</term>
    /// <description>Check for Operator</description>
    /// </item>
    /// <item>
    /// <term>Change Key</term>
    /// <description>Change Key Press to relative function</description>
    /// </item>
    /// <item>
    /// <term>IsScientitfic</term>
    /// <description>Checks for scientific Operation</description>
    /// </item>
    /// <item>
    /// <term>Change Expression</term>
    /// <description>Changes the expression</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>This class can add, subtract, multiply, divide and modulus.</para>
    /// <para>These operations can be performed on doubles.</para>
    /// </remarks>

    class Expression
    {
        public string CheckResult(double res)
        {
            if (double.IsInfinity(res))
            {
                return Lib.NotDivideZero;
            }
            else if (double.IsNaN(res))
            {
                return Lib.NaN;
            }
            else
            {
                return res.ToString();
            }
        }
        public bool CheckInput(string input)
        {
            if (input == Lib.NotDivideZero)
            {
                return false;
            }
            else if (input == Lib.NaN)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Check for High Precedence Operator
        /// </summary>
        /// <param name="op"></param>
        /// <returns>
        /// The charcter value for Operator
        /// </returns>
        public char OperatorValue(char op)
        {
            switch (op)
            {
                case '*':
                case '/':
                    return '1';
                default:
                    return '0';
            }
        }
        /// <summary>
        /// Checks for Arithmetic Operator
        /// </summary>
        /// <param name="op"></param>
        /// <returns>
        /// boolean for Arithmetic Operation
        /// </returns>
        public bool IsOperator(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// Check for scientific 
        /// </summary>
        /// <param name="funcitionValue">A string</param>
        /// <returns>
        /// A string
        /// </returns>
        public bool IsScientific(string funcitionValue)
        {
            switch (funcitionValue)
            {
                case "sin":
                case "cos":
                case "tan":
                case "log":
                case "ln":
                case "1/x":
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// Change Key Press to relative function
        /// </summary>
        /// <param name="funcitionValue">A string</param>
        /// <returns>
        /// A string which is relative to key Pressed.
        /// </returns>
        public string ChangeKey(string funcitionValue)
        {
            string returnValue;
            switch (funcitionValue)
            {
                case "S":
                    returnValue = "sin";
                    break;
                case "C":
                    returnValue = "cos";
                    break;
                case "T":
                    returnValue = "tan";
                    break;
                case "L":
                    returnValue = "ln";
                    break;
                case "l":
                    returnValue = "Log";
                    break;
                case "?":
                    returnValue = "1/x";
                    break;
                case "&":
                    returnValue = "MS";
                    break;
                case "$":
                    returnValue = "MR";
                    break;
                case "#":
                    returnValue = "MC";
                    break;
                case "@":
                    returnValue = "M-";
                    break;
                case "!":
                    returnValue = "M+";
                    break;
                case "~":
                    returnValue = "+/-";
                    break;
                case "c":
                    returnValue = "CE";
                    break;
                case "B":
                    returnValue = "AC";
                    break;
                default:
                    returnValue = funcitionValue;
                    break;
            }
            return returnValue;
        }
        /// <summary>
        /// This change the expression to be evaluated one.
        /// </summary>
        /// <param name="a"> A string</param>
        /// <param name="bracketDifference">An integer</param>
        /// <returns>
        /// The changed the expression in sring.
        /// </returns>
        public string ChangeExpression(string a, int bracketDifference)
        {
            if (a[0] == '-')
            {
                a = $"{'0'}{a}";
            }
            a = a.Replace(CalculatorWinResource.PlusMinus, CalculatorWinResource.minusSign);
            a = a.Replace(CalculatorWinResource.MinusMinus, CalculatorWinResource.Plus);
            string new_str = String.Empty;
            bool flag = false;
            for (int i = 0; i < a.Length; i++)
            {
                if (flag && (!char.IsDigit(a[i])))
                {
                    new_str += ')';
                    flag = false;
                }
                if (a[i] == '-' && a[i - 1] == '*')
                {
                    new_str += CalculatorWinResource.ZeroMinus;
                    flag = true;
                }
                else if (a[i] == '-' && a[i - 1] == '/')
                {
                    new_str += CalculatorWinResource.ZeroMinus;
                    flag = true;
                }
                else if (a[i] == '(')
                {
                    if (i != 0)
                    {
                        if (char.IsDigit(a[i - 1]))
                            new_str += "*(";
                        else
                            new_str += a[i];
                    }
                    else
                        new_str += a[i];
                }
                else if (a[i] == ')')
                {
                    if (IsOperator(a[i - 1]))
                    {
                        new_str += OperatorValue(a[i - 1]);
                    }
                    if (i < a.Length - 1)
                    {
                        if (Char.IsDigit(a[i + 1]))
                        {
                            new_str += ")*";
                        }
                        else
                        {
                            new_str += CalculatorWinResource.CloseBrace;
                        }
                    }
                    else
                    {
                        new_str += a[i];
                    }
                }
                else
                {
                    new_str += a[i];
                }
            }
            if (flag)
            {
                new_str += ')';
            }
            for (int i = 0; i < bracketDifference; i++)
            {
                new_str += ")";
            }
            a = new_str;
            return a;
        }
    }
}