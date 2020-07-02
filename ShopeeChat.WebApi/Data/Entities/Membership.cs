using Microsoft.AspNetCore.Identity;
using ShopeeChat.WebApi.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopeeChat.WebApi.Data.Entities
{
    [Table("Memberships")]
    public class Membership : IdentityUser, IDateTracking, ISwitchable
    {
        [MaxLength(50)]
        [Required]
        public string FullName { get; set; }

        public string Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}