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
    public class CookieSignedInContext : PrincipalContext<CookieAuthenticationOptions>
    {
        public CookieSignedInContext(
            HttpContext context,
            AuthenticationScheme scheme,
            ClaimsPrincipal principal,
            AuthenticationProperties properties,
            CookieAuthenticationOptions options)
            : base(context, scheme, options, properties)
        {
            Principal = principal;
        }
    }
}
