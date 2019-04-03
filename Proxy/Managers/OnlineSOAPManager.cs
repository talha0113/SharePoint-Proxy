// Inspirations
// https://paulryan.com.au/2014/spo-remote-authentication-rest
// https://shareyourpoint.net/2017/01/25/operations-using-rest-in-sharepoint-online-authorization/amp


using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Proxy
{
    public static class OnlineSOAPManager
    {
        private static readonly string SecuritTokenUrl = "https://login.microsoftonline.com/extSTS.srf";
        private static readonly string AccessTokenUrl = "#SITEURL#/_forms/default.aspx?wa=wsignin1.0";

        public static async Task<CookieContainer> Cookies(ProxySetting setting, string staticContentFolder) {
            var tokenDocument = new StringBuilder();
            staticContentFolder = staticContentFolder.Replace(".Test\\bin\\Debug\\netcoreapp2.1", "");
            using (var sr = File.OpenText(staticContentFolder + Path.DirectorySeparatorChar.ToString() + "Token.xml"))
            {
                tokenDocument.Append(sr.ReadToEnd());
            }
            tokenDocument = tokenDocument.Replace("#USERNAME#", setting.UserName).Replace("#PASSWORD#", setting.Password).Replace("#SITEURL#", setting.BaseUrl);
            var httpClient = new HttpClient();            
            var response = await httpClient.PostAsync(SecuritTokenUrl, new StringContent(tokenDocument.ToString()));

            var xTemplate = XDocument.Parse(await response.Content.ReadAsStringAsync());
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("S", "http://www.w3.org/2003/05/soap-envelope");
            namespaceManager.AddNamespace("wst", "http://schemas.xmlsoap.org/ws/2005/02/trust");
            namespaceManager.AddNamespace("wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            var securityToken = xTemplate.XPathSelectElement("/S:Envelope/S:Body/wst:RequestSecurityTokenResponse/wst:RequestedSecurityToken/wsse:BinarySecurityToken", namespaceManager);

            var cookieContainer = new CookieContainer();
            var httpHandler = new HttpClientHandler();
            httpHandler.CookieContainer = cookieContainer;
            httpClient = new HttpClient(httpHandler, true);
            var uri = new Uri(AccessTokenUrl.Replace("#SITEURL#", setting.BaseUrl));
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = new StringContent(securityToken.Value);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            response = await httpClient.SendAsync(requestMessage);
            //var cookies = cookieContainer.GetCookies(uri);

            //var cookieContainerOut = new CookieContainer();
            //foreach (var cookie in cookies)
            //{
            //    if(cookie)
            //    cookieContainerOut.Add(oneCookie);
            //    cookieContainerOut.Add(twoCookie);
            //}

            //var oneCookie = cookies[0];
            //var twoCookie = cookies[3];

            
            

            //httpHandler = new HttpClientHandler();
            //httpHandler.CookieContainer = cookieContainer;
            //httpClient = new HttpClient(httpHandler, true);
            //response = await httpClient.PostAsync("#SITEURL#/_api/contextinfo".Replace("#SITEURL#", setting.BaseUrl), null);
            //var requestDigest = await response.Content.ReadAsStringAsync();
            return cookieContainer;
        }
    }
}
