using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopeeChat.CoreAPI.RestClientShopee.Models;

namespace ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces
{
    public interface IUploadRepository
    {
        Task<UploadResponse> UploadImage(IFormFile formFile);
    }
}