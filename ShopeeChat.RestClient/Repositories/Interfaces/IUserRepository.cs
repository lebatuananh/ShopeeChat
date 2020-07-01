using ShopeeChat.RestClient.Models.Requests;
using ShopeeChat.RestClient.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeChat.RestClient.Repositories.Interfaces
{
    public interface IUserSystemRepository
    {
        Task<UserSystemLoginResponse> LoginSystem(UserSystemLoginRequest model);
    }
}
