using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Authentication.Cookies
{
    public class CookieAuthenticationEvents
    {
        public Func<CookieValidatePrincipalContext,Task> OnValidatePrincipal {  get; set; } = context => Task.CompletedTask;
        public Func<CookieSignedInContext,Task> OnSigningIn { get; set; } = context => Task.CompletedTask;
        public Func<CookieSignedInContext, Task> OnSignedIn { get; set; } = context => Task.CompletedTask;
        public Func<CookieSigningOutContext, Task> OnSigningOut { get; set; } = context => Task.CompletedTask;
        public Func<RedirectContext<CookieAuthenticationOptions>, Task> OnRedirectToLogin { get; set; } = context =>
        {
            if (IsAjaxRequest(context.Request))
            {
                context.Response.Headers["Location"] = context.RedirectUri;
                context.Response.StatusCode = 401;
            }
            else
            {
                context.Response.Redirect(context.RedirectUri);
            }
            return Task.CompletedTask;
        };
        public Func<RedirectContext<CookieAuthenticationOptions>, Task> OnRedirectToAccessDenied { get; set; } = context =>
        {
            if (IsAjaxRequest(context.Request))
            {
                context.Response.Headers["Location"] = context.RedirectUri;
                context.Response.StatusCode = 403;
            }
            else
            {
                context.Response.Redirect(context.RedirectUri);
            }
            return Task.CompletedTask;
        };
        public Func<RedirectContext<CookieAuthenticationOptions>, Task> OnRedirectToLogout { get; set; } = context =>
        {
            if (IsAjaxRequest(context.Request))
            {
                context.Response.Headers["Location"] = context.RedirectUri;
            }
            else
            {
                context.Response.Redirect(context.RedirectUri);
            }
            return Task.CompletedTask;
        };
        public Func<RedirectContext<CookieAuthenticationOptions>, Task> OnRedirectToReturnUrl { get; set; } = context =>
        {
            if (IsAjaxRequest(context.Request))
            {
                context.Response.Headers["Location"] = context.RedirectUri;
            }
            else
            {
                context.Response.Redirect(context.RedirectUri);
            }
            return Task.CompletedTask;
        };
        private static bool IsAjaxRequest(HttpRequest request)
        {
            return string.Equals(request.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal);
        }
        public virtual Task ValidatePrincipal(CookieValidatePrincipalContext context) => OnValidatePrincipal(context);
        //public virtual Task SigningIn(CookieSigningInContext context) => OnSigningIn(context);
        public virtual Task SignedIn(CookieSignedInContext context) => OnSignedIn(context);
        public virtual Task SigningOut(CookieSigningOutContext context) => OnSigningOut(context);
        public virtual Task RedirectToLogout(RedirectContext<CookieAuthenticationOptions> context) => OnRedirectToLogout(context);
        public virtual Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context) => OnRedirectToLogin(context);
        public virtual Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context) => OnRedirectToReturnUrl(context);
        public virtual Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context) => OnRedirectToAccessDenied(context);
    }
}
