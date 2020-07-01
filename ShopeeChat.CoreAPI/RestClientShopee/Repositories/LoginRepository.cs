using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopeeChat.CoreAPI.Models;
using ShopeeChat.CoreAPI.RestClientShopee.Models;
using ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces;
using ShopeeChat.CoreAPI.Statics;
using ShopeeChat.Infrastructure.Utility;

namespace ShopeeChat.CoreAPI.RestClientShopee.Repositories
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        public LoginRepository(ShopeeApiClient shopeeApiClient) : base(shopeeApiClient)
        {
        }

        public async Task<LoginInterfaceResponse> GetLoginInterface()
        {
            var result = await _shopeeApiClient.GetAsync<LoginInterfaceResponse>(ApiUrls.GetDataLoginPage);
            return result;
        }

        public async Task<LoginModelResponse> Login(object loginObj)
        {
            var result =
                await _shopeeApiClient.PostLoginShopeeAsync<LoginModelResponse>(ApiUrls.PathApiLogin, loginObj, Constants.FormData);
            return result;
        }

        public async Task<RefreshTokenResponse> RefreshToken()
        {
            List<HeaderModel> headerModels = new List<HeaderModel>()
            {
                new HeaderModel()
                {
                    Key = "cookie",
                    Value =
                        "SPC_EC=dtZjS3YehTBxOuRx5jtIJVSDXA3j/RyzXLNhQAolZOKr7qnzeABb6Lz2W/y+kENHyGVTT83DE8yi94OcAjjutGsZl0MdOpfVz4og+3V1WGZXEOm6Kl/xc8wEpsbmDbAOrDHRD+aREPTzBbKNiP3x+Q=="
                }
            };
            var result = await _shopeeApiClient.PostAsync<RefreshTokenResponse>(ApiUrls.PathRefreshToken, null, Constants.FormData, headerModels);
            return result;
        }

        public async Task<byte[]> GetCaptcha(object o)
        {
            List<HeaderModel> headerModels = new List<HeaderModel>()
            {
                new HeaderModel()
                {
                    Key = "cookie",
                    Value =
                        "SPC_F=5xZlJ50EWFbkMmqrqd5VyXR5eTcNzapx"
                }
            };
            var result = await _shopeeApiClient.GetStreamAsync<object>(ApiUrls.PathGetCaptcha, headerModels, o.ToDictionary());
            return result;
        }
    }
}