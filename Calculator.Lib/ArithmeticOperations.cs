using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Lib
{
    /*
    The main ArithmeticOperations class
    Contains all methods for performing basic Arithmetic Operations 
    */
    /// <summary>
    /// The main <c>ArithmeticOperations</c> class.
    /// Contains all methods for performing basic math functions.
    /// <list type="bullet">
    /// <item>
    /// <term>Add</term>
    /// <description>Addition Operation</description>
    /// </item>
    /// <item>
    /// <term>Subtract</term>
    /// <description>Subtraction Operation</description>
    /// </item>
    /// <item>
    /// <term>Multiply</term>
    /// <description>Multiplication Operation</description>
    /// </item>
    /// <item>
    /// <term>Divide</term>
    /// <description>Division Operation</description>
    /// </item>
    /// <item>
    /// <term>Modulus</term>
    /// <description>Modulus Operation</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>This class can add, subtract, multiply, divide and modulus.</para>
    /// <para>These operations can be performed on doubles.</para>
    /// </remarks>
    public class ArithmeticOperations : IArithmeticOperations
    {
        // Add two doubles and returns the result
        ///<summary>
        ///Adds two doubles<paramref name="value1"/> and<paramref name= "value2" /> and returns the result.
        ///</summary>
        ///<returns>
        ///The sum of two doubles.
        ///</returns>
        ///<param name= "value1" > A double precision number.</param>
        ///<param name = "value2" > A double precision number.</param>
        public double Add(double value1, double value2)
        {
            return value1 + value2;
        }

        // Subtracts a double from another and returns the result
        /// <summary>
        /// Subtracts a double <paramref name="value1"/> from another double <paramref name="value2"/> and returns the result.
        /// </summary>
        /// <returns>
        /// The difference between two doubles.
        /// </returns>
        /// <param name="value1">A double precision number.</param>
        /// <param name="value2">A double precision number.</param>
        public double Subtract(double value1, double value2)
        {
            return value1 - value2;
        }

        // Multiplies two doubles and returns the result
        /// <summary>
        /// Multiplies two doubles <paramref name="value1"/> and <paramref name="value2"/> and returns the result.
        /// </summary>
        /// <returns>
        /// The product of two doubles.
        /// </returns>
        /// <param name="value1">A double precision number.</param>
        /// <param name="value2">A double precision number.</param>
        public double Multiply(double value1, double value2)
        {
            return value1 * value2;
        }
        // Divides a double by another and returns the result
        /// <summary>
        /// Divides a double <paramref name="dividened"/> by another double <paramref name="divisor"/> and returns the result.
        /// </summary>
        /// <returns>
        /// The quotient of two doubles.
        /// </returns>
        /// <param name="dividened">A double precision dividend.</param>
        /// <param name="divisor">A double precision divisor.</param>
        public double Divide(double dividened, double divisor)
        {
            return dividened / divisor;
        }

        // Modulus a double by another and returns the result
        /// <summary>
        /// Modulus a double <paramref name="value1"/> by another double <paramref name="value2"/> and returns the result.
        /// </summary>
        /// <returns>
        /// The Remainder of two doubles.
        /// </returns>
        /// <param name="value1">A double precision dividend.</param>
        /// <param name="value2">A double precision divisor.</param>
        public double Modulus(double value1, double value2)
        {
            return value1 % value2;
        }
    }
}
