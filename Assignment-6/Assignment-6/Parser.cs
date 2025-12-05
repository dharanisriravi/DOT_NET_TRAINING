using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_6
{
    public interface IParser
    {
        bool TryParse(string input, out string number);
    }
}