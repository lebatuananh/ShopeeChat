using System;
using Newtonsoft.Json;

namespace ShopeeChat.CoreAPI.RestClientShopee.Models
{
    public class ChatMessageRequest
    {
        public Guid RequestId { get; set; }

        public long ToId { get; set; }

        public string Type { get; set; }

        public ContentChatMessagesRequest Content { get; set; }

        public ChatSendOption ChatSendOption { get; set; }

        public string Source { get; set; }
    }

    public class ContentChatMessagesRequest
    {
        public string Text { get; set; }

        public string StickerId { get; set; }

        public string StickerPackageId { get; set; }
        public Uri Url { get; set; }
    }

    public class ChatSendOption
    {
        public bool ForceSendCancelOrderWarning { get; set; }

        public bool ComplyCancelOrderWarning { get; set; }
    }
}