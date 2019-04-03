using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Proxy.Test
{
    static class ProxyTestServer
    {
        public static TestServer Initialize()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName).AddJsonFile("appsettings.Test.json").AddEnvironmentVariables().Build();
            return new TestServer(new WebHostBuilder().UseConfiguration(config).ConfigureServices(services => {
                services.Configure<ProxySetting>(config.GetSection("ProxySetting"));
                services.AddSingleton<ProxyRequestService>();
                services.AddSingleton<ProxyResponseService>();
            }).Configure(configure => {
                configure.MapWhen(context => context.Request.Path.Value != "/", (appBuilder) =>
                {
                    appBuilder.UseMiddleware<SharePointProxyMiddleware>();
                });
            }).UseEnvironment("Development").UseStartup<Startup>());
        }
    }
}
