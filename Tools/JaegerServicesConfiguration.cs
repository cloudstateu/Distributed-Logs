using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Contrib.NetCore.Configuration;

namespace Common
{
    public static class JaegerServicesConfiguration
    {
        public static IServiceCollection AddJaegerLogging(this IServiceCollection services)
        {
            services.AddOpenTracing();

            services.AddSingleton<ITracer>(_ =>
            {
                var serviceName = _.GetRequiredService<IWebHostEnvironment>().ApplicationName;
                var loggerFactory = _.GetRequiredService<ILoggerFactory>();
                var reporter = new RemoteReporter
                    .Builder()
                    .WithLoggerFactory(loggerFactory)
                    .WithSender(new UdpSender("10.5.0.5", 0, 0)) // TODO Get from configuration
                    .Build();

                var tracer = new Tracer.Builder(serviceName)
                    .WithSampler(new ConstSampler(true)) // The constant sampler reports every span.
                    .WithReporter(reporter) // LoggingReporter prints every reported span to the logging framework.
                    .Build();

                return tracer;
            });

            services.Configure<HttpHandlerDiagnosticOptions>(_ =>
                _.OperationNameResolver =
                    request => $"{request.Method.Method}::{request?.RequestUri?.AbsoluteUri}");

            return services;
        }
    }
}
