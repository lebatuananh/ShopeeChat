using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace ShopeeChat.Infrastructure.Extensions
{
    public static class IdentityResultExtensions
    {
        public static string GetError(this IdentityResult identityResult)
        {
            return identityResult.Errors.Select(e => e.Description).First();
        }
    }
}