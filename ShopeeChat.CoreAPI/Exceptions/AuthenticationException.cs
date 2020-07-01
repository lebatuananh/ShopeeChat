using System.Collections.Generic;
using System.Net;

namespace ShopeeChat.CoreAPI.Exceptions
{
    public class AuthenticationException : BaseCustomException
    {
        public AuthenticationException(string message) : base(new List<string>() { message }, HttpStatusCode.Unauthorized)
        {

        }
    }

}
