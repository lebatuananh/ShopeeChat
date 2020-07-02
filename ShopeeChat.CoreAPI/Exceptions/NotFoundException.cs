using System.Collections.Generic;
using ShopeeChat.CoreAPI.Exceptions;

namespace DVG.AutoPortal.Indian.Satellite.API.Core.Exceptions
{
    public class NotFoundException : BaseCustomException
    {
        public NotFoundException(string message) : base(new List<string>() {message},
            System.Net.HttpStatusCode.NotFound)
        {
        }
    }
}