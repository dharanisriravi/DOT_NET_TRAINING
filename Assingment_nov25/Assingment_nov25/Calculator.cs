using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assingment_nov25
{

    public interface ICalculator
    {
        int Add(int a, int b);
    }
    internal class CalCulator : ICalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
