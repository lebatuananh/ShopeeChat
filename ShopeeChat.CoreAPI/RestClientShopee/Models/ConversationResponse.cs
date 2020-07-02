using System;

namespace ShopeeChat.CoreAPI.RestClientShopee.Models
{
    public class ConversationResponse
    {
        public string Id { get; set; }
        public int ToId { get; set; }
        public string ToName { get; set; }
        public string ToAvatar { get; set; }
        public int ShopId { get; set; }
        public int ToShopId { get; set; }
        public string Status { get; set; }
        public int UnreadCount { get; set; }
        public bool Pinned { get; set; }
        public string LastReadMessageId { get; set; }
        public string LatestMessageId { get; set; }
        public string LatestMessageType { get; set; }
        public LatestMessageContent LatestMessageContent { get; set; }
        public int LatestMessageFromId { get; set; }
        public DateTime LastMessageTime { get; set; }
        public int NextTimestamp { get; set; }
        public string NextTimestampNano { get; set; }
    }

    public class LatestMessageContent
    {
        public string Text { get; set; }
    }
}