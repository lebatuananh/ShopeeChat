using System;

namespace ShopeeChat.RestClient.Models.Base
{
    public class ClientException : Exception
    {
        public ClientException(ErrorModel error)
        {
            this.Error = error;
        }

        public ErrorModel Error { get; set; }
    }
}