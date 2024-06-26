﻿using System.Reflection;
using MetricsAPI.BackgroundTasks.QuartZ;
using MetricsAPI.BackgroundTasks.QuartZ.Jobs;
using MetricsAPI.BackgroundTasks.QuartZ.Schedulers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsAPI.BackgroundTasks;

public static class BDependencyInjection
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddBackgroundTasks(
        this IServiceCollection services)
    {
        services.AddMediatR(x=>
            x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(SaveMetricsJob));

            configure
                .AddJob<SaveMetricsJob>(jobKey);
            
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        
        services.AddTransient<IJobFactory, QuartzJobFactory>();
        services.AddSingleton(_ =>
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;
            
            return scheduler;
        });
        
        services.AddTransient<SaveMetricsScheduler>();
        
        var scheduler = new SaveMetricsScheduler();
        scheduler.Start(services);
        
        return services;
    }
}