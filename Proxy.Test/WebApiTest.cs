using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Proxy.Test
{
    [TestClass]
    public class WebApiTest
    {
        private readonly HttpClient httpClient;
        public WebApiTest()
        {
            var server = ProxyTestServer.Initialize();
            httpClient = server.CreateClient();
        }
        [TestMethod]
        public async Task WebDataTest()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/_api/web");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Contains("sharepoint.com"));
        }
    }
}
