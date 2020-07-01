using System.Threading.Tasks;
using ShopeeChat.RestClient.Models;
using ShopeeChat.RestClient.Models.Requests;
using ShopeeChat.RestClient.Models.Responses;
using ShopeeChat.RestClient.Repositories.Interfaces;
using ShopeeChat.RestClient.RestClients;

namespace ShopeeChat.RestClient.Repositories.Interfaces
{
    public class UserSystemRepository : BaseRepository, IUserSystemRepository
    {
        public UserSystemRepository(ApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<UserSystemLoginResponse> LoginSystem(UserSystemLoginRequest model)
        {
            var result = await ApiClient.PostAsync<UserSystemLoginResponse>(ApiUrls.PathApiClientUserSystemLogin, model);
            return result;
        }
    }
}
