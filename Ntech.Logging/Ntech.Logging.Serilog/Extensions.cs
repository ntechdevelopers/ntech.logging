using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Ntech.Logging.Serilog.Enrichers;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace Ntech.Logging.Serilog
{
    public static class Extensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);
            return model;
        }

        public static IHostBuilder UseLogging(this IHostBuilder hostBuilder, string applicationName = "")
        {
            hostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                var appOptions = context.Configuration.GetOptions<AppOptions>("App");
                var loggingOptions = context.Configuration.GetOptions<LoggingOptions>("Logging");

                applicationName = string.IsNullOrWhiteSpace(applicationName) ? appOptions.Name : applicationName;

                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration, "Logging")
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("ApplicationName", applicationName)
                    .Enrich.With<OpenTracingContextLogEventEnricher>();

                // Config Log Seq
                Configure(loggerConfiguration, loggingOptions.Seq, loggingOptions);

                // Config Log ELK
                var format = $"{context.Configuration["AppName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";
                Configure(loggerConfiguration, loggingOptions.Elk, loggingOptions, format);

                // Config Log File
                Configure(loggerConfiguration, loggingOptions.File, loggingOptions);
            });

            return hostBuilder;
        }

        private static void Configure(LoggerConfiguration loggerConfiguration, SeqOptions seq, LoggingOptions loggingOptions)
        {
            if (seq.Enabled)
            {
                loggerConfiguration.WriteTo.Seq(seq.Url, apiKey: seq.ApiKey);
            }

            if (loggingOptions.ConsoleEnabled)
            {
                loggerConfiguration.WriteTo
                    .Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {Properties:j}] {Message:lj}{NewLine}{Exception}");
            }
        }

        private static void Configure(LoggerConfiguration loggerConfiguration, ELKOptions elk, LoggingOptions loggingOptions, string format)
        {
            if (elk.Enabled)
            {
                loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elk.Url))
                {
                    IndexFormat = format,
                    AutoRegisterTemplate = true,
                    NumberOfShards = 2,
                    NumberOfReplicas = 1
                });
            }

            if (loggingOptions.ConsoleEnabled)
            {
                loggerConfiguration.WriteTo
                    .Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {Properties:j}] {Message:lj}{NewLine}{Exception}");
            }
        }

        private static void Configure(LoggerConfiguration loggerConfiguration, FileOptions file, LoggingOptions loggingOptions)
        {
            if (file.Enabled)
            {
                loggerConfiguration.WriteTo.File(file.PathFile);
            }

            if (loggingOptions.ConsoleEnabled)
            {
                loggerConfiguration.WriteTo
                    .Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {Properties:j}] {Message:lj}{NewLine}{Exception}");
            }
        }

    }
}
