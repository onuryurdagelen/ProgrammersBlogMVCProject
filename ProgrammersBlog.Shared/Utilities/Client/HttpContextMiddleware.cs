namespace Consumer.Client
{
    public class HttpContextMiddleware:DelegatingHandler
    {
        private string _ctor;
        private HttpContext httpContext;

        public HttpContextMiddleware(IHttpContextAccessor httpContextAccessor)
        {
            _ctor = Guid.NewGuid().ToString();
            httpContext = httpContextAccessor.HttpContext;
        }

        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if(httpContext.User == null)
            {

            }

            var method = Guid.NewGuid().ToString();

            request.Headers.Add("Middleware-Ctor", _ctor);
            request.Headers.Add("Middleware-Method", method);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
