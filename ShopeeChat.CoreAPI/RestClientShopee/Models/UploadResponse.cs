using System;
using Newtonsoft.Json;

namespace ShopeeChat.CoreAPI.RestClientShopee.Models
{
    public class UploadResponse
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; set; }
    }
}