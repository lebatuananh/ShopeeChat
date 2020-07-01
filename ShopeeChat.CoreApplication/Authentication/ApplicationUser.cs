using System;
using System.Security.Principal;

namespace ShopeeChat.CoreApplication.Authentication
{
    public class ApplicationUser : IIdentity
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual decimal Gender { get; set; }
        public virtual string Address { get; set; }
        public virtual string LoweredUsername { get; set; }
        public virtual string MobileAlias { get; set; }
        public virtual string Password { get; set; }
        public virtual string Hashcode { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordQuestion { get; set; }
        public virtual string PasswordAnswer { get; set; }
        public virtual bool IsApproved { get; set; }
        public virtual bool IsAnonymous { get; set; }
        public string FullName { get; set; }
        public virtual decimal FailedPasswordAttemptCount { get; set; }
        public virtual string ActivateCode { get; set; }
        public virtual DateTime Createdate { get; set; }
        public virtual DateTime LastupdatedDate { get; set; }
        public virtual int CreatedUser { get; set; }
        public virtual int LastUpdatedUser { get; set; }
        public virtual DateTime RequestPasswordDate { get; set; }
        public virtual bool Islocked { get; set; }
        public virtual string Avatar { get; set; }
        public virtual string MobilePrefix { get; set; }
        public virtual int Type { get; set; }
        public virtual int ProductCount { get; set; }
        public virtual long CreatedAteSpan { get; set; }
        public virtual string Note { get; set; }
        public virtual int UserCrawlId { get; set; }
        public virtual string PhoneImages { get; set; }
        public int? CityId { get; set; }
        public string BirthDay { get; set; }
        public bool PhoneVerify { get; set; }

        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}