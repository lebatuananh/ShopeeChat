using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ShopeeChat.CoreAPI.Statics
{
    public class JsonSettings
    {
        public static JsonSerializerSettings CamelIgnoreNullOutput = new JsonSerializerSettings()
        {
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy(),
            }
        };

        public static readonly JsonSerializerSettings Snake = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public static readonly JsonSerializerSettings Camel = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };
    }
}