using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions.Cookies
{
    public class DeleteCookieContext
    {
        public DeleteCookieContext(HttpContext context, CookieOptions options, string name)
        {
            Context = context;
            CookieOptions = options;
            CookieName = name;
        }
        public HttpContext Context { get; set; }
        public CookieOptions CookieOptions { get; }
        public string CookieName { get; set; }
        public bool IsConsentNeeded { get; internal set; }
        public bool HasConsent { get; internal set; }
        public bool IssueCookie { get; internal set; }
    }
}
