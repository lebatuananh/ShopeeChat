using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopeeChat.WebApi.Data.Entities
{
    [Table("ShopeeUserTagMappings")]
    public class ShopeeUserTagMapping
    {
        [Required]
        public int TagId { get; set; }

        [Required]
        public int ShopeeUserId { get; set; }
    }
}