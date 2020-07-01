using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopeeChat.WebApi.Data.Entities
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Alias { get; set; }
        public string TagColor { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}