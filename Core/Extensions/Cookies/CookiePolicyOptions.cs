using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions.Cookies
{
    public class CookiePolicyOptions
    {
        private string _consentCookieValue = "yes";
        public SameSiteMode MinimumSameSitePolicy { get; set; } = SameSiteMode.Unspecified;
        public HttpOnlyPolicy HttpOnly { get; set; } = HttpOnlyPolicy.None;
        public CookieSecurePolicy Secure { get; set; } = CookieSecurePolicy.None;
        public CookieBuilder ConsentCookie { get; set; } = new CookieBuilder()
        {
            Name = ".AspNet.Consent",
            Expiration = TimeSpan.FromDays(1),
            IsEssential = true,
        };
        public string ConsentCookieValue
        {
            get => _consentCookieValue;
            set
            {
                ArgumentException.ThrowIfNullOrEmpty(value);
                _consentCookieValue = value;
            }
        }
        public Func<HttpContext, bool>? CheckConsentNeeded { get; set; }
        public Action<AppendCookieContext>? OnAppendCookie { get; set; }
        public Action<DeleteCookieContext>? OnDeleteCookie { get; set; }
    }
}
