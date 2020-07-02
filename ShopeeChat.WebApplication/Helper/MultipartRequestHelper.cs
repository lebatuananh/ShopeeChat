using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace ShopeeChat.WebApplication.Helper
{
    public static class MultipartRequestHelper
    {
        public static bool IsMultipartContentType(this HttpRequest request)
        {
            return !string.IsNullOrEmpty(request.ContentType)
                   && request.ContentType.Contains("multipart/", StringComparison.OrdinalIgnoreCase);
        }
    }
}
