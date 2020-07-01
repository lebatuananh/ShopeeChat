using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ShopeeChat.CoreAPI.Configurations
{
    public class ClientAppDetectorConfiguration
    {
        public string HeaderName { get; set; }
        public Dictionary<string,string> ClientAppTokenNameMap { get; set; }

        public string GetCurrentClientAppName(HttpRequest request)
        {
            if (!request.Headers.ContainsKey(HeaderName))
                return null;
            var headerValue = request.Headers[HeaderName];

            if (!ClientAppTokenNameMap.ContainsKey(headerValue))
                return null;

            var clientName = ClientAppTokenNameMap[headerValue];
            return clientName;
        }
    }
}