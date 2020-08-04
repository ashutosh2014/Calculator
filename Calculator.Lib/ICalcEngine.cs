using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Lib
{
    interface ICalcEngine { 
        double Calculate(string expression);
        double Solve(char op, double b, double a);
    }
}
