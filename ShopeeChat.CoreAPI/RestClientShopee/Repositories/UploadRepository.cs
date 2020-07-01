using Microsoft.AspNetCore.Http;
using ShopeeChat.CoreAPI.Models;
using ShopeeChat.CoreAPI.RestClientShopee.Models;
using ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopeeChat.CoreAPI.Statics;

namespace ShopeeChat.CoreAPI.RestClientShopee.Repositories
{
    public class UploadRepository : BaseRepository, IUploadRepository
    {
        public UploadRepository(ShopeeApiClient shopeeApiClient) : base(shopeeApiClient)
        {
        }

        public async Task<UploadResponse> UploadImage(IFormFile formFile)
        {
            List<HeaderModel> headerModels = new List<HeaderModel>()
            {
                new HeaderModel()
                {
                    Key = "authorization",
                    Value =
                        Constants.TokenFake

                }
            };
            var result = await _shopeeApiClient.PostAsync<UploadResponse>(ApiUrls.PathUploadFile, formFile,
                Constants.MultimediaBody, headerModels);
            return result;
        }
    }
}