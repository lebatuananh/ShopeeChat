namespace ShopeeChat.CoreAPI.RestClientShopee.Models
{
    public class LoginModelRequest
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string VCode { get; set; }
        public string Captcha { get; set; }
        public string CaptchaKey { get; set; }
    }
}