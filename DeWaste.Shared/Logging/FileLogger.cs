using DeWaste.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeWaste.Logging
{
    internal class FileLogger : ILogger
    {
        string logFileName = "log.txt";
        IFileHandler fileHandler;
        IServiceProvider container = ((App)App.Current).Container;

        public FileLogger()
        {
            fileHandler = (IFileHandler)container.GetService(typeof(IFileHandler));
            fileHandler.ClearFile(logFileName);
        }
        
        public void Log(string message)
        {
            fileHandler.AppendDataToFileAsync(logFileName, message);
        }
    }
}
