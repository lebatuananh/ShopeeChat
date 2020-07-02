using System;

namespace ShopeeChat.CoreAPI.RestClientShopee.Models.Base
{
    public class ApiException : Exception
    {
        public ApiException(ErrorModelRestClientShopee error)
        {
            this.Error = error;
        }

        public ErrorModelRestClientShopee Error { get; set; }
    }
}