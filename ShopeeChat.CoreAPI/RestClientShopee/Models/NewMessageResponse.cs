using System;
using Newtonsoft.Json;

namespace ShopeeChat.CoreAPI.RestClientShopee.Models
{
    public class NewMessageResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("to_id")]
        public long ToId { get; set; }

        [JsonProperty("to_name")]
        public string ToName { get; set; }

        [JsonProperty("to_avatar")]
        public Uri ToAvatar { get; set; }

        [JsonProperty("shop_id")]
        public long ShopId { get; set; }

        [JsonProperty("to_shop_id")]
        public long ToShopId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("unread_count")]
        public long UnreadCount { get; set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        [JsonProperty("last_read_message_id")]
        public string LastReadMessageId { get; set; }

        [JsonProperty("latest_message_id")]
        public string LatestMessageId { get; set; }

        [JsonProperty("latest_message_type")]
        public string LatestMessageType { get; set; }

        [JsonProperty("latest_message_content")]
        public LatestMessageContent LatestMessageContent { get; set; }

        [JsonProperty("latest_message_from_id")]
        public long LatestMessageFromId { get; set; }

        [JsonProperty("last_message_time")]
        public DateTimeOffset LastMessageTime { get; set; }

        [JsonProperty("last_message_time_nano")]
        public string LastMessageTimeNano { get; set; }

        [JsonProperty("next_timestamp")]
        public long NextTimestamp { get; set; }

        [JsonProperty("next_timestamp_nano")]
        public long NextTimestampNano { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("auto_translation")]
        public bool AutoTranslation { get; set; }
    }
}