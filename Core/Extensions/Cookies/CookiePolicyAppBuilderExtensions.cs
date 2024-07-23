using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions.Cookies
{
    public static class CookiePolicyAppBuilderExtensions
    {
        public static IApplicationBuilder UseCookiePolicy(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(nameof(app));

            return app.UseMiddleware<CookiePolicyMiddleware>();
        }

        public static  IApplicationBuilder UseCookiePolicy(this IApplicationBuilder app, CookiePolicyOptions options)
        {
            ArgumentNullException.ThrowIfNull(app);
            ArgumentNullException.ThrowIfNull(options);

            return app.UseMiddleware<CookiePolicyMiddleware>(Options.Create(options));
        }
    }
}
