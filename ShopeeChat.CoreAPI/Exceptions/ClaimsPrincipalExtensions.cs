using DVG.AutoPortal.InfrastructureLayer.Utility;
using ShopeeChat.CoreAPI.Models;
using ShopeeChat.CoreAPI.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ShopeeChat.CoreAPI.Exceptions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetSpecificClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType);
            return claim != null ? claim.Value : string.Empty;
        }

        public static ApplicationUser CurrentUser(this ClaimsPrincipal user)
        {
            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                return new ApplicationUser
                {
                    Id = CryptoEngine.Decrypt(user.GetSpecificClaim("Id"), ApiStaticVariables.CommonSalt),
                    Email = CryptoEngine.Decrypt(user.GetSpecificClaim("Email"), ApiStaticVariables.CommonSalt),
                    UserName = CryptoEngine.Decrypt(user.GetSpecificClaim("UserName"), ApiStaticVariables.CommonSalt)
                };
            }
            return null;
        }
    }
}
