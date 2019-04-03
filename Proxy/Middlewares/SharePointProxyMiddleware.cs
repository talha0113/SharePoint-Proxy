// Inspirations
// https://auth0.com/blog/building-a-reverse-proxy-in-dot-net-core


using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Proxy
{
    public class SharePointProxyMiddleware
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly RequestDelegate next;
        private readonly ProxySetting setting;
        private readonly ProxyRequestService requestService;
        private readonly ProxyResponseService responseService;
        private readonly ILogger logger;

        public SharePointProxyMiddleware(RequestDelegate next, IOptions<ProxySetting> setting, ProxyRequestService requestService, ProxyResponseService responseService, ILogger<SharePointProxyMiddleware> logger)
        {
            this.next = next;
            this.setting = setting.Value;
            this.requestService = requestService;
            this.responseService = responseService;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
               
                if (setting.IsOnline)
                {
                    var requestMessage = requestService.Online(context.Request, setting);
                    await responseService.HandleRequest(requestMessage, context.Response);
                    return;
                }
            }
            catch (Exception ex) {
                logger.LogError(ex.Message, ex);
                await next(context);
            }
        }
    }
}
