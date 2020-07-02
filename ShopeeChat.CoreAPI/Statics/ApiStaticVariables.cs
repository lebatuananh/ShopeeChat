using ShopeeChat.CoreAPI.Configurations;
using ShopeeChat.Infrastructure.Utility;

namespace ShopeeChat.CoreAPI.Statics
{
    public class ApiStaticVariables
    {
        public static ClientAppDetectorConfiguration ClientAppDetector = AppSettings.Get<ClientAppDetectorConfiguration>("ClientAppDectector");
        public static ApiShopeeConfiguration ApiConfig = AppSettings.Get("Api", new ApiShopeeConfiguration());

        public static AuthConfiguration AuthConfig = AppSettings.Get("Auth:Jwt", new AuthConfiguration());
        public static string CommonSalt = AppSettings.Get<string>("Application:CommonSalt");
    }
}