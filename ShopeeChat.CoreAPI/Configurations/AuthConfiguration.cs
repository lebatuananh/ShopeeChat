using System;
using System.Collections.Generic;
using System.Text;

namespace ShopeeChat.CoreAPI.Configurations
{
    public class AuthConfiguration
    {
        public string Audience { get; set; }
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int TokenExpirationInMinutes { get; set; }
    }
}
