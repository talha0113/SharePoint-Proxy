using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Proxy
{
    public class ProxyRequestService
    {
        private readonly ProxySetting setting;
        public ProxyRequestService(IOptions<ProxySetting> setting)
        {
            this.setting = setting.Value;
        }
        public HttpRequestMessage Online(HttpRequest request, ProxySetting setting)
        {            
            var targetUri = new Uri(setting.BaseUrl + request.Path);
            var requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = targetUri;
            foreach (var header in request.Headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value.ToArray());
            }
            requestMessage.Headers.Host = targetUri.Host;
            requestMessage.Method = new HttpMethod(request.Method);
            return requestMessage;
        }
    }
}
