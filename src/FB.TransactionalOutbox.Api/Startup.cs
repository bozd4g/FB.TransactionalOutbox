using System;
using Autofac;
using FB.TransactionalOutbox.Api.Extensions;
using FB.TransactionalOutbox.Api.Middlewares;
using FB.TransactionalOutbox.Application.Contracts;
using FB.TransactionalOutbox.Application.Modules;
using FB.TransactionalOutbox.EntityFrameworkCore;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FB.TransactionalOutbox.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppSettings(Configuration);
            services.AddSwagger(Configuration);
            services.AddMapper();
            services.AddHttpContextAccessor();

            services
                    .AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });


            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("FBTransactionalOutboxDb")
                           .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

            services.AddMediatR(typeof(Startup));
            services.AddHangfire();
            services.AddRabbitMq(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, IWebHostEnvironment env)
        {
            var settings = new AppSettings();
            Configuration.GetSection("AppSettings").Bind(settings);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseMiddleware<UserFriendlyExceptionMiddleware>();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

            app.UseCors(x => x
                             .AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(e =>
            {
                e.SwaggerEndpoint($"/swagger/{settings.Swagger.Version}/swagger.json", settings.Swagger.Title);
            });
            app.UseHangfire(serviceProvider);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new MediatorModule());
        }
    }
}