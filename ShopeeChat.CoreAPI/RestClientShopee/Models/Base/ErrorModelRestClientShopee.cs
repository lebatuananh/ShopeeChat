using System.Collections.Generic;

namespace ShopeeChat.CoreAPI.RestClientShopee.Models.Base
{
    public class ErrorModelRestClientShopee
    {
        public int ErrCode { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ErrMessage { get; set; }
    }
}