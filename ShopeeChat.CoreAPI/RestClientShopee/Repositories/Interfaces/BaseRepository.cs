using System.Collections.Generic;
using ShopeeChat.CoreAPI.Configurations;
using ShopeeChat.CoreAPI.Statics;

namespace ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces
{
    public class BaseRepository : IRepository
    {
        protected ShopeeApiClient _shopeeApiClient;
        protected ApiUrls ApiUrls = ApiStaticVariables.ApiConfig.Urls;

        public BaseRepository(ShopeeApiClient shopeeApiClient)
        {
            _shopeeApiClient = shopeeApiClient;
        }

        protected Dictionary<string, string> ConvertFilterToDictionary(object obj)
        {
            Dictionary<string, string> newDict = new Dictionary<string, string>();
            if (typeof(IDictionary<string, string>).IsAssignableFrom(obj.GetType()))
            {
                IDictionary<string, string> idict = (IDictionary<string, string>) obj;

                foreach (var key in idict.Keys)
                {
                    newDict.Add(key.ToString(), idict[key].ToString());
                }
            }
            else
            {
                // My object is not a dictionary
            }

            return newDict;
        }
    }
}