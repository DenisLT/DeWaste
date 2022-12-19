using DeWaste.Logging;
using DeWaste.Models.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeWaste.WebServices
{
    public abstract class WebApiBase
    {
        // Insert variables below here
        protected static HttpClient _client;
        protected static IServiceProvider container = App.Container;
        protected static NavigationViewModel ViewModel;
        protected static ILogger logger;

        // Insert static constructor below here
        static WebApiBase()
        {
            //bypass SSL certificate validation
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            _client = new HttpClient(clientHandler);
            ViewModel = container?.GetService(typeof(NavigationViewModel)) as NavigationViewModel;
            logger = container?.GetService(typeof(ILogger)) as ILogger;
        }

        // Insert CreateRequestMessage method below here
        private HttpRequestMessage CreateRequestMessage(HttpMethod method, string url, Dictionary<string, string> headers = null)
        {
            var httpRequestMessage = new HttpRequestMessage(method, url);
            if (headers != null && headers.Any())
            {
                foreach (var header in headers)
                {
                    httpRequestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            return httpRequestMessage;
        }

        // Insert GetAsync method below here
        protected async Task<string> GetAsync(string url, Dictionary<string, string> headers = null)
        {
            using (var request = CreateRequestMessage(HttpMethod.Get, url, headers))
            {
                try
                {
                    var response = await _client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        if(ViewModel != null)
                            ViewModel.FailedConnectToServer = false;
                        return await response.Content.ReadAsStringAsync();
                    }

                    throw new HttpRequestException(response.ReasonPhrase);
                }
                catch (Exception exception)
                {
                    if (ViewModel != null)
                        ViewModel.FailedConnectToServer = true;
                    logger.Log(exception.ToString());
                }
                return null;
            }
        }

        // Insert DeleteAsync method below here

        protected async Task<string> DeleteAsync(string url, Dictionary<string, string> headers = null)
        {
            using (var request = CreateRequestMessage(HttpMethod.Delete, url, headers))
            {
                try
                {
                    var response = await _client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        if (ViewModel != null)
                            ViewModel.FailedConnectToServer = false;
                        return await response.Content.ReadAsStringAsync();
                    }

                    throw new HttpRequestException(response.ReasonPhrase);
                }
                catch (Exception exception)
                {
                    if (ViewModel != null)
                        ViewModel.FailedConnectToServer = true;
                    logger.Log(exception.ToString());
                }
                return null;
            }
        }

        // Insert PostAsync method below here

        protected async Task<string> PostAsync(string url, string content, Dictionary<string, string> headers = null)
        {
            using (var request = CreateRequestMessage(HttpMethod.Post, url, headers))
            {
                try
                {
                    request.Content = new StringContent(content, Encoding.UTF8, "application/json");

                    var response = await _client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        if (ViewModel != null)
                            ViewModel.FailedConnectToServer = false;
                        return await response.Content.ReadAsStringAsync();
                    }

                    throw new HttpRequestException(response.ReasonPhrase);
                }
                catch (Exception exception)
                {
                    if (ViewModel != null)
                        ViewModel.FailedConnectToServer = true;
                    logger.Log(exception.ToString());
                }
                return null;
            }
        }

        // Insert PutAsync method below here

        protected async Task<string> PutAsync(string url, string content, Dictionary<string, string> headers = null)
        {
            using (var request = CreateRequestMessage(HttpMethod.Put, url, headers))
            {
                try
                {
                    request.Content = new StringContent(content, Encoding.UTF8, "application/json");

                    var response = await _client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        if (ViewModel != null)
                            ViewModel.FailedConnectToServer = false;
                        return await response.Content.ReadAsStringAsync();
                    }

                    throw new HttpRequestException(response.ReasonPhrase);
                }
                catch (Exception exception)
                {
                    if (ViewModel != null)
                        ViewModel.FailedConnectToServer = true;
                    logger.Log(exception.ToString());
                }
                return null;
            }
        }


    }
}
