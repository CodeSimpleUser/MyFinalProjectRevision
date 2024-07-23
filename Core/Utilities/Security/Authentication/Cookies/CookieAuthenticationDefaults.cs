using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Authentication.Cookies
{
    public class CookieAuthenticationDefaults
    {
        public const string AuthenticationScheme = "Cookies";
        public static readonly string CookiePrefix = ".AspNetCore.";
        public static readonly PathString LoginPath = new PathString("auth/login");
        public static readonly PathString LogoutPath = new PathString("auth/logout");
        public static readonly PathString AccessDeniedPath = new PathString("auth/accessdenied");
        public static readonly string ReturnUrlParameter = "ReturnUrl";
    }
}
