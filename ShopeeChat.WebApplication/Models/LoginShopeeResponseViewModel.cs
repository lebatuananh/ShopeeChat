using ShopeeChat.RestClient.Models;

namespace ShopeeChat.WebApplication.Models
{
    public class LoginShopeeResponseViewModel
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string VCode { get; set; }
        public string CaptchaKey { get; set; }
        public ErrorModel ErrorModel { get; set; }
    }
}