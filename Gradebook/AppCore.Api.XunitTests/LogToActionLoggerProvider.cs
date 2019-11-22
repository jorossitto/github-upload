using AppCore.Data;
using AppCore.Domain;
using AppCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System;
using System.Linq;
using Xunit;
using Microsoft.Extensions.Logging;

namespace AppCore.Api.XunitTests
{
    internal class LogToActionLoggerProvider : ILoggerProvider
    {
        private readonly Action<string> efCoreLogAction;
        private readonly LogLevel logLevel;

        public LogToActionLoggerProvider(Action<string> efCoreLogAction, 
            LogLevel logLevel = LogLevel.Information)
        {
            this.efCoreLogAction = efCoreLogAction;
            this.logLevel = logLevel;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new EFCoreLogger(efCoreLogAction, logLevel);
        }

        public void Dispose()
        {
            // nothing to dispose
        }
    }
}
