using System.Reflection;
using HealthChecks.UI.Client;
using MetricsAPI.Application;
using MetricsAPI.Application.ApiHelpers.Configurations;
using MetricsAPI.Application.ApiHelpers.Middlewares;
using MetricsAPI.BackgroundTasks;
using MetricsAPI.Common.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;
using Prometheus.Client.AspNetCore;
using Prometheus.Client.HttpRequestDurations;

#region BuilderRegion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMediatr();

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
