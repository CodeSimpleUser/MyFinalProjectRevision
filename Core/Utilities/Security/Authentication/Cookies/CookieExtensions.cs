//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.DependencyInjection.Extensions;

//namespace Core.Utilities.Security.Authentication.Cookies
//{
//    public static class CookieExtensions
//    {
//        public static AuthenticationBuilder AddCookie(this AuthenticationBuilder builder)
//            => builder.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

//        public static AuthenticationBuilder AddCookie(this AuthenticationBuilder builder, string authenticationScheme)
//            => builder.AddCookie(authenticationScheme, configureOptions: null);

//        public static AuthenticationBuilder AddCookie(this AuthenticationBuilder builder, Action<CookieAuthenticationOptions> configureOptions)
//            => builder.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configureOptions);

//        public static AuthenticationBuilder AddCookie(this AuthenticationBuilder builder, string authenticationScheme, Action<CookieAuthenticationOptions> configureOptions)
//            => builder.AddCookie(authenticationScheme, displayName: null, configureOptions: configureOptions);

//        public static AuthenticationBuilder AddCookie(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<CookieAuthenticationOptions> configureOptions)
//        {
//            //builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<CookieAuthenticationOptions>,PostConfigureCookieAuthenticationOptions>());
//            return builder.AddScheme<CookieAuthenticationOptions, CookieAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
//        }
//    }
//}
