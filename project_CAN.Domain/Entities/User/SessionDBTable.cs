using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_CAN.Domain.Entities.User
{
    [Table("sessionTable")]
    public class SessionDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sessionId { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }

        public virtual UDBTable User { get; set; }

        [Required]
        public string cookieValue { get; set; }

        [Required]
        public DateTime expireTime { get; set; }

    }
}
