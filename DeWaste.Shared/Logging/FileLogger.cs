using DeWaste.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeWaste.Logging
{
    internal class FileLogger : ILogger
    {
        string logFileName = "log.txt";

        public FileLogger()
        {
            FileHandler.ClearFile(logFileName);
        }
        
        public void Log(string message)
        {
            FileHandler.AppendDataToFileAsync(logFileName, message);
        }
    }
}
