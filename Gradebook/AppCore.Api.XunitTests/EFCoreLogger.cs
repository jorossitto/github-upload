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
    internal class EFCoreLogger : ILogger
    {
        private readonly Action<string> efCoreLogAction;
        private readonly LogLevel logLevel;

        public EFCoreLogger(Action<string> efCoreLogAction, LogLevel logLevel)
        {
            this.efCoreLogAction = efCoreLogAction;
            this.logLevel = logLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= this.logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            this.efCoreLogAction($"Loglevel: {logLevel}, {state}");
        }
    }
}
