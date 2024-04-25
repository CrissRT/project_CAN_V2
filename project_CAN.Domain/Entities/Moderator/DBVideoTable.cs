using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_CAN.Domain.Entities.Moderator
{
    [Table("videoTable")]
    public class DBVideoTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int videoLinkId { get; set; }

        [Required]
        public string videoLink { get; set; }
    }
}
