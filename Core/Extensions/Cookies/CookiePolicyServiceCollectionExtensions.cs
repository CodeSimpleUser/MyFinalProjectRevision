using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions.Cookies
{
    public static class CookiePolicyServiceCollectionExtensions
    {
        public static IServiceCollection AddCookiePolicy(this IServiceCollection services,Action<CookiePolicyOptions> configureOptions)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configureOptions);

            return services.Configure(configureOptions);
        }

        public static IServiceCollection AddCookiePolicy<TService>(this IServiceCollection services,Action<CookiePolicyOptions,TService> configureOptions)
            where TService : class
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configureOptions);

            services.AddOptions<CookiePolicyOptions>().Configure(configureOptions);
            return services;
        }
    }
}
