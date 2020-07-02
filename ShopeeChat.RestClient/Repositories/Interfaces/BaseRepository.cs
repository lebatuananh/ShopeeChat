using ShopeeChat.CoreApplication.Configurations;
using ShopeeChat.CoreApplication.Statics;
using ShopeeChat.RestClient.RestClients;
using System.Collections.Generic;

namespace ShopeeChat.RestClient.Repositories.Interfaces
{
    public class BaseRepository : IRepository
    {
        protected ApiClient ApiClient;
        protected ApiUrls ApiUrls = ApplicationStaticVariables.ApiConfig.Urls;
        protected string[] StickerUrls = ApplicationStaticVariables.ApiConfig.StickerUrls;

        public BaseRepository(ApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        protected Dictionary<string, string> ConvertFilterToDictionary(object obj)
        {
            Dictionary<string, string> newDict = new Dictionary<string, string>();
            if (typeof(IDictionary<string, string>).IsAssignableFrom(obj.GetType()))
            {
                IDictionary<string, string> idict = (IDictionary<string, string>)obj;

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