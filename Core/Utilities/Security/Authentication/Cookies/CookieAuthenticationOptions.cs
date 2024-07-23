using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Internal;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Authentication.Cookies
{
    public class CookieAuthenticationOptions : AuthenticationSchemeOptions
    {
        private CookieBuilder _cookieBuilder = new RequestPathBaseCookieBuilder
        {
            // the default name is configured in PostConfigureCookieAuthenticationOptions

            // To support OAuth authentication, a lax mode is required, see https://github.com/aspnet/Security/issues/1231.
            SameSite = SameSiteMode.Lax,
            HttpOnly = true,
            SecurePolicy = CookieSecurePolicy.SameAsRequest,
            IsEssential = true,
        };
        public CookieAuthenticationOptions()
        {
            ExpireTimeSpan = TimeSpan.FromDays(14);
            ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            SlidingExpiration = true;
            Events = new CookieAuthenticationEvents();
        }
        public CookieBuilder Cookie
        {
            get => _cookieBuilder;
            set => _cookieBuilder = value ?? throw new ArgumentNullException(nameof(value));
        }
        public IDataProtectionProvider DataProtectionProvider { get; set; }
        public bool SlidingExpiration { get; set; }
        public PathString LoginPath { get; set; }
        public PathString LogoutPath { get; set; }
        public PathString AccessDeniedPath { get; set; }
        public string ReturnUrlParameter { get; set; }
        public new CookieAuthenticationEvents Events
        {
            get => (CookieAuthenticationEvents)base.Events;
            set => base.Events = value;
        }
        public ISecureDataFormat<AuthenticationTicket> TicketDataFormat { get; set; }
        public ICookieManager CookieManager { get; set; }
        public ITicketStore SessionStore { get; set; }
        public TimeSpan ExpireTimeSpan { get; set; }

    }
}
