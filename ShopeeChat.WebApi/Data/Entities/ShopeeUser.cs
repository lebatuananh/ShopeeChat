using ShopeeChat.WebApi.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopeeChat.WebApi.Data.Entities
{
    [Table("ShopeeUsers")]
    public class ShopeeUser : IDateTracking, ISwitchable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid MembershipId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Userid { get; set; }
        public string UserName { get; set; }
        public string ShopeeUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public string SpcEc { get; set; }
        public string Token { get; set; }
        public string Avatar { get; set; }
        public string Locale { get; set; }
        public string ShopId { get; set; }
    }
}