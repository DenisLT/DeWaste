using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using IDbCommandInterceptor = Microsoft.EntityFrameworkCore.Diagnostics.IDbCommandInterceptor;

namespace DeWasteApi.Interceptors
{
    public class TimeLogInterceptor : IDbCommandInterceptor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public int NonQueryExecuting(
            DbCommand command,
            CommandEventData eventData,
            int result)
        {
            _stopwatch.Restart();
            return result;
        }

        public int NonQueryExecuted(
            DbCommand command,
            CommandEventData eventData,
            int result)
        {
            _stopwatch.Stop();
            LogExecutedCommand("Non-queryExecuted", _stopwatch.ElapsedMilliseconds);
            return result;
        }

        public object ScalarExecuting(
            DbCommand command,
            CommandEventData eventData,
            object result)
        {
            _stopwatch.Restart();
            return result;
        }

        public object ScalarExecuted(
            DbCommand command,
            CommandEventData eventData,
            object result)
        {
            _stopwatch.Stop();
            LogExecutedCommand("ScalarExecuted", _stopwatch.ElapsedMilliseconds);
            return result;
        }

        public DbDataReader ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            DbDataReader result)
        {
            _stopwatch.Restart();
            return result;
        }

        public DbDataReader ReaderExecuted(
            DbCommand command,
            CommandEventData eventData,
            DbDataReader result)
        {
            _stopwatch.Stop();
            LogExecutedCommand("ReaderExecuted", _stopwatch.ElapsedMilliseconds);
            return result;
        }

        private void LogExecutedCommand(string functionName, long elapsedMilliseconds)
        {
            Console.WriteLine($"{functionName} function completed in {elapsedMilliseconds}ms");
        }
    }
}