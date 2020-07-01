using ShopeeChat.Infrastructure.Models;
using System.Text.RegularExpressions;

namespace ShopeeChat.Infrastructure.Utility
{
    public static class CheckerUtility
    {
        public static string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static CheckUserNameModel CheckUserName(string userName)
        {
            CheckUserNameModel checkUserNameModel = new CheckUserNameModel();
            string x = "";
            string y = "";
            string z = "";
            MatchCollection matchNumber = Regex.Matches(userName, "(\\d)");
            MatchCollection matchUserName = Regex.Matches(userName, ".");
            for (int i = 0; i < matchNumber.Count; i++)
            {
                y = x;
                x = y + matchNumber[i].Groups[1].Value;
            }

            if (x.Length == 11)
            {
                if (userName.Length == 11)
                {
                    checkUserNameModel.IsPhone = true;
                    checkUserNameModel.UserName = userName;
                    return checkUserNameModel;
                }

                checkUserNameModel.IsUserName = true;
                checkUserNameModel.UserName = userName;
                return checkUserNameModel;
            }

            if (x.Length == 10)
            {
                if (userName.Length == 10)
                {
                    z = "84" + userName.Substring(1, 9);
                    checkUserNameModel.IsPhone = true;
                    checkUserNameModel.UserName = z;
                    return checkUserNameModel;
                }

                checkUserNameModel.IsUserName = true;
                checkUserNameModel.UserName = userName;
                return checkUserNameModel;
            }

            if (x.Length == 9)
            {
                if (userName.Length == 9)
                {
                    z = "84" + userName;
                    checkUserNameModel.UserName = z;
                    checkUserNameModel.IsPhone = true;
                    return checkUserNameModel;
                }

                checkUserNameModel.UserName = userName;
                checkUserNameModel.IsUserName = true;
                return checkUserNameModel;
            }
            if (matchUserName.Count <= 0) return checkUserNameModel;
            checkUserNameModel.UserName = userName;
            checkUserNameModel.IsUserName = true;
            return checkUserNameModel;
        }
    }
}