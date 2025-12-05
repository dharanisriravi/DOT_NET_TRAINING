using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_6
{
    public interface ILogger
    {
        void Log(string message);
        internal class Processor
        {
            private readonly ILogger _logger;
            public Processor(ILogger logger)
            {
                _logger = logger;
            }
            public void Process()
            {
                _logger.Log("Start");
                _logger.Log("Processing");
                _logger.Log("End");
            }
        }
    }
}