using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.AwsCloudWatch;
using System.Globalization;

namespace DemoLambda.Application.Factories
{
    public static class LoggerFactory
    {
        private static string serilogOutputTemplate { get; set; } = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties}{NewLine}{Exception}";
        public static ILogger CreateLogger()
        {
            var logger = new LoggerConfiguration()
                .Enrich.WithProperty("CorrelationId", Guid.NewGuid().ToString())
                         .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
                         .WriteTo.Debug(restrictedToMinimumLevel: LogEventLevel.Information, outputTemplate: serilogOutputTemplate)
                         .WriteTo.Console(formatter: new JsonFormatter(formatProvider: new CultureInfo("pt-BR")), restrictedToMinimumLevel: LogEventLevel.Information)
                         .WriteTo.AmazonCloudWatch(logGroup: "dotnet/LambdaDemo", new DefaultLogStreamProvider());

            Log.Logger = logger.CreateLogger();
            return Log.Logger;
        }

    }
}
