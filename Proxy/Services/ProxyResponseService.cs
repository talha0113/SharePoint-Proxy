using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Proxy
{
    public class ProxyResponseService
    {
        private readonly ProxySetting setting;
        private readonly IHostingEnvironment environment;

        public ProxyResponseService(IOptions<ProxySetting> setting, IHostingEnvironment environment)
        {
            this.setting = setting.Value;
            this.environment = environment;
        }
        public async Task HandleRequest(HttpRequestMessage requestMessage, HttpResponse response)
        {
            var httpHandler = new HttpClientHandler();
            
            httpHandler.CookieContainer = await OnlineSOAPManager.Cookies(setting, (new DirectoryInfo(Directory.GetCurrentDirectory())).FullName + Path.DirectorySeparatorChar.ToString() + "Documents");

            using (var httpClient = new HttpClient(httpHandler))
            {
                using (var responseMessage = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.StatusCode = (int)responseMessage.StatusCode;
                    foreach (var header in responseMessage.Headers)
                    {
                        response.Headers[header.Key] = header.Value.ToArray();
                    }
                    foreach (var header in responseMessage.Content.Headers)
                    {
                        response.Headers[header.Key] = header.Value.ToArray();
                    }
                    response.Headers.Remove("transfer-encoding");
                    await responseMessage.Content.CopyToAsync(response.Body);
                }
            }
        }
    }
}
