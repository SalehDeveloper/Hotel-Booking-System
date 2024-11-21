using Hangfire;
using HootelBooking.Application.Contracts;
using HootelBooking.Persistence.Jobs;
using HootelBooking.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Persistence.Services
{
    public static class HangfireConfiguration
    {
        


        public static IServiceCollection ConfigureHangfireJobs(this IServiceCollection services)
        {

            services.AddScoped<ExpiredCodesJob>();

            services.AddSingleton<IRecurringJobManager>(provider =>
            {
                var jobManager = provider.GetRequiredService<IRecurringJobManager>();
                jobManager.AddOrUpdate<ExpiredCodesJob>(
                    "ClearExpiredPasswordCodes",
                    job => job.ClearExpiredCodesAsync(),
                    "*/2 * * * *");  // Run every 2 minutes
                return jobManager;
            });

            services.AddScoped<ExpiredReservationsJob>();
            services.AddSingleton<IRecurringJobManager>(provider => 
            {

                var jobManager = provider.GetRequiredService<IRecurringJobManager>();
                jobManager.AddOrUpdate<ExpiredReservationsJob>(
                    "ClearExpiredReservations" , 
                    job => job.ClearExpiredReservationsAsync(),
                   Cron.Hourly
                    );
             return jobManager;
            });

            return services;
        }
    }
}
