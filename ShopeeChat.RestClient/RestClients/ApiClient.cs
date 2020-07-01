using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShopeeChat.CoreApplication.Authentication;
using ShopeeChat.CoreApplication.Statics;
using ShopeeChat.RestClient.Models;
using ShopeeChat.RestClient.Models.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopeeChat.RestClient.RestClients
{
    public class ApiClient : CustomHttpClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiClient(
            IHttpContextAccessor httpContextAccessor
        ) : base()
        {
            this._httpContextAccessor = httpContextAccessor;
            BaseAddress = new Uri(ShopeeStaticVariables.ApiConfig.Address);
            Timeout = TimeSpan.FromMilliseconds(ShopeeStaticVariables.ApiConfig.TimeoutInMilliseconds);
        }

        private static readonly JsonSerializerSettings JSON_SETTING = NewtonJsonSettings.CAMEL;

        public override string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, JSON_SETTING);
        }

        public override T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, JSON_SETTING);
        }

        public override HttpRequestMessage InitRequest(HttpMethod method, string path, Dictionary<string, string> queries)
        {
            var request = base.InitRequest(method, path, queries);
            var user = (ApplicationUser)_httpContextAccessor.HttpContext.User.CurrentUser();
            request.Headers.Add("X-AutoPortal-App-Token", ShopeeStaticVariables.ApiConfig.ClientId);
            if (user != null)
            {
                var jwt = user.Token;
                request.Headers.Add("Authorization", "Bearer " + jwt);
            }
            return request;
        }

        public override async Task PreprocessResponse(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                string bodyText = await responseMessage.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(bodyText, JSON_SETTING);
                throw new ClientException(errorModel);
            }
        }
    }

    public static class NewtonJsonSettings
    {
        public static readonly JsonSerializerSettings SNAKE = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public static readonly JsonSerializerSettings CAMEL = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };
    }
}