using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace FB.TransactionalOutbox.Infrastructure.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddMapper(this IServiceCollection services, Type startupType)
        { 
            services.AddAutoMapper(startupType);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps(new []
                {
                    "FB.TransactionalOutbox.Domain",
                    "FB.TransactionalOutbox.Application",
                    "FB.TransactionalOutbox.Application.Contracts",
                });
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}