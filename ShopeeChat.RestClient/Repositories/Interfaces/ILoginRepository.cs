using System.Threading.Tasks;
using ShopeeChat.RestClient.Models;

namespace ShopeeChat.RestClient.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<LoginModelResponse> Login(LoginModelRequest loginModelRequest);
        Task<RefreshTokenResponse> RefreshToken();
    }
}