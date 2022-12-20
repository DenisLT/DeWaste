using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeWaste.Services
{
    interface IFileHandler
    {
        Task AppendDataToFileAsync(string logFileName, string message);
        Task ClearFile(string logFileName);
        Task<string> ReadFileContentsAsync(string itemsPath);
        Task WriteDataToFileAsync(string itemsPath, string data);
    }
}
