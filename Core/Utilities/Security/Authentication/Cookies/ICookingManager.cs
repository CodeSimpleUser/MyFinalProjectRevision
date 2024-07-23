using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Authentication.Cookies
{
    public interface ICookingManager
    {
        string GetRequestCookie(HttpContext context, string key);
        void AppendResponseCookir(HttpContext context, string key,string value ,CookieOptions options);
        void DeleteCookie(HttpContext context, string key,CookieOptions options);
    }
}
