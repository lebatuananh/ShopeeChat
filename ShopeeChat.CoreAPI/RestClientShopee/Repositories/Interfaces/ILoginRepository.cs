using System.Threading.Tasks;
using ShopeeChat.CoreAPI.RestClientShopee.Models;

namespace ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<LoginInterfaceResponse> GetLoginInterface();
        Task<LoginModelResponse> Login(object loginObj);
        Task<RefreshTokenResponse> RefreshToken();
        Task<byte[]> GetCaptcha(object o);
    }
}