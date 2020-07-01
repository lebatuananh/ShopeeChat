using Microsoft.Extensions.DependencyInjection;
using ShopeeChat.CoreAPI.RestClientShopee;
using ShopeeChat.CoreAPI.RestClientShopee.Repositories;
using ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces;

namespace ShopeeChat.Demo.WebApplication
{
    public class IoC
    {
        public static void RegisterTypes(IServiceCollection services)
        {
            services.AddSingleton<ShopeeApiClient>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IConversationsRepository, ConversationsRepository>();
        }
    }
}