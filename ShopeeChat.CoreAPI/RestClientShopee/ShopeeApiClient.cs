using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShopeeChat.CoreAPI.Models;
using ShopeeChat.CoreAPI.RestClientShopee.Models.Base;
using ShopeeChat.CoreAPI.Statics;

namespace ShopeeChat.CoreAPI.RestClientShopee
{
    public class ShopeeApiClient : CustomHttpClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShopeeApiClient(
            IHttpContextAccessor httpContextAccessor
        ) : base()
        {
            this._httpContextAccessor = httpContextAccessor;
            BaseAddress = new Uri(ApiStaticVariables.ApiConfig.Address);
            Timeout = TimeSpan.FromMilliseconds(ApiStaticVariables.ApiConfig.TimeoutInMilliseconds);
        }

        private static readonly JsonSerializerSettings JSON_SETTING = NewtonJsonSettings.SNAKE;

        public override string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, JSON_SETTING);
        }

        public override T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, JSON_SETTING);
        }

        public override async Task PreprocessResponse(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                string bodyText = await responseMessage.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModelRestClientShopee>(bodyText, JSON_SETTING);
                throw new ApiException(errorModel);
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