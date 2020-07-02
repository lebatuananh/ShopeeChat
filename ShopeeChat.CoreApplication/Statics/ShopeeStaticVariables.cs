using ShopeeChat.CoreApplication.Configurations;
using ShopeeChat.Infrastructure.Utility;

namespace ShopeeChat.CoreApplication.Statics
{
    public class ShopeeStaticVariables
    {
        public static ApiConfiguration ApiConfig = AppSettings.Get("Api", new ApiConfiguration());
    }
}