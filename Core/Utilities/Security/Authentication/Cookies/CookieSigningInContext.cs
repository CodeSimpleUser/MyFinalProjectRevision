using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Authentication.Cookies
{
    public class CookieSigningInContext : PrincipalContext<CookieAuthenticationOptions>
    {
        public CookieSigningInContext(
           HttpContext context,
           AuthenticationScheme scheme,
           CookieAuthenticationOptions options,
           ClaimsPrincipal principal,
           AuthenticationProperties properties,
           CookieOptions cookieOptions)
           : base(context, scheme, options, properties)
        {
            CookieOptions = cookieOptions;
            Principal = principal;
        }

        /// <summary>
        /// The options for creating the outgoing cookie.
        /// May be replace or altered during the SigningIn call.
        /// </summary>
        public CookieOptions CookieOptions { get; set; }
    }
}
