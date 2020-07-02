namespace ShopeeChat.CoreAPI.RestClientShopee.Models
{
    public class LoginShopeeUseVCodeAndUserNameModelRequest
    {
        public string Captcha { get; set; }
        public string Captcha_Key { get; set; }
        public bool Remember { get; set; }
        public string Password_Hash { get; set; }
        public string VCode { get; set; }
        public string UserName { get; set; }
    }
}