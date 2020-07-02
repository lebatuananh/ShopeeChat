using System;
using System.Collections.Generic;
using System.Net;

namespace ShopeeChat.CoreAPI.Exceptions
{
    public abstract class BaseCustomException : Exception
    {
        public List<string> Messages { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public object AdditionalData { get; set; }

        public BaseCustomException(List<string> messages, HttpStatusCode statusCode)
        {
            Messages = messages;
            StatusCode = statusCode;
        }
    }
}