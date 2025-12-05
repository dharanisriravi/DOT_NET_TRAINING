using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_6
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