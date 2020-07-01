using System;

namespace ShopeeChat.RestClient.Models
{
    public class NewMessageResponse
    {
        public string Id { get; set; }

        public long ToId { get; set; }

        public string ToName { get; set; }

        public Uri ToAvatar { get; set; }

        public long ShopId { get; set; }

        public long ToShopId { get; set; }

        public string Status { get; set; }

        public long UnreadCount { get; set; }

        public bool Pinned { get; set; }

        public string LastReadMessageId { get; set; }

        public string LatestMessageId { get; set; }

        public string LatestMessageType { get; set; }

        public LatestMessageContent LatestMessageContent { get; set; }

        public long LatestMessageFromId { get; set; }

        public DateTimeOffset LastMessageTime { get; set; }

        public string LastMessageTimeNano { get; set; }

        public long NextTimestamp { get; set; }

        public long NextTimestampNano { get; set; }

        public string Flag { get; set; }

        public bool AutoTranslation { get; set; }
    }
}