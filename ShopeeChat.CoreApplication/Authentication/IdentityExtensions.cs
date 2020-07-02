using System.Linq;
using System.Security.Claims;

namespace ShopeeChat.CoreApplication.Authentication
{
    public static class IdentityExtensions
    {
        public static string GetSpecificClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static ApplicationUser CurrentUser(this ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                var appUser = new ApplicationUser
                {
                    Id = int.Parse(user.GetSpecificClaim("Id")),
                    UserName = user.GetSpecificClaim("UserName"),
                    Email = user.GetSpecificClaim("Email"),
                    DisplayName = user.GetSpecificClaim("DisplayName"),
                    FullName = user.GetSpecificClaim("FullName"),
                    Avatar = user.GetSpecificClaim("Avatar"),
                    CityId = string.IsNullOrEmpty(user.GetSpecificClaim("CityId")) ? 0 : int.Parse(user.GetSpecificClaim("CityId")),
                    Token = user.GetSpecificClaim("BearerToken")
                };

                return appUser;
            }
            return null;
        }
    }
}