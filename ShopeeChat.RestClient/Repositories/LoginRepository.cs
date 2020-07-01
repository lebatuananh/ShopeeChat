using System.Threading.Tasks;
using ShopeeChat.RestClient.Models;
using ShopeeChat.RestClient.Repositories.Interfaces;
using ShopeeChat.RestClient.RestClients;

namespace ShopeeChat.RestClient.Repositories
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        public LoginRepository(ApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<LoginModelResponse> Login(LoginModelRequest loginModelRequest)
        {
            var result = await ApiClient.PostAsync<LoginModelResponse>(ApiUrls.PathApiClientLogin, loginModelRequest);
            return result;
        }

        public async Task<RefreshTokenResponse> RefreshToken()
        {
            var result = await ApiClient.GetAsync<RefreshTokenResponse>(ApiUrls.PathApiClientRefreshToken);
            return result;
        }
    }
}