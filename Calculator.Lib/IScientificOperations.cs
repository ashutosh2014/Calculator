using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Lib
{
    interface IScientificOperations
    {
        double LogBase10(double value);
        double LogBaseE(double value);
        double Exponential(double value);
        double SquareRoot(double value);
        double Sine(double value);
        double Cosine(double value);
        double Tangent(double value);
        double Power(double value1, double value2);
    }
}
