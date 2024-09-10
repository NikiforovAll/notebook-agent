using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

public static class ServiceDefaults
{
    public static IHostApplicationBuilder ConfigureOpenTelemetry(IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        AppContext.SetSwitch("Microsoft.SemanticKernel.Experimental.GenAI.EnableOTelDiagnostics", true);
        AppContext.SetSwitch("Microsoft.SemanticKernel.Experimental.GenAI.EnableOTelDiagnosticsSensitive", true);

        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        builder
            .Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
                metrics.AddHttpClientInstrumentation().AddMeter("Microsoft.SemanticKernel*")
            )
            .WithTracing(tracing =>
            {
                tracing.SetSampler(new AlwaysOnSampler());

                tracing.AddHttpClientInstrumentation().AddSource("Microsoft.SemanticKernel*");
            });

        AddOpenTelemetryExporters(builder);

        return builder;
    }

    private static IHostApplicationBuilder AddOpenTelemetryExporters(IHostApplicationBuilder builder)
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(
            builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]
        );

        if (useOtlpExporter)
        {
            builder.Services.Configure<OpenTelemetryLoggerOptions>(logging =>
                logging.AddOtlpExporter()
            );
            builder.Services.ConfigureOpenTelemetryMeterProvider(metrics =>
                metrics.AddOtlpExporter()
            );
            builder.Services.ConfigureOpenTelemetryTracerProvider(tracing =>
                tracing.AddOtlpExporter()
            );
        }

        return builder;
    }
}