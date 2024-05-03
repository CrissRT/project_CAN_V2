using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using project_CAN.Domain.Entities.Moderator;

namespace project_CAN.Domain.Entities.User
{
    [Table("likesTable")]
    public class LikesDBTable
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("User")]
        public int userId { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Tutorial")]
        public int tutorialId { get; set; }


        public virtual UDBTable User { get; set; }

        public virtual DBTutorialTable Tutorial { get; set; }
    }
}
