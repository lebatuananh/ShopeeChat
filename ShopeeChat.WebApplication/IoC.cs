using Microsoft.Extensions.DependencyInjection;
using ShopeeChat.RestClient.Repositories;
using ShopeeChat.RestClient.Repositories.Interfaces;
using ShopeeChat.RestClient.RestClients;

namespace ShopeeChat.WebApplication
{
    public class IoC
    {
        public static void RegisterTypes(IServiceCollection services)
        {
            services.AddSingleton<ApiClient>();
            services.AddTransient<IConversationsRepository, ConversationsRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IUserSystemRepository, UserSystemRepository>();
        }
    }
}