namespace ShopeeChat.RestClient.Models
{
    public class RefreshTokenResponse
    {
        public string Token { get; set; }
        public string PToken { get; set; }
        public UserLoginResponse User { get; set; }
    }
}