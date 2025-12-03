using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace todowpf.Services
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ITokenStorage _storage;

        public AuthorizationMessageHandler(ITokenStorage storage)
        {
            _storage = storage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken ct)
        {
            if (!string.IsNullOrWhiteSpace(_storage.Token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",_storage.Token);
            }
            return await base.SendAsync(request, ct);
        }
    }
}
