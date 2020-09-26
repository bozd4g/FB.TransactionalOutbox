using System;
using FB.TransactionalOutbox.Infrastructure.Hangfire;
using FB.TransactionalOutbox.Infrastructure.Hangfire.Jobs;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FB.TransactionalOutbox.Infrastructure.Extensions
{
    public static class HangfireExtension
    {
        public static void AddHangfire(this IServiceCollection services)
        {
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                                                  .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                                  .UseSimpleAssemblyNameTypeSerializer()
                                                  .UseRecommendedSerializerSettings()
                                                  .UseMemoryStorage());
            services.AddHangfireServer();
        }

        public static void UseHangfire(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseHangfireDashboard();
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(serviceProvider));

            RecurringJob.RemoveIfExists(nameof(OutboxTableReadJob));
            RecurringJob.AddOrUpdate<OutboxTableReadJob>(nameof(OutboxTableReadJob), job => job.Start(), Cron.Minutely);
        }
    }
}