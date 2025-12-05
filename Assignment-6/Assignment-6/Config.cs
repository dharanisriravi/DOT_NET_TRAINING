using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_6
{
    public interface IConfig
    {
        string Environment { get; set; }
    }
    public class EnvironmentChecker
    {
        private readonly IConfig _config;

        public EnvironmentChecker(IConfig config)
        {
            _config = config;
        }

        public bool IsProduction()
        {
            return _config.Environment == "Production";
        }
    }

}