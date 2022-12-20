using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeWaste.Services
{
    public class FileHandler : IFileHandler
    {
        public Task AppendDataToFileAsync(string logFileName, string message)
        {
            throw new NotImplementedException();
        }

        public Task ClearFile(string logFileName)
        {
            throw new NotImplementedException();
        }

        public Task<string> ReadFileContentsAsync(string itemsPath)
        {
            throw new NotImplementedException();
        }

        public Task WriteDataToFileAsync(string itemsPath, string data)
        {
            throw new NotImplementedException();
        }
    }
}
