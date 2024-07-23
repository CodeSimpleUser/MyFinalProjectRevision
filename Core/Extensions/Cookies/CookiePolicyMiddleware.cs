using Azure.Core;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Builder;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions.Cookies;
    public class CookiePolicyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CookiePolicyMiddleware(RequestDelegate next, IOptions<CookiePolicyOptions> options /*,ILoggerFactory factory*/)
        {
            Options = options.Value;
            _next = next ?? throw new ArgumentNullException(nameof(next));
            //_logger = factory.CreateLogger<CookiePolicyMiddleware>;
        }
        //public CookiePolicyMiddleware(RequestDelegate next, IOptions<CookiePolicyOptions> options)
        //{
        //    Options = options.Value;
        //    _next = next;
        //    //_logger = NullLogger.Instance;
        //}
        public CookiePolicyOptions Options { get; set; }
        public Task Invoke(HttpContext context)
        {
            var feature = context.Features.Get<IResponseCookiesFeature>() ?? new ResponseCookiesFeature(context.Features);
            var wrapper = new ResponseCookiesWrapper(context, Options ,feature);
            context.Features.Set<IResponseCookiesFeature>(new CookiesWrapperFeature(wrapper));
            //context.Features.Set<ITrackingConsentFeature>(wrapper);

            return _next(context);
        }
        private sealed class CookiesWrapperFeature : IResponseCookiesFeature
        {
            public CookiesWrapperFeature(ResponseCookiesWrapper wrapper)
            {
                Cookies = wrapper;
            }
            public IResponseCookies Cookies { get; }
        }
    }

