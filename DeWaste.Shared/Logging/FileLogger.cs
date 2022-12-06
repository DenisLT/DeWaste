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
        IServiceProvider container = App.Container;

        public FileLogger(IServiceProvider container)
        {
            this.container = container;
            fileHandler = (IFileHandler)container.GetService(typeof(IFileHandler));
            fileHandler.ClearFile(logFileName);
        }


        public void Log(string message)
        {
            fileHandler.AppendDataToFileAsync(logFileName, message);
        }
    }
}
