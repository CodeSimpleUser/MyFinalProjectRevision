using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Authentication.Cookies
{
    public static class CookieAppBuilderExtensions
    {
        [Obsolete("UseCookieAuthentication is obsolete. Configure Cookie auhentication with AddAuthentication().AddCookie in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.\", error: true")]
        public static IApplicationBuilder UseCookiesAuhentication (this IApplicationBuilder app) 
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }
        [Obsolete("UseCookieAuthentication is obsolete. Configure Cookie authentication with AddAuthentication().AddCookie in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
        public static IApplicationBuilder UseCookiesAuthentication(this IApplicationBuilder app, CookieAuthenticationOptions options)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }
    }
}
