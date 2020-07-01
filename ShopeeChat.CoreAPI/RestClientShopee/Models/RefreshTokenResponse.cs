namespace ShopeeChat.CoreAPI.RestClientShopee.Models
{
    public class RefreshTokenResponse
    {
        public string Token { get; set; }
        public string PToken { get; set; }
        public UserLoginResponse User { get; set; }
    }
}