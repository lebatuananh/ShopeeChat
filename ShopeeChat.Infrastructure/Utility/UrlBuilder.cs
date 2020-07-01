using System;
using System.Collections.Generic;
using System.Text;

namespace ShopeeChat.Infrastructure.Utility
{
    public class UrlBuilder
    {
        private const string ApplicationUrl = "https://localhost:5001";

        public static string EmailConfirmationLink(int userId, string code)
        {
            return $"{ApplicationUrl}/v1/Account/ConfirmEmail?userId={userId}&code={code}";
        }

        public static string ResetPasswordCallbackLink(string code)
        {
            return $"{ApplicationUrl}/v1/Account/ResetPassword?code={code}";
        }
    }
}
