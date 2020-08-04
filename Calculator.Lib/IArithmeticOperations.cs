using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Lib
{
    interface IArithmeticOperations
    {
        double Add(double value1, double value2);
        double Subtract(double value1, double value2);
        double Multiply(double value1, double value2);
        double Divide(double dividened, double divisor);
        double Modulus(double value1, double value2);
    }
}
