using System.Reflection;
using HealthChecks.UI.Client;
using MetricsAPI.Application;
using MetricsAPI.Application.ApiHelpers.Configurations;
using MetricsAPI.Application.ApiHelpers.Middlewares;
using MetricsAPI.BackgroundTasks;
using MetricsAPI.Common.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.ResponseCompression;
using Prometheus;
using Prometheus.Client.AspNetCore;
using Prometheus.Client.HttpRequestDurations;
using SharpCompress.Compressors.Deflate;

#region BuilderRegion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMediatr();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes;
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Optimal;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.SmallestSize;
});

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddLoggingExtension(builder.Configuration);

builder.Services.AddCaching();

builder.Services.AddSwachbackleService(Assembly.GetExecutingAssembly());

builder.Services.AddValidators();

builder.Services.AddBackgroundTasks();

builder.Services.AddApplication();

#endregion

#region ApplicationRegion

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwaggerApp();
}

app.UseResponseCompression();

app.UseCors();

UseMetrics();

app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

UseCustomMiddlewares();

app.Run();

#endregion

#region UseMiddlewaresRegion

void UseCustomMiddlewares()
{
    if (app is null)
        throw new ArgumentException();

    builder.Services.AddTransient<ResponseCachingMiddleware>();
    
    app.UseMiddleware<RequestLoggingMiddleware>(app.Logger);
    app.UseMiddleware<ResponseCachingMiddleware>();
}

void UseMetrics()
{
    if (app is null)
        throw new ArgumentException();
    
    app.UseMetricServer();
    app.UseHttpMetrics();
    app.UsePrometheusServer();
    app.UsePrometheusRequestDurations();
}

#endregion
