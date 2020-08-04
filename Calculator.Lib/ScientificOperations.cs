using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Lib
{
    /*
    The main ScientificOperations class
    Contains all methods for performing basic Scientific Operations 
    */
    /// <summary>
    /// The main <c>ArithmeticOperations</c> class.
    /// Contains all methods for performing basic math functions.
    /// <list type="bullet">
    /// <item>
    /// <term>LogBase10</term>
    /// <description>Logarithmic Operation of Base 10</description>
    /// </item>
    /// <item>
    /// <term>LogBaseE</term>
    /// <description>Logarithmic Operation of Base e</description>
    /// </item>
    /// <item>
    /// <term>Exponential</term>
    /// <description>Exponential Operation</description>
    /// </item>
    /// <item>
    /// <term>SquareRoot</term>
    /// <description>SquareRoot Operation</description>
    /// </item>
    /// <item>
    /// <term>Sine</term>
    /// <description>Sine Operation</description>
    /// </item>
    /// <item>
    /// <term>Cosine</term>
    /// <description>Cosine Operation</description>
    /// </item>
    /// <item>
    /// <term>Tangent</term>
    /// <description>Tangent Operation</description>
    /// </item>
    /// <item>
    /// <term>Power</term>
    /// <description>Power Operation</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>This class can find ln, log, e^x, SquareRoot, Sine, Cosine, Tangent and Power.</para>
    /// <para>These operations can be performed on doubles.</para>
    /// </remarks>
    public class ScientificOperations : IScientificOperations
    {
        // Find the log of double and returns the result
        ///<summary>
        ///log a double<paramref name="value"/>and returns the result.
        ///</summary>
        ///<returns>
        ///The log of a double.
        ///</returns>
        ///<param name= "value" > A double precision number.</param>
        public double LogBase10(double value)
        {
            return Math.Log10(value);
        }

        // Find the ln of double and returns the result
        ///<summary>
        ///ln a  doubles<paramref name="value"/>and returns the result.
        ///</summary>
        ///<returns>
        ///The ln of a double.
        ///</returns>
        ///<param name= "value" > A double precision number.</param>
        public double LogBaseE(double value)
        {
            return Math.Log(value);
        }
        // Find the exponential of double and returns the result
        ///<summary>
        ///exponent a double<paramref name="value"/>and returns the result.
        ///</summary>
        ///<returns>
        ///The exponent of a double.
        ///</returns>
        ///<param name= "value" > A double precision number.</param>
        public double Exponential(double value)
        {
            return Math.Exp(value);

        }

        // Find the Square Root of double and returns the result
        ///<summary>
        ///Square Root <paramref name="value"/>and returns the result.
        ///</summary>
        ///<returns>
        ///The square root of a double.
        ///</returns>
        ///<param name= "value" > A double precision number.</param>
        public double SquareRoot(double value)
        {
            return Math.Sqrt(value);

        }
        // Find the sine value of double and returns the result
        ///<summary>
        ///sine the double<paramref name="value"/>and returns the result.
        ///</summary>
        ///<returns>
        ///The sine of a double.
        ///</returns>
        ///<param name= "value" > A double precision number.</param>
        public double Sine(double value)
        {
            return Math.Sin(value);

        }
        // Find the cosine value of double and returns the result
        ///<summary>
        ///cosine the double<paramref name="value"/>and returns the result.
        ///</summary>
        ///<returns>
        ///The cosine of a double.
        ///</returns>
        ///<param name= "value" > A double precision number.</param>
        public double Cosine(double value)
        {
            return Math.Cos(value);

        }
        // Find the tangent value of double and returns the result
        ///<summary>
        ///tangent the double<paramref name="value"/>and returns the result.
        ///</summary>
        ///<returns>
        ///The tangent of a double.
        ///</returns>
        ///<param name= "value" > A double precision number.</param>
        public double Tangent(double value)
        {
            return Math.Tan(value);
        }

        // Find the power value of a double to another and returns the result
        ///<summary>
        ///power of a double  <paramref name="value1"/>  to another double <paramref name="value2"/> and returns the result.
        ///</summary>
        ///<returns>
        ///The power of a double to another double.
        ///</returns>
        ///<param name= "value1" > A double precision number.</param>
        ///<param name= "value2" > A double precision number.</param>
        public double Power(double value1, double value2)
        {
            return Math.Pow(value1, value2);
        }
    }
}
