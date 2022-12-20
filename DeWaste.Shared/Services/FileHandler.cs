using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace DeWaste.Services
{
    public class FileHandler : IFileHandler
    {
        SemaphoreSlim mutex = new SemaphoreSlim(1, 1);
        
        private async Task<Stream> OpenFileAsync(string fileName)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
            return stream.AsStream();
        }

        private async Task<Stream> GetStreamAsync(string fileName)
        {
            var stream = await OpenFileAsync(fileName);
            return stream;
        }
        
        public async Task ClearStream(Stream stream)
        {
            stream.SetLength(0);
        }

        public async Task ClearFile(string fileName)
        {
            await mutex.WaitAsync();
            using (var stream = await GetStreamAsync(fileName))
            {
                await ClearStream(stream);
            }
            mutex.Release();
        }
         


        public async Task<string> ReadFileContentsAsync(string fileName)
        {
            await mutex.WaitAsync();
            using (var stream = await GetStreamAsync(fileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    string data = await reader.ReadToEndAsync();
                    mutex.Release();
                    return data;
                }
            }
        }

        public async Task WriteDataToFileAsync(string fileName, string content)
        {
            await mutex.WaitAsync();
            byte[] data = Encoding.UTF8.GetBytes(content);
            using (Stream stream = await GetStreamAsync(fileName))
            {
                await ClearStream(stream);
                await stream.WriteAsync(data, 0, data.Length);
                await stream.FlushAsync();
            }
            mutex.Release();
        }
        
        public async Task AppendDataToFileAsync(string fileName, string content)
        {
            await mutex.WaitAsync();
            byte[] data = Encoding.UTF8.GetBytes(content);
            using (Stream stream = await GetStreamAsync(fileName))
            {
                await stream.WriteAsync(data, 0, data.Length);
                await stream.FlushAsync();
                stream.Dispose();
            }
            mutex.Release();
        }
    }
}
