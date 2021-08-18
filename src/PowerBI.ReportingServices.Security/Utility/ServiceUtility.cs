using System;

using PowerBI.ReportingServices.Security.Abstractions;
using PowerBI.ReportingServices.Security.Options;
using PowerBI.ReportingServices.Security.Services;
using Microsoft.Extensions.DependencyInjection;

namespace PowerBI.ReportingServices.Security
{
    static class ServiceUtility
    {
        private static readonly IServiceCollection services;
        private static readonly IServiceProvider provider;

        static ServiceUtility()
        {
            services = new ServiceCollection();
            AddServices();
            provider = services.BuildServiceProvider();
        }

        private static void AddServices()
        {
            services.AddSingleton<IOptions<CasOptions>, Options<CasOptions>>();
            services.AddSingleton<ICasService, CasService>();

            services.AddScoped<UserAccountContext>();
            services.AddScoped<IUserService, UserService>();
        }

        public static IServiceProvider Provider => provider;
    }
}
