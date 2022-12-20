using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeWasteApi.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync(ex);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        private async Task LogExceptionAsync(Exception ex)
        {
            using (var writer = File.AppendText("exceptions.txt"))
            {
                await writer.WriteLineAsync(ex.ToString());
            }
        }
    }
}
