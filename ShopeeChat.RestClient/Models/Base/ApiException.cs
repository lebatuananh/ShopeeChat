using System;

namespace ShopeeChat.RestClient.Models.Base
{
    public class ApiException : Exception
    {
        public ApiException(ErrorModel error)
        {
            this.Error = error;
        }

        public ErrorModel Error { get; set; }
    }
}