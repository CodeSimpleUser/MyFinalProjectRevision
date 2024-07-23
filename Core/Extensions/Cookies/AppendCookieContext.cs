using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions.Cookies
{
    public class AppendCookieContext
    {
        public AppendCookieContext(HttpContext context,CookieOptions options,string name, string value)
        {
            Context = context;
            CookieOptions = options;
            CookieName = name;
            CookieValue = value;
        }

        public HttpContext Context { get; set; }
        public CookieOptions CookieOptions { get; set; }
        public string CookieName { get; set; }
        public string CookieValue { get; set; }
        public bool IsConsentNeeded { get; internal set; }
        public bool HasConsent { get; internal set; }
        public bool IssueCookie { get; set; }   
    }
}
