namespace ShopeeChat.CoreAPI.Configurations
{
    public class ApiShopeeConfiguration
    {
        public string ClientId { get; set; }
        public string Address { get; set; }
        public int TimeoutInMilliseconds { get; set; }
        public ApiUrls Urls { get; set; }

        public ApiShopeeConfiguration()
        {
            ClientId = "";
            Address = "";
            TimeoutInMilliseconds = 60000;
        }
    }

    public class ApiUrls
    {
        public string GetDataLoginPage { get; set; }
        public string PathApiLogin { get; set; }
        public string PathRefreshToken { get; set; }
        public string PathSyncConversations { get; set; }
        public string PathUnreadCountConversations { get; set; }
        public string PathGetAllConversations { get; set; }
        public string PathGetDetailConversations { get; set; }
        public string PathSendChatMessage { get; set; }
        public string PathUploadFile { get; set; }
        public string PathGetCaptcha { get; set; }
    }
}