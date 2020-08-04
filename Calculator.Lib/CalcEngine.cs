using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Lib
{
    /*
    The main CalcEngine class
    Contains all methods for performing evaluating expression 
    */
    /// <summary>
    /// The main <c>ArithmeticOperations</c> class.
    /// Contains all methods for performing basic math functions.
    /// <list type="bullet">
    /// <item>
    /// <term>calculate</term>
    /// <description>evaluate the expression</description>
    /// </item>
    /// <item>
    /// <term>CheckPrecedence</term>
    /// <description>checks the precedence</description>
    /// </item>
    /// <item>
    /// <term>Solve</term>
    /// <description>Solves the basic expression</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>This class can evalaute the expresssion having only 4 arithmetic operators like +,-,*,/ and caontaining bracket.</para>
    /// <para>These operations can be performed on doubles, string and bool.</para>
    /// </remarks>
    public class CalcEngine : ICalcEngine
    {
        // Evaluate expresssion having only 4 arithmetic operators like +,-,*,/ and caontaining bracket and returns the result
        ///<summary>
        ///evaluete the expression in string <paramref name="expression"/>and returns the result.
        ///</summary>
        ///<returns>
        ///The result of evaluted expression in double.
        ///</returns>
        ///<param name= "expression" > A string.</param>
        public double Calculate(string expression)
        {
            char[] extractTokens = expression.ToCharArray();
            Stack<double> operands = new Stack<double>();
            Stack<char> operators = new Stack<char>();
            string getNum = String.Empty;
            for (int i = 0; i < extractTokens.Length; i++)
            {
                if ((extractTokens[i] >= '0' && extractTokens[i] <= '9') || extractTokens[i] == '.')
                {
                    getNum += extractTokens[i];
                }
                else if (extractTokens[i] == '(')
                {
                    operators.Push(extractTokens[i]);
                }
                else if (extractTokens[i] == ')')
                {
                    try
                    {
                        operands.Push(Convert.ToDouble(getNum));
                        getNum = String.Empty;
                    }
                    catch (Exception e)
                    {
                        operands.Push(0);
                    }
                    while (operators.Peek() != '(')
                    {
                        operands.Push(Solve(operators.Pop(), operands.Pop(), operands.Pop()));
                    }
                    operators.Pop();
                }
                else if (extractTokens[i] == '+' || extractTokens[i] == '-' || extractTokens[i] == '*' || extractTokens[i] == '/')
                {
                    try
                    {
                        if (extractTokens[i - 1] != ')')
                        {
                            operands.Push(Convert.ToDouble(getNum));
                            getNum = String.Empty;
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        //do nothing
                    }
                    while (operators.Count > 0 && CheckPrecedence(extractTokens[i], operators.Peek()))
                    {
                        operands.Push(Solve(operators.Pop(), operands.Pop(), operands.Pop()));
                    }
                    operators.Push(extractTokens[i]);
                }
            }
            if (extractTokens[extractTokens.Length - 1] != ')')
            {
                operands.Push(Convert.ToDouble(getNum));
            }
            while (operators.Count > 0)
            {
                operands.Push(Solve(operators.Pop(), operands.Pop(), operands.Pop()));
            }
            return operands.Pop();
        }

        // Checks the precedence of one operator with another and returns the result
        ///<summary>
        ///Checks the precedence of one character <paramref name="operator1"/> with another character <paramref name="operator2"/> and returns the result.
        ///</summary>
        ///<returns>
        ///The result of precendence in bool.
        ///</returns>
        ///<param name= "operator1" > A character.</param>
        ///<param name= "operator2" > A character.</param>
        public static bool CheckPrecedence(char operator1, char operator2)
        {
            if (operator2 == '(' || operator2 == ')')
            {
                return false;
            }
            if ((operator1 == '*' || operator1 == '/') && (operator2 == '+' || operator2 == '-'))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Evaluate expresssion having 2 operands and one operator and returns the result
        ///<summary>
        ///evaluete the expression Evaluate expresssion having 2 operands<paramref name="a"/> , <paramref name="b"/>  and one operator <paramref name="op"/>and returns the result.
        ///</summary>
        ///<returns>
        ///The result of evaluted expression in double.
        ///</returns>
        ///<param name= "op" > A character.</param>
        ///<param name= "a" > A double precision number.</param>
        ///<param name= "b" > A double precision number.</param>
        public double Solve(char op, double b, double a)
        {
            ArithmeticOperations arithmeticOperations = new ArithmeticOperations();
            switch (op)
            {
                case '+':
                    return arithmeticOperations.Add(a, b);
                case '-':
                    return arithmeticOperations.Subtract(a, b);
                case '*':
                    return arithmeticOperations.Multiply(a, b);
                case '/':
                    return arithmeticOperations.Divide(a, b);
            }
            return 0;
        }
    }
}
