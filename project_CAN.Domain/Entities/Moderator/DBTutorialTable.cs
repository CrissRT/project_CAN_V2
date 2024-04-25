using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_CAN.Domain.Entities.Moderator
{
    [Table("tutorialTable")]
    public class DBTutorialTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tutorialId { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        [ForeignKey("Image")]
        public int imageId { get; set; }

        [Required]
        [ForeignKey("Video")]
        public int videoLinkId { get; set; }

        public virtual DBImageTable Image { get; set; }

        public virtual DBVideoTable Video { get; set; }
    }
}
