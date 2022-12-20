using DeWaste.Logging;
using DeWaste.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeWaste.Logging
{
    public class DummyLogger : ILogger
    {
        public void Log(string message)
        {
            
        }

        public DummyLogger(IServiceProvider container)
        {
        }
    }
}
