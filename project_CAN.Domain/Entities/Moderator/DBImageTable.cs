using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_CAN.Domain.Entities.Moderator
{
    [Table("imageTable")]
    public class DBImageTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int imageId { get; set; }

        [Required]
        public string imageName { get; set; }
    }
}
