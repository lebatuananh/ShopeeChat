using System;
using Newtonsoft.Json;

namespace ShopeeChat.CoreAPI.RestClientShopee.Models
{
    public class ChatMessagesUsingTextRequest
    {
        [JsonProperty("request_id")]
        public Guid RequestId { get; set; }

        [JsonProperty("to_id")]
        public long ToId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("content")]
        public ContentChatMessagesUsingTextRequest Content { get; set; }

        [JsonProperty("chat_send_option")]
        public ChatSendOption ChatSendOption { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public class ChatSendOption
    {
        [JsonProperty("force_send_cancel_order_warning")]
        public bool ForceSendCancelOrderWarning { get; set; }

        [JsonProperty("comply_cancel_order_warning")]
        public bool ComplyCancelOrderWarning { get; set; }
    }

    public class ContentChatMessagesUsingTextRequest
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}