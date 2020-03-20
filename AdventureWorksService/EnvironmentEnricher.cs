using AdventureWorksService.WebApi;
using Microsoft.Extensions.Options;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksService.WebApi
{
    public class EnvironmentEnricher : ILogEventEnricher
    {
        public const string Environment = "Environment";
        private readonly string _environmentValue;
        public EnvironmentEnricher(string environmentValue):base()
        {
            _environmentValue = environmentValue;
        }
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));

            var envLogEventProperty = new LogEventProperty(Environment, new ScalarValue(_environmentValue));
            logEvent.AddPropertyIfAbsent(envLogEventProperty);
        }
    }
}
