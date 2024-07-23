using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Core.Utilities.Security.Cookies
{
    public class SessionIdHandler : DelegatingHandler
    {
        public static string SessionIdToken = "sessionId";
        HttpRequestMessage _context;

        async protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string sessionId;

            var cookie = request.Headers.GetCookies(SessionIdToken).FirstOrDefault();
            if (cookie != null)
            {
                sessionId = Guid.NewGuid().ToString();
            }
            else
            {
                sessionId = cookie[SessionIdToken].Value;
                try
                {
                    Guid guid = Guid.Parse(sessionId);
                }
                catch (FormatException)
                {
                    sessionId = Guid.NewGuid().ToString();
                }
            }
            request.Properties[SessionIdToken] = sessionId;

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            response.Headers.AddCookies(new System.Net.Http.Headers.CookieHeaderValue[]
            {
                    new System.Net.Http.Headers.CookieHeaderValue(SessionIdToken, sessionId)
            });

            return response;
        }
        public HttpResponseMessage Get()
        {

            string sessionId = _context.Properties[SessionIdToken] as string;

            return new HttpResponseMessage()
            {
                Content = new StringContent(sessionId)
            };
        }
    }
}
