using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_6
{
    public interface DataReader
    {
        string ReadLine();
    }
    public class Line
    {
        private readonly DataReader _reader;

        public Line(DataReader reader)
        {
            _reader = reader;
        }

        public List<string> ReadThreeLines()
        {
            return new List<string>
        {
            _reader.ReadLine(),
            _reader.ReadLine(),
            _reader.ReadLine()
        };
        }
    }
}

