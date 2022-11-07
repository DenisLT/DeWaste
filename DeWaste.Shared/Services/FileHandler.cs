using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace DeWaste.Services
{
    public static class FileHandler
    {

        private static Dictionary<string, Stream> openedFiles = new Dictionary<string, Stream>();

        static SemaphoreSlim mutex = new SemaphoreSlim(1, 1);

        private static async Task<Stream> OpenFileAsync(string fileName)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
            return stream.AsStream();
        }

        private static async Task<Stream> GetStreamAsync(string fileName)
        {
            if (openedFiles.ContainsKey(fileName))
            {
                return openedFiles[fileName];
            }
            else
            {
                var stream = await OpenFileAsync(fileName);
                openedFiles.Add(fileName, stream);
                return stream;
            }
        }

        public static async Task ClearFile(string fileName)
        {
            var stream = await GetStreamAsync(fileName);
            stream.SetLength(0);
        }


        public static async Task<string> ReadFileContentsAsync(string fileName)
        {

            try
            {
                Stream stream = await GetStreamAsync(fileName);    

                using (StreamReader reader = new StreamReader(stream, leaveOpen: true))
                {
                    return await reader.ReadToEndAsync();
                }

            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public static async Task WriteDataToFileAsync(string fileName, string content)
        {
            await mutex.WaitAsync();
            byte[] data = Encoding.UTF8.GetBytes(content);
            Stream stream = await GetStreamAsync(fileName);
            await ClearFile(fileName);
            await stream.WriteAsync(data, 0, data.Length);
            await stream.FlushAsync();
            mutex.Release();
        }
        
        public static async Task AppendDataToFileAsync(string fileName, string content)
        {
            await mutex.WaitAsync();
            byte[] data = Encoding.UTF8.GetBytes(content);
            Stream stream = await GetStreamAsync(fileName);
            await stream.WriteAsync(data, 0, data.Length);
            await stream.FlushAsync();
            mutex.Release();
        }
    }
}
