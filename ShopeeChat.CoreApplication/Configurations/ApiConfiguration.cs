namespace ShopeeChat.CoreApplication.Configurations
{
    public class ApiConfiguration
    {
        public string ClientId { get; set; }
        public string Address { get; set; }
        public int TimeoutInMilliseconds { get; set; }
        public ApiUrls Urls { get; set; }
        public string[] StickerUrls { get; set; }
        public ApiConfiguration()
        {
            ClientId = "";
            Address = "";
            TimeoutInMilliseconds = 60000;
        }
    }

    public class ApiUrls
    {
        public string PathApiClientGetAllConversation { get; set; }
        public string PathApiClientGetDetailConversation { get; set; }
        public string PathApiClientLogin { get; set; }
        public string PathApiClientRefreshToken { get; set; }
        public string PathApiClientGetUnreadCount { get; set; }
        public string PathApiClientSyncConversations { get; set; }
        public string PathApiClientSendChatMessage { get; set; }
        public string PathApiClientGetNewMessage { get; set; }
        public string PathApiClientUploadImage { get; set; }
        public string PathApiClientUserSystemLogin { get; set; }
        public string PathApiClientUserSystemRegister { get; set; }
        public string PathApiClientUserSystemForgotPassword { get; set; }
    }
}